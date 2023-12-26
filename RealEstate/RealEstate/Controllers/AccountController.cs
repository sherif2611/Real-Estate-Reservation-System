using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.Repository;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using RealEstate.ViewModels;
using Microsoft.AspNetCore.Authorization;
namespace RealEstate.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> SavedLogin(Person person)
        {
            PersonRepository personRep=new PersonRepository();
            var _person =personRep.GetPersonByEmail(person.Email);
            if (_person != null)
            {
                if (person.Password == _person.Password)
                {
                    var ClaimsIdentity = new ClaimsIdentity("MyCookie");
                    ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Email, _person.Email, ClaimValueTypes.String));
                    var role = _person.Role; // Assuming Role is the property in Person entity
                    ClaimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role, ClaimValueTypes.String));
                    var princiaple = new ClaimsPrincipal(ClaimsIdentity);
                    Thread.CurrentPrincipal = princiaple;
                    await HttpContext.SignInAsync("MyCookie", princiaple);
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public IActionResult SignUp() 
        { 
            return View();
        }
        public IActionResult SaveSignUp(UserViewModel person)
        {
            if(ModelState.IsValid)
            {
                var _person=new Person();
                _person.Email = person.Email;
                _person.FName = person.FName;
                _person.LName=person.LName;
                _person.Password = person.Password;
                _person.Role = "user";
                var personRep=new PersonRepository();
                personRep.Insert(_person);
                User user=new User();
                user.PersonId = _person.Id;
                UserRepository userRep= new UserRepository();
                userRep.Insert(user);
                return View("Login");
            }
            else
            {
                return RedirectToAction("SignUp",person);
            }
        }
        public async Task<RedirectToActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index", "Home");
        }
    }
}