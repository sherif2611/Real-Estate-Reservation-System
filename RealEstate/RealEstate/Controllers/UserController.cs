using Microsoft.AspNetCore.Mvc;
using RealEstate.Repository;

namespace RealEstate.Controllers
{
    public class UserController : Controller
    {
        public IActionResult GetAllUsers()
        {
            PersonRepository personRepository = new PersonRepository();
            var users=personRepository.GetAllUsersInfo();
            return View("GetAllUsers",users);
        }
        public IActionResult Delete(int id)
        {
            UserRepository userRepository = new UserRepository();
            var user=userRepository.GetByPersonId(id);
            userRepository.Delete(user.Id);
            return RedirectToAction("GetAllUsers");
        }
    }
}
