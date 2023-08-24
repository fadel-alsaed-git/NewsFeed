using Application.IServices;
using Domain.Filters;
using Localization.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using NewsFeed.Models;
using System.Diagnostics;

namespace NewsFeed.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsFeedService _newsFeedService;
        public HomeController(ILogger<HomeController> logger, INewsFeedService newsFeedService)
        {
            _logger = logger;
            _newsFeedService = newsFeedService;
        }

        public IActionResult Index()
        {

            return View();
        }


        public async Task<IActionResult> GetFeed([FromQuery] FeedFilter filter)
        {
           
            return Ok(await _newsFeedService.Filter(filter));
        }
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            });
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Details(int id)
        {

            var feed = await _newsFeedService.GetDetails(id);

            if (feed == null)
               return NotFound();

            return View(feed);
        }

       
     

    }
}