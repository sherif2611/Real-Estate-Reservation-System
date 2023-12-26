using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Models;
using RealEstate.Repository;
using RealEstate.ViewModels;
using System;
using System.Security.Claims;

namespace RealEstate.Controllers
{
    public class RealEstateController : Controller
    {
        public IActionResult AddRealEstate()
        {
            return View();
        }
        public IActionResult SaveAddition(RealEstateViewModel realEstate,List<IFormFile> Imgs)
        {
            if (ModelState.IsValid)
            {
                byte[] MainImageBytes;
                byte[] CredentailImageBytes;
                using (var memoryStream = new MemoryStream())
                {
                    realEstate.MainImg.CopyTo(memoryStream);
                    MainImageBytes = memoryStream.ToArray();
                }
                using (var memoryStream = new MemoryStream())
                {
                    realEstate.Credentail.CopyTo(memoryStream);
                    CredentailImageBytes = memoryStream.ToArray();
                }
                PersonRepository personRepository = new PersonRepository();
                var account = User.FindFirstValue(ClaimTypes.Email);
                var person = personRepository.GetPersonByEmail(account);
                var personId = person.Id;
                UserRepository userRepository = new UserRepository();
                var user = userRepository.GetByPersonId(personId);
                var userId = user.Id;
                Models.RealEstate obj = new Models.RealEstate();
                obj.Title = realEstate.Title;
                obj.Address = realEstate.Address;
                obj.Area = realEstate.Area;
                obj.Desc = realEstate.Desc;
                obj.Price = realEstate.Price;
                obj.AdminId = 1;
                obj.UserId = userId;
                obj.MainImg = MainImageBytes;
                obj.Category = realEstate.Category;
                obj.Credentail = CredentailImageBytes;
                obj.Available = true;
                RealEstateRepository realEstateRepository = new RealEstateRepository();
                realEstateRepository.Insert(obj);
                
                var id = realEstateRepository.IdOfLastRealEstateInserted();

                ImgsRepository imgRep = new ImgsRepository();
                for (int i = 0; i < realEstate.countOfImgs; i++)
                {
                    Imgs imgs = new Imgs();
                    byte[] ImageBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        Imgs[i].CopyTo(memoryStream);
                        ImageBytes = memoryStream.ToArray();
                    }
                    imgs.RealEstateId = id;
                    imgs.Img = ImageBytes;
                    imgRep.Insert(imgs);
                }
                return RedirectToAction("GetRealEstate", "RealEstate", new { id = id });
            }
            else
            {
                return RedirectToAction("AddRealEstate");
            }
        }
        public IActionResult GetRealEstate(int id)
        {
            if(User?.Identity?.IsAuthenticated == false){
                return RedirectToAction("Login", "Home");
            }
            RealEstateReservationRepository reservRepo=new RealEstateReservationRepository();
            var reservation=reservRepo.GetByRealEstateId(id);
            RealEstateRepository realRep = new RealEstateRepository();
            var ownerId = realRep.GetOwnerIdByRealEstateId(id);
            var customerId = -1;
            if (reservation != null) {
                customerId = reservation.UserId;
            }
            int ? userReserveId = null;
            if(reservation!=null)
             userReserveId = reservation.UserId;
            PersonRepository personRepository = new PersonRepository();
            var account = User.FindFirstValue(ClaimTypes.Email);
            var person = personRepository.GetPersonByEmail(account);
            var personId = person.Id;
            UserRepository userRepository = new UserRepository();
            var user = userRepository.GetByPersonId(personId);
            var userId = user.Id;
            var realEstate = realRep.GetById(id);
            ImgsRepository imgsRep=new ImgsRepository();
            var imgs = imgsRep.GetImgs(id).ToList();
            GetRealEstateViewModel getRealEstateViewModel = new GetRealEstateViewModel();
            getRealEstateViewModel.Id = id;
            getRealEstateViewModel.userLoginId= userId;
            getRealEstateViewModel.userId = realEstate.UserId;
            getRealEstateViewModel.userReserveId= userReserveId;
            getRealEstateViewModel.Address = realEstate.Address;
            getRealEstateViewModel.Area=realEstate.Area;
            getRealEstateViewModel.Desc=realEstate.Desc;
            getRealEstateViewModel.Available=realEstate.Available;
            getRealEstateViewModel.Title=realEstate.Title;
            getRealEstateViewModel.Price=realEstate.Price;
            getRealEstateViewModel.MainImg=realEstate.MainImg;
            getRealEstateViewModel.Imgs = imgs;
            getRealEstateViewModel.ownerId = ownerId;
            getRealEstateViewModel.customerId = customerId;
            getRealEstateViewModel.Credintial = realEstate.Credentail;
            return View("GetRealEstate", getRealEstateViewModel);
        }
        public IActionResult SaveReverse(int id, GetRealEstateViewModel obj)
        {
            PersonRepository personRepository = new PersonRepository();
            var account = User.FindFirstValue(ClaimTypes.Email);
            var person = personRepository.GetPersonByEmail(account);
            UserRepository userRepository = new UserRepository();
            var user = userRepository.GetByPersonId(person.Id);
            var userId = user.Id;
            RealEstateReservations reservations = new RealEstateReservations();
            reservations.UserId = userId;
            reservations.RealEstateId = id;
            reservations.StartReservationDate= DateTime.Now;
            reservations.EndReservationDate = DateTime.Now.AddDays(obj.NumberOfDays);
            RealEstateReservationRepository reservationRepo= new RealEstateReservationRepository();
            reservationRepo.Insert(reservations);
            RealEstateRepository realEstateRepo= new RealEstateRepository();
            realEstateRepo.ChangeAvailabilty(id);
            return RedirectToAction("Bill","RealEstate", new { id = id });
        }
        public IActionResult GetMyPosts()
        {
            PersonRepository personRepository = new PersonRepository();
            var account = User.FindFirstValue(ClaimTypes.Email);
            var person = personRepository.GetPersonByEmail(account);
            UserRepository userRepository = new UserRepository();
            var user = userRepository.GetByPersonId(person.Id);
            var userId = user.Id;
            RealEstateRepository realRep=new RealEstateRepository();
            var realEstates=realRep.GetUserRealEstate(userId).ToList();
            return View("GetMyPosts",realEstates);
        }
        public IActionResult Bill(int id)
        {
            RealEstateReservationRepository reservationRepo = new RealEstateReservationRepository();
            var reservation =reservationRepo.GetByRealEstateId(id);
            var customerId = reservation.UserId;
            var startDate = reservation.StartReservationDate;
            var endDate = reservation.EndReservationDate;
            RealEstateRepository realRep= new RealEstateRepository();
            var ownerId = realRep.GetOwnerIdByRealEstateId(id);
            UserRepository userRep= new UserRepository();
            var personOwnerId = userRep.GetPersonId(ownerId);
            var personCustomerId = userRep.GetPersonId(customerId);
            PersonRepository personRep = new PersonRepository();
            var Owner=personRep.GetPersonById(personOwnerId);
            var Customer = personRep.GetPersonById(personCustomerId);
            RealEstateRepository RealRep=new RealEstateRepository();

            var realEstate = realRep.GetById(id);

            TimeSpan duration = endDate - startDate;
            int totalDays = (int)duration.TotalDays;
            decimal totalPrice = realEstate.Price * totalDays;

            BillViewModel billViewModel = new BillViewModel();
            billViewModel.CustomerEmail = Customer.Email;
            billViewModel.OwnerEmail= Owner.Email;
            billViewModel.CustomerName = Customer.FName + " " + Customer.LName;
            billViewModel.OwnerName=Owner.FName + " " + Owner.LName;
            billViewModel.RealEstateTitle = realEstate.Title;
            billViewModel.RealEstateDescription = realEstate.Desc;
            billViewModel.MainImg = realEstate.MainImg;
            billViewModel.StartDate = startDate;
            billViewModel.EndDate = endDate;
            billViewModel.totalPrice = totalPrice;

            return View("Bill",billViewModel);
        }
        public IActionResult GetMyReservation() 
        {
            PersonRepository personRepository = new PersonRepository();
            var account = User.FindFirstValue(ClaimTypes.Email);
            var person = personRepository.GetPersonByEmail(account);
            UserRepository userRepository = new UserRepository();
            var user = userRepository.GetByPersonId(person.Id);
            var userId = user.Id;
            RealEstateRepository realRep = new RealEstateRepository();
            var realEstates = realRep.GetCustomerReservations(userId).ToList();
            return View("GetMyPosts", realEstates);
        }
        public IActionResult Update(int id)
        {
            RealEstateRepository real=new RealEstateRepository();
            var realEsate=real.GetById(id);
            RealEstateViewModel realEstateViewModel = new RealEstateViewModel();
            realEstateViewModel.Title = realEsate.Title;
            realEstateViewModel.Address = realEsate.Address;
            realEstateViewModel.Area = realEsate.Area;
            realEstateViewModel.Desc = realEsate.Desc;
            realEstateViewModel.Price = realEsate.Price;
            realEstateViewModel.Category = realEsate.Category;
            realEstateViewModel.Id = id;
            return View("Update",realEstateViewModel);
        }
        public IActionResult SaveUpdating(RealEstateViewModel realEstate,int id, List<IFormFile> Imgs)
        {
            if (ModelState.IsValid)
            {
                ImgsRepository imgsRep = new ImgsRepository();
                imgsRep.DeleteAllById(realEstate.Id);
                byte[] MainImageBytes;
                byte[] CredentailImageBytes;
                using (var memoryStream = new MemoryStream())
                {
                    realEstate.MainImg.CopyTo(memoryStream);
                    MainImageBytes = memoryStream.ToArray();
                }
                using (var memoryStream = new MemoryStream())
                {
                    realEstate.Credentail.CopyTo(memoryStream);
                    CredentailImageBytes = memoryStream.ToArray();
                }
                PersonRepository personRepository = new PersonRepository();
                var account = User.FindFirstValue(ClaimTypes.Email);
                var person = personRepository.GetPersonByEmail(account);
                var personId = person.Id;
                UserRepository userRepository = new UserRepository();
                var user = userRepository.GetByPersonId(personId);
                var userId = user.Id;
                Models.RealEstate obj = new Models.RealEstate();
                obj.Title = realEstate.Title;
                obj.Address = realEstate.Address;
                obj.Area = realEstate.Area;
                obj.Desc = realEstate.Desc;
                obj.Price = realEstate.Price;
                obj.AdminId = 1;
                obj.UserId = userId;
                obj.MainImg = MainImageBytes;
                obj.Category = realEstate.Category;
                obj.Credentail = CredentailImageBytes;
                obj.Confirmed = false;
                RealEstateRepository realEstateRepository = new RealEstateRepository();
                realEstateRepository.Update(realEstate.Id,obj);
                var RealEstateId = id;

                ImgsRepository imgRep = new ImgsRepository();
                for (int i = 0; i < realEstate.countOfImgs; i++)
                {
                    Imgs imgs = new Imgs();
                    byte[] ImageBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        Imgs[i].CopyTo(memoryStream);
                        ImageBytes = memoryStream.ToArray();
                    }
                    imgs.RealEstateId = RealEstateId;
                    imgs.Img = ImageBytes;
                    imgRep.Insert(imgs);
                }
                return RedirectToAction("GetRealEstate", "RealEstate", new { id = id });
            }
            else
            {
                return RedirectToAction("Update");
            }
        }
        public IActionResult Delete(int id)
        {
            RealEstateRepository realRep=new RealEstateRepository();
            realRep.Delete(id);
            return RedirectToAction("GetMyPosts","RealEstate");
        }
        public IActionResult AdminDelete(int id)
        {
            RealEstateRepository realRep = new RealEstateRepository();
            realRep.Delete(id);
            return RedirectToAction("index", "Home");
        }
        public IActionResult GetWaitingPosts()
        {
            RealEstateRepository realRep = new RealEstateRepository();
            var realEstates = realRep.GetWaitingPosts().ToList();
            return View("GetWaitingPosts", realEstates);
        }
        public IActionResult WaitingPost(int id)
        {
            if (User?.Identity?.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Account");
            }
            RealEstateRepository realRep = new RealEstateRepository();
            var realEstate = realRep.GetById(id);
            ImgsRepository imgsRep = new ImgsRepository();
            var imgs = imgsRep.GetImgs(id).ToList();
            GetRealEstateViewModel getRealEstateViewModel = new GetRealEstateViewModel();
            getRealEstateViewModel.Id = id;
            getRealEstateViewModel.Address = realEstate.Address;
            getRealEstateViewModel.Area = realEstate.Area;
            getRealEstateViewModel.Desc = realEstate.Desc;
            getRealEstateViewModel.Available = realEstate.Available;
            getRealEstateViewModel.Title = realEstate.Title;
            getRealEstateViewModel.Price = realEstate.Price;
            getRealEstateViewModel.MainImg = realEstate.MainImg;
            getRealEstateViewModel.Imgs = imgs;
            getRealEstateViewModel.Credintial = realEstate.Credentail;
            getRealEstateViewModel.Confirmed = realEstate.Confirmed;
            return View("WaitingPost", getRealEstateViewModel);
        }
        public IActionResult Confirm(int id)
        {
            RealEstateRepository realRep = new RealEstateRepository();
            realRep.Confirm(id);
            return RedirectToAction("GetWaitingPosts", "RealEstate");
        }
    }
}