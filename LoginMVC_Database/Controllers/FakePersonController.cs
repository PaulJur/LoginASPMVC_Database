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

        [HttpGet]
        public IActionResult Data()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    List<FakePersonData> objPersonDataList = _context.FakePeople.ToList();
                    return View(objPersonDataList);
                }
                catch (Exception ex)
                {
                    return View(new List<FakePersonData>());
                }
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
