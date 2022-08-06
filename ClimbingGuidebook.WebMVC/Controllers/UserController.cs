using ClimbingGuidebook.Models.UserModels;
using ClimbingGuidebook.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClimbingGuidebook.WebMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null)
                return false;
            _userService.SetUserId(userId);
            return true;
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            if (userIdClaim == null) return default;
            return Guid.Parse(userIdClaim);
        }

        public ActionResult Index()
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _userService.GetUsers();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(UserCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            _userService.CreateUser(model);

            if (ModelState.IsValid)
            {
                TempData["SaveResult"] = "User was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "User could not be created");

            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _userService.GetUserById(id);

            return View(model);
        }

        public ActionResult Edit(Guid id)
        {

            if (!SetUserIdInService()) return Unauthorized();

            var detail = _userService.GetUserById(id);
            var model = new UserEdit
            {

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, UserEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (!ModelState.IsValid) return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (_userService.UpdateUser(model))
            {
                TempData["SaveResult"] = "Your User was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your User could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _userService.GetUserById(id);

            return View(model);
        }

    }
}
