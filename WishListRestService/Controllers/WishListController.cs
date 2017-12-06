using System;
using System.Collections.Generic;
using System.Web.Http;
using WishListRestService.Models;
using WishListRestService.Services;


namespace WishListRestService.Controllers {
    public class WishListController : ApiController {
        private readonly IWishListRepository _wishListRepository;

        public WishListController(IWishListRepository wishListRepository) {
            _wishListRepository = wishListRepository;
        }

        public IEnumerable<WishList> Get() {
            return _wishListRepository.All;
        }

        // GET api/values/5
        public IHttpActionResult Get(int id) {
            return Ok(_wishListRepository.Find(id));
        }

        public IHttpActionResult Create([Microsoft.AspNetCore.Mvc.FromBody] WishList item) {
            try {
                if (item == null || !ModelState.IsValid) {
                    return BadRequest("Not valid");
                }
                _wishListRepository.Insert(item);
            }
            catch (Exception) {
                return BadRequest("Couldn't create");
            }
            return Ok(item);
        }
    }
}