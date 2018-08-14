using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishListRestService.Models;
using WishListRestService.Repository;

namespace WishListRestService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class WishListController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IWishListRepository WishListRepository { get; set; }

        public WishListController(IWishListRepository repo, UserManager<ApplicationUser> userManager)
        {
            WishListRepository = repo;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var name = _userManager.GetUserAsync(User).Result?.UserName;
            var wishLists = WishListRepository.GetAll().Where(wl => wl.CreatorName == name);
            var enumerable = wishLists as Wishlist[] ?? wishLists.ToArray();
            foreach (var wishList in enumerable)
            {
                wishList.PendingInvites.ForEach(pi => pi.WishList = null);
                wishList.Subscribers.ForEach(s => s.WishList = null);
                wishList.PendingRequests.ForEach(pr => pr.WishList = null);
            }
            return Ok(enumerable);
        }

        [HttpGet("all")]
        public IEnumerable<Wishlist> GetAll()
        {
            var wishLists = WishListRepository.GetAll();
            var enumerable = wishLists as Wishlist[] ?? wishLists.ToArray();
            foreach (var wishList in enumerable)
            {
                wishList.PendingInvites.ForEach(pi => pi.WishList = null);
                wishList.Subscribers.ForEach(s => s.WishList = null);
                wishList.PendingRequests.ForEach(pr => pr.WishList = null);
            }
            return enumerable;
        }

        [Authorize]
        [ProducesResponseType(201, Type = typeof(IList<PendingRequest>))]
        [HttpGet("requests")]
        public IActionResult GetRequests()
        {
            var name = _userManager.GetUserAsync(User).Result?.UserName;
            var requests = WishListRepository.GetAll().Where(wl => wl.CreatorName == name && wl.PendingRequests.Count > 0).SelectMany(wl => wl.PendingRequests).ToArray();
            foreach (var request in requests) request.WishList = null;
            return Ok(requests);
        }

        [HttpGet("{id}", Name = "GetWishLists")]
        public IActionResult GetById(int id)
        {
            var item = WishListRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item.PendingInvites.ForEach(pi => pi.WishList = null);
            item.Subscribers.ForEach(sub => sub.WishList = null);
            item.PendingRequests.ForEach(pr => pr.WishList = null);

            return Ok(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] Wishlist item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var currentUser = _userManager.GetUserAsync(User).Result;
            currentUser.CreateWishList(item);

            WishListRepository.Add(item);

            // _userManager.UpdateAsync(currentUser);
            WishListRepository.SaveChanges();

            return Created($"/api/wishlist/{item.WishlistId}", item);
        }

        [Authorize]
        [HttpPost("{id}/wish")]
        public IActionResult CreateWish(int id, [FromBody] Wish item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var wishList = WishListRepository.Find(id);

            wishList.AddWish(item);

            WishListRepository.Update(wishList);

            return Created($"/api/wish/{item.WishId}", item);
        }

        [Authorize]
        [HttpPost("{id}/invite")]
        public IActionResult InvitePerson(int id, [FromBody] EmailModel email)
        {
            if (string.IsNullOrWhiteSpace(email.Email))
            {
                return BadRequest("no valid email");
            }

            var wishList = WishListRepository.Find(id);
            var user = _userManager.FindByEmailAsync(email.Email).Result;

            if (wishList.PendingInvites.Any(pi => pi.UserId == user.Id))
            {
                return BadRequest("invite already exists");
            }

            PendingInvite invite = new PendingInvite
            {
                User = user,
                WishList = wishList
            };

            wishList.AddInvite(invite);

            WishListRepository.Update(wishList);
            // _userManager.UpdateAsync(user);

            return Ok();
        }

        [HttpGet("invited")]
        public IEnumerable<Wishlist> InvitedWishLists()
        {
            var id = _userManager.GetUserAsync(User).Result?.Id;

            var wishLists = WishListRepository.GetAll().Where(wl => wl.PendingInvites.Any(pi => pi.UserId == id));
            var invitedWishLists = wishLists as Wishlist[] ?? wishLists.ToArray();
            foreach (var wishList in invitedWishLists)
            {
                wishList.PendingInvites = null;
                wishList.Subscribers = null;
            }
            return invitedWishLists;
        }

        [Authorize]
        [HttpGet("subscribed")]
        public IEnumerable<Wishlist> SubscribedWishLists()
        {
            var id = _userManager.GetUserAsync(User).Result?.Id;

            var wishLists = WishListRepository.GetAll().Where(wl => wl.Subscribers.Any(s => s.UserId == id));

            // var filtered = wishLists.Where(wl => wl.Subscribers.Any(s => s.UserId == id));
            var subscribedWishLists = wishLists as Wishlist[] ?? wishLists.ToArray();
            foreach (var wishList in subscribedWishLists)
            {
                wishList.PendingInvites = null;
                wishList.Subscribers = null;
            }
            return subscribedWishLists;
        }

        [Authorize]
        [HttpPost("{id}/accept")]
        public IActionResult AcceptInvite(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;

            var wishList = WishListRepository.Find(id);


            if (wishList.Subscribers.Any(pi => pi.UserId == user?.Id))
            {
                return BadRequest("subscription already exists");
            }


            wishList.PendingInvites.RemoveAll(pi => pi.UserId == user?.Id);

            var subscriber = new WishlistSubscriber
            {
                User = _userManager.GetUserAsync(User).Result,
                WishList = wishList
            };

            wishList.AddSubscriber(subscriber);

            WishListRepository.Update(wishList);
            // _userManager.UpdateAsync(user);

            return Ok();
        }

        [Authorize]
        [HttpPost("{id}/request")]
        public IActionResult RequestAccess(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;

            var wishList = WishListRepository.Find(id);

            if (wishList.CreatorName == user.Email)
            {
                return BadRequest("not possible to request access to own wishlist");
            }
            if (wishList.Subscribers.Any(sub => sub.UserId == user?.Id))
            {
                return BadRequest("already subscribed to wishlist");
            }
            if (wishList.PendingRequests.Any(pi => pi.UserId == user?.Id))
            {
                return BadRequest("access already requested");
            }

            var request = new PendingRequest
            {
                User = user,
                WishList = wishList
            };

            wishList.AddRequest(request);

            WishListRepository.Update(wishList);

            return Ok();
        }

        [HttpPost("{id}/grant")]
        public IActionResult GrantAccess(int id, [FromBody] EmailModel email)
        {
            var user = _userManager.GetUserAsync(User).Result;

            var wishList = WishListRepository.Find(id);

            if (wishList.CreatorName != user.Email)
            {
                return BadRequest("only owner of wishlist can grant access");
            }

            var requestUser = _userManager.FindByEmailAsync(email.Email).Result;

            if (!wishList.PendingRequests.Any(pr => pr.UserId == requestUser?.Id))
            {
                return BadRequest("user with given email did not request access");
            }

            wishList.PendingRequests.RemoveAll(pr => pr.UserId == requestUser.Id);

            var subscriber = new WishlistSubscriber
            {
                User = requestUser,
                WishList = wishList
            };

            wishList.Subscribers.Add(subscriber);

            WishListRepository.Update(wishList);

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Wishlist item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var contactObj = WishListRepository.Find(id);
            if (contactObj == null)
            {
                return NotFound();
            }
            WishListRepository.Update(item);
            return new NoContentResult();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            WishListRepository.Remove(id);
        }
    }

    public class EmailModel
    {
        public string Email { get; set; }
    }
}