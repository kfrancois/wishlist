using System;
using WebApplication1.Models;
using WebApplication1.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers
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

        [HttpGet]
        public IEnumerable<WishList> Get()
        {
            var name = _userManager.GetUserAsync(User).Result?.UserName;
            return WishListRepository.GetAll().Where(wl => wl.CreatorName == name);
        }

        [HttpGet("all")]
        public IEnumerable<WishList> GetAll()
        {
            var wishLists = WishListRepository.GetAll();
            return wishLists;
        }

        [HttpGet("{id}", Name = "GetWishLists")]
        public IActionResult GetById(int id)
        {
            var item = WishListRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] WishList item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var currentUser = _userManager.GetUserAsync(User).Result;
            currentUser.CreateWishList(item);

            WishListRepository.Add(item);

            _userManager.UpdateAsync(currentUser);
            WishListRepository.SaveChanges();

            return Created($"/api/wishlist/{item.WishListId}", item);
        }

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
                UserId = user.Id,
                User = user,
                WishListId = wishList.WishListId,
                WishList = wishList
            };

            wishList.AddInvite(invite);

            WishListRepository.Update(wishList);
            _userManager.UpdateAsync(user);

            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] WishList item)
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