using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.Repository;
using System.Diagnostics;

namespace RealEstate.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string search)
        {
            RealEstateReservationRepository reservationRepository = new RealEstateReservationRepository();
            var reservations = reservationRepository.GetAll();
            for (int i = 0; i < reservations.Count; i++)
            {
                var startDate = reservations[i].StartReservationDate;
                var endDate = reservations[i].EndReservationDate;
                TimeSpan duration = endDate - startDate;
                if (duration.TotalMilliseconds < 0)
                {
                    var realId = reservations[i].RealEstateId;
                    RealEstateRepository realEstateRepository = new RealEstateRepository();
                    realEstateRepository.ChangeAvailabilty(realId);
                    RealEstateReservationRepository reserveationRep=new RealEstateReservationRepository();
                    reserveationRep.Delete(realId);
                }
            }
            if(search == null)
            {
                RealEstateRepository realRep =new RealEstateRepository();
                var list=realRep.GetAll();
                var floor = list.Where(x => x.Category.ToLower().Contains("floor")).ToList();
                var villa = list.Where(x => x.Category.ToLower().Contains("villa")).ToList();
                var store = list.Where(x => x.Category.ToLower().Contains("store")).ToList();
                var other = list.Where(x =>!( x.Category.ToLower().Contains("floor") 
                || x.Category.ToLower().Contains("villa") 
                || x.Category.ToLower().Contains("store"))).ToList();
                ViewData["floor"] = floor;
                ViewData["villa"] = villa;
                ViewData["store"] = store;
                ViewData["other"] = other;
                return View("index");
            }
            else
            {
                decimal area = 0;
                bool ok =decimal.TryParse(search, out area);
                if (ok)
                {
                    RealEstateRepository realEstateRep =new RealEstateRepository();
                    var list = realEstateRep.SearchByArea(area);
                    var floor = list.Where(x => x.Category.ToLower().Contains("floor")).ToList();
                    var villa = list.Where(x => x.Category.ToLower().Contains("villa")).ToList();
                    var store = list.Where(x => x.Category.ToLower().Contains("store")).ToList();
                    var other = list.Where(x => !(x.Category.ToLower().Contains("floor")
                    || x.Category.ToLower().Contains("villa")
                    || x.Category.ToLower().Contains("store"))).ToList();
                    ViewData["floor"] = floor;
                    ViewData["villa"] = villa;
                    ViewData["store"] = store;
                    ViewData["other"] = other;
                    return View("index");
                }
                else
                {
                    RealEstateRepository realEstateRep = new RealEstateRepository();
                    var list = realEstateRep.SeaechByAddress(search);
                    var floor = list.Where(x => x.Category.ToLower().Contains("floor")).ToList();
                    var villa = list.Where(x => x.Category.ToLower().Contains("villa")).ToList();
                    var store = list.Where(x => x.Category.ToLower().Contains("store")).ToList();
                    var other = list.Where(x => !(x.Category.ToLower().Contains("floor")
                    || x.Category.ToLower().Contains("villa")
                    || x.Category.ToLower().Contains("store"))).ToList();
                    ViewData["floor"] = floor;
                    ViewData["villa"] = villa;
                    ViewData["store"] = store;
                    ViewData["other"] = other;
                    return View("index");
                }
            }

        }
        public IActionResult Support()
        {
            return View();
        }
    }
}