using LoginMVC_Database.Data;
using LoginMVC_Database.Models;
using Microsoft.AspNetCore.Authorization;
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
            else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        [Authorize("Admin")]
        public IActionResult Create(FakePersonData obj)
        {
            if (_context.FakePeople.Any(n => n.LastName == obj.LastName && n.FirstName == obj.FirstName && n.Age == obj.Age)) //If First name and Last name and age are the same, give error
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
        public IActionResult Edit(int? id)//This is a server side check
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            FakePersonData? FakePersonFromDb = _context.FakePeople.Find(id);//will work off the primary key off the model

            if (FakePersonFromDb == null)
            {
                return NotFound();
            }
            return View(FakePersonFromDb);
        }

        [HttpPost]

        public IActionResult Edit(FakePersonData obj)//This is post http, meaning what the server receives from the user
        {

            if (ModelState.IsValid)//if everything from FakePersonData.cs is validated like maxlenght and Id, then return valid and execute
            {
                _context.FakePeople.Update(obj);//This will Update the category object(made by the user input) to the category table. This keeps track of the changes
                _context.SaveChanges();//this makes the changes in the database table
                TempData["success"] = "Category updated succesfully";
                return RedirectToAction("Data");//This will refresh the database table and show the new stuff
            }
            return View(obj);//stays on the create category page
        }

        public IActionResult Delete(int? id)//This is a server side check
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            FakePersonData? FakePersonFromDb = _context.FakePeople.Find(id);//will work off the primary key off the model

            if (FakePersonFromDb == null)
            {
                return NotFound();
            }
            return View(FakePersonFromDb);
        }

        [HttpPost, ActionName("Delete")]//this returns the input from the user side to the back-end and the actionName is "Delete"
        //send data to a server to create/update a resource

        public IActionResult DeletePOST(int? id)
        {
            FakePersonData? obj = _context.FakePeople.Find(id);

            if(obj == null)
            {
                return NotFound();
            }
            _context.FakePeople.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Data");
        }
    }

}
