using ClimbingGuidebook.Models.BoulderModels;
using ClimbingGuidebook.Services.BoulderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClimbingGuidebook.WebMVC.Controllers
{
    [Authorize]
    public class BoulderController : Controller
    {
        private readonly IBoulderService _boulderService;

        public BoulderController(IBoulderService boulderService)
        {
            _boulderService = boulderService;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null)
                return false;
            _boulderService.SetUserId(userId);
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

            var model = _boulderService.GetBoulders();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BoulderCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            //_boulderService.CreateBoulder(model);

            if (_boulderService.CreateBoulder(model))
            {
                TempData["SaveResult"] = "Your boulder was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Boulder could not be created");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _boulderService.GetBoulderById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {

            if (!SetUserIdInService()) return Unauthorized();

            var detail = _boulderService.GetBoulderById(id);
            var model = new BoulderEdit
            {

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BoulderEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (!ModelState.IsValid) return View(model);

            if (model.BoulderId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (_boulderService.UpdateBoulder(model))
            {
                TempData["SaveResult"] = "Your boulder was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your boulder could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _boulderService.GetBoulderById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            _boulderService.DeleteBoulder(id);

            TempData["SaveResult"] = "Your route was deleted";

            return RedirectToAction("Index");
        }
    }
}
