using ClimbingGuidebook.Models.RouteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Services.RouteServices
{
    public interface IRouteService
    {
        public IEnumerable<RouteListItem> GetRoutes();
        public bool CreateRoute(RouteCreate model);
        public RouteDetail GetRouteById(int id);
        public bool UpdateRoute(RouteEdit model);
        public bool DeleteRoute(int id);
        void SetUserId(Guid userId);
    }
}
