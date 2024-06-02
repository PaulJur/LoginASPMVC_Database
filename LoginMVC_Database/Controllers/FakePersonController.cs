using LoginMVC_Database.Data;
using LoginMVC_Database.Interfaces;
using LoginMVC_Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginMVC_Database.Controllers
{
    public class FakePersonController : Controller
    {

        private readonly ILoginMVCRepository _loginMVCRepository;

        public FakePersonController(ILoginMVCRepository loginMVCRepository)
        {
            _loginMVCRepository = loginMVCRepository;
        }
        public async Task<IActionResult> Data()
        {
            if (User.Identity.IsAuthenticated)//checks if the user is logged in
            {
                try
                {
                    var objPersonDataList = await _loginMVCRepository.GetAllFakePeople();
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(FakePersonData fakePerson)
        {
           /* if (_context.FakePeople.Any(n => n.LastName == obj.LastName && n.FirstName == obj.FirstName && n.Age == obj.Age)) //If First name and Last name and age are the same, give error
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
            */

            if (ModelState.IsValid)//if everything from FakePersonController.cs is validated like maxlenght and display order, then return valid and execute
            {
                _loginMVCRepository.AddFakePerson(fakePerson);//This will add the category object(made by the user input) to the fakeperson table. This keeps track of the changes
                TempData["success"] = "Person created succesfully"; //"success" is the key name
                return RedirectToAction("Data");//This will refresh the database table and show the new stuff
            }
            return View(fakePerson);//stays on the create category page
        }
        public async Task <IActionResult> Edit(int id)//This is a server side check
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var getPersonID = await _loginMVCRepository.GetIdAsync(id); //will work off the primary key off the model

            if (getPersonID == null)
            {
                return NotFound();
            }
            return View(getPersonID);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(FakePersonData fakePerson)//This is post http, meaning what the server receives from the user
        {

            if (ModelState.IsValid)//if everything from FakePersonData.cs is validated like maxlenght and Id, then return valid and execute
            {
                _loginMVCRepository.UpdateFakePerson(fakePerson);//This will Update the category object(made by the user input) to the category table. This keeps track of the changes
                TempData["success"] = "Category updated succesfully";
                return RedirectToAction("Data");//This will refresh the database table and show the new stuff
            }
            return View(fakePerson);//stays on the create category page
        }

        public async Task<IActionResult> Delete(int id)//This is a server side check
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var getPersonID = await _loginMVCRepository.GetIdAsync(id); //will work off the primary key off the model

            if (getPersonID == null)
            {
                return NotFound();
            }
            return View(getPersonID);
        }

        [HttpPost, ActionName("Delete")]//this returns the input from the user side to the back-end and the actionName is "Delete"
        //send data to a server to create/update a resource
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePOST(int id)
        {
            var getPersonID = await _loginMVCRepository.GetIdAsync(id);

            if(getPersonID == null)
            {
                return NotFound();
            }
            _loginMVCRepository.DeleteFakePerson(id);
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Data");
        }
    }

}
