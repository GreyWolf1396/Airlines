using System.Linq;
using System.Web.Mvc;
using BLL;
using Grey_Airlines.Models.AirlineModels;

namespace Grey_Airlines.Controllers
{
    [AllowAnonymous]
    public class SearchController : Controller
    {
        private BllUnit _bllUnit;

        public SearchController()
        {
            _bllUnit = new BllUnit();
        }

        public ActionResult Index()
        {
            ViewBag.Airports = new SelectList(_bllUnit.AirlineService.Airports.GetAll().Select(a => new AirportModel()
                {
                    Id = a.Id,
                    Name = $"{a.Name} ({a.City})"
                }).ToList(), "Id", "Name");
            return View();
        }

        public ActionResult ErrorSearch(string message)
        {
            return View((object)message);
        }
    }
}