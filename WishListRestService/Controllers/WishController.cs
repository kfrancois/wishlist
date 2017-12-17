using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WishListRestService.Models;
using WishListRestService.Repository;

namespace WishListRestService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class WishController : Controller
    {
        public IWishRepository WishRepository { get; set; }

        public WishController(IWishRepository repo)
        {
            WishRepository = repo;
        }

        [HttpGet]
        public IEnumerable<Wish> GetAll()
        {
            return WishRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetWishes")]
        public IActionResult GetById(int id)
        {
            var item = WishRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] Wish item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            WishRepository.Add(item);
            return Created($"/api/wishlist/{item.WishId}", item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Wish item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var contactObj = WishRepository.Find(id);
            if (contactObj == null)
            {
                return NotFound();
            }
            WishRepository.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            WishRepository.Remove(id);
        }
    }
}