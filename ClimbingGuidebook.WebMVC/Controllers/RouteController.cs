using ClimbingGuidebook.Models.RouteModels;
using ClimbingGuidebook.Services.RouteServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClimbingGuidebook.WebMVC.Controllers
{
    [Authorize]
    public class RouteController : Controller
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null)
                return false;
            _routeService.SetUserId(userId);
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

            var model = _routeService.GetRoutes();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RouteCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            //_routeService.CreateRoute(model);

            if (_routeService.CreateRoute(model))
            {
                TempData["SaveResult"] = "Your route was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Route could not be created");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _routeService.GetRouteById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {

            if (!SetUserIdInService()) return Unauthorized();

            var detail = _routeService.GetRouteById(id);
            var model = new RouteEdit
            {
                
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RouteEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (!ModelState.IsValid) return View(model);

            if (model.RouteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (_routeService.UpdateRoute(model))
            {
                TempData["SaveResult"] = "Your route was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your route could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _routeService.GetRouteById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            _routeService.DeleteRoute(id);

            TempData["SaveResult"] = "Your route was deleted";

            return RedirectToAction("Index");
        }

    }
}
