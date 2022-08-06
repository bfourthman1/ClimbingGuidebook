using ClimbingGuidebook.Data;
using ClimbingGuidebook.Models.RouteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Services.RouteServices
{
    public class RouteService : IRouteService
    {
        private readonly ApplicationDbContext _ctx;
        private Guid _userId;

        public RouteService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool CreateRoute(RouteCreate model)
        {
            var entity =
                new Route()
                {
                    Name = model.Name,
                    Location = model.Location,
                    Grade = model.Grade,
                    Rating = model.Rating,
                    FirstAscent = DateTimeOffset.Now,
                    OwnerId = _userId,
                };

            _ctx.Routes.Add(entity);
            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<RouteListItem> GetRoutes()
        {
            var query =
                _ctx
                .Routes/*.Where(e => e.OwnerId == _userId)*/
                .Select(
                    e =>
                       new RouteListItem
                       {
                           RouteId = e.RouteId,
                           UserId = e.OwnerId,
                           Name = e.Name,
                           Location = e.Location,
                           Grade = e.Grade,
                           Rating = e.Rating,
                           FirstAscent = e.FirstAscent,
                       }
                   );
            return query.ToArray();
        }

        public RouteDetail GetRouteById(int id)
        {
            var entity = _ctx
                 .Routes
                 .Single(e => e.RouteId == id /*&& e.OwnerId == _userId*/);
            return
                new RouteDetail
                {
                    RouteId = entity.RouteId,
                    UserId = entity.OwnerId,
                    Name = entity.Name,
                    Location = entity.Location,
                    Grade = entity.Grade,
                    Rating = entity.Rating,
                    FirstAscent = entity.FirstAscent,
                };
        }

        public bool UpdateRoute(RouteEdit model)
        {
            {
                var entity =
                    _ctx
                        .Routes
                        .Single(e => e.RouteId == model.RouteId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Location = model.Location;
                entity.Grade = model.Grade;
                entity.Rating = model.Rating;
                entity.FirstAscent = DateTimeOffset.UtcNow;

                return _ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteRoute(int id)
        {
            var entity =
                _ctx
                    .Routes
                    .Single(e => e.RouteId == id && e.OwnerId == _userId);

            _ctx.Routes.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }


        public void SetUserId(Guid userId) => _userId = userId;
    }
}
