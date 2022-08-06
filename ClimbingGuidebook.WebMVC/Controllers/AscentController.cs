using ClimbingGuidebook.Models.AscentModels;
using ClimbingGuidebook.Services.AscentServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClimbingGuidebook.WebMVC.Controllers
{
    public class AscentController : Controller
    {
        private readonly IAscentService _ascentService;

        public AscentController(IAscentService ascentService)
        {
            _ascentService = ascentService;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null)
                return false;
            _ascentService.SetUserId(userId);
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

            var model = _ascentService.GetAscents();

            return View(model);
        }

        [ActionName("Create")]
        public ActionResult Create(AscentCreate? model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAscent(AscentCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            //_routeService.CreateRoute(model);

            if (_ascentService.CreateAscent(model))
            {
                TempData["SaveResult"] = "Your ascent was logged.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Ascent could not be logged");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _ascentService.GetAscentById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {

            if (!SetUserIdInService()) return Unauthorized();

            var detail = _ascentService.GetAscentById(id);
            var model = new AscentEdit
            {


            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AscentEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (!ModelState.IsValid) return View(model);

            if (model.AscentId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (_ascentService.UpdateAscent(model))
            {
                TempData["SaveResult"] = "Your ascent was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your ascent could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _ascentService.GetAscentById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            _ascentService.DeleteAscent(id);

            TempData["SaveResult"] = "Your ascent was deleted";

            return RedirectToAction("Index");
        }
    }
}
