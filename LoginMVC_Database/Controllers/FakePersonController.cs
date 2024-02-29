using LoginMVC_Database.Data;
using LoginMVC_Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginMVC_Database.Controllers
{
    public class FakePersonController : Controller
    {

        private readonly LoginMVC_DbContext _context;

        public FakePersonController(LoginMVC_DbContext context)
        {
            _context = context;
        }
        public IActionResult Data()
        {
            List<FakePersonData> objPersonDataList = _context.FakePeople.ToList();
            if (User.Identity.IsAuthenticated && objPersonDataList != null)
            {
                try
                {
                    
                    return View(objPersonDataList);
                }
                catch (Exception ex)
                {
                    return View(new List<FakePersonData>());
                }
            }

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(FakePersonData obj)
        {
            if (_context.FakePeople.Any(n => n.LastName == obj.LastName && n.FirstName == obj.FirstName && n.Age == obj.Age)) //If First name and Last name are the same, give error
            {
                ModelState.AddModelError("name", "Cannot have an existing person added into the database");
            }
            if (obj.LastName.Any(char.IsDigit))
            {
                ModelState.AddModelError("LastName", "Last Name cannot contain numbers");
            }
            if (obj.FirstName.Any(char.IsDigit))
            {
                ModelState.AddModelError("FirstName", "First Name cannot contain numbers");
            }

            if (ModelState.IsValid)//if everything from FakePersonController.cs is validated like maxlenght and display order, then return valid and execute
            {
                _context.FakePeople.Add(obj);//This will add the category object(made by the user input) to the category table. This keeps track of the changes
                _context.SaveChanges();//this makes the changes in the database table
                TempData["success"] = "Person created succesfully"; //"success" is the key name
                return RedirectToAction("Data");//This will refresh the database table and show the new stuff
            }
            return View(obj);//stays on the create category page
        }
    }
}
