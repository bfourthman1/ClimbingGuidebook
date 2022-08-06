using ClimbingGuidebook.Data;
using ClimbingGuidebook.Models.AscentModels;
using ClimbingGuidebook.Models.RouteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Services.AscentServices
{
    public class AscentService : IAscentService
    {
        private readonly ApplicationDbContext _ctx;
        private Guid _userId;

        public AscentService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool CreateAscent(AscentCreate model)
        {
            var entity =
                new Ascent()
                {
                    Name = model.Name,
                    Location = model.Location,
                    Grade = model.Grade,
                    Rating = model.Rating,
                    FirstAscent = DateTimeOffset.Now,
                    OwnerId = _userId,
                };

            _ctx.Ascents.Add(entity);
            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<AscentListItem> GetAscents()
        {
            var query =
                _ctx
                .Ascents/*.Where(e => e.OwnerId == _userId)*/
                .Select(
                    e =>
                       new AscentListItem
                       {
                           AscentId = e.AscentId,
                           UserId = e.OwnerId,
                           Name = e.Name,
                           Location = e.Location,
                           Grade = e.Grade,
                           Rating = e.Rating,
                           FirstAscent = e.FirstAscent,
                       }
                   );
            return query;
        }

        public AscentDetail GetAscentById(int id)
        {
            var entity = _ctx
                 .Ascents
                 .Single(e => e.AscentId == id /*&& e.OwnerId == _userId*/);
            return
                new AscentDetail
                {
                    AscentId = entity.AscentId,
                    UserId = entity.OwnerId,
                    Name = entity.Name,
                    Location = entity.Location,
                    Grade = entity.Grade,
                    Rating = entity.Rating,
                    FirstAscent = entity.FirstAscent,
                };
        }

        public bool UpdateAscent(AscentEdit model)
        {
            {
                var entity =
                    _ctx
                        .Ascents
                        .Single(e => e.AscentId == model.AscentId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Location = model.Location;
                entity.Grade = model.Grade;
                entity.Rating = model.Rating;
                entity.FirstAscent = DateTimeOffset.UtcNow;

                return _ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAscent(int id)
        {
            var entity =
                _ctx
                    .Ascents
                    .Single(e => e.AscentId == id && e.OwnerId == _userId);

            _ctx.Ascents.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }


        public void SetUserId(Guid userId) => _userId = userId;
    }
}
