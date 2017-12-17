using Microsoft.AspNetCore.Mvc;

namespace WishListRestService.Controllers
{
    public class IndexController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok("Server is running!");
        }
    }
}