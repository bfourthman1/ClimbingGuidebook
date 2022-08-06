using ClimbingGuidebook.Data;
using ClimbingGuidebook.Models.BoulderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Services.BoulderServices
{
    public class BoulderService : IBoulderService
    {
        private readonly ApplicationDbContext _ctx;
        private Guid _userId;

        public BoulderService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool CreateBoulder(BoulderCreate model)
        {
            var entity =
                new Boulder()
                {
                    Name = model.Name,
                    Location = model.Location,
                    Difficulty = model.Difficulty,
                    Rating = model.Rating,
                    FirstAscent = DateTimeOffset.Now,
                    OwnerId = _userId,
                };

            _ctx.Boulders.Add(entity);
            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<BoulderListItem> GetBoulders()
        {
            var query =
                _ctx
                .Boulders/*.Where(e => e.OwnerId == _userId)*/
                .Select(
                    e =>
                       new BoulderListItem
                       {
                           BoulderId = e.BoulderId,
                           UserId = _userId,
                           Name = e.Name,
                           Location = e.Location,
                           Difficulty = e.Difficulty,
                           Rating = e.Rating,
                           FirstAscent = e.FirstAscent,
                       }
                   );
            return query.ToArray();
        }

        public BoulderDetail GetBoulderById(int id)
        {
            var entity = _ctx
                 .Boulders
                 .Single(e => e.BoulderId == id /*&& e.OwnerId == _userId*/);
            return
                new BoulderDetail
                {
                    BoulderId = entity.BoulderId,
                    UserId = entity.OwnerId,
                    Name = entity.Name,
                    Location = entity.Location,
                    Difficulty = entity.Difficulty,
                    Rating = entity.Rating,
                    FirstAscent = entity.FirstAscent,
                };
        }

        public bool UpdateBoulder(BoulderEdit model)
        {
            {
                var entity =
                    _ctx
                        .Boulders
                        .Single(e => e.BoulderId == model.BoulderId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Location = model.Location;
                entity.Difficulty = model.Difficulty;
                entity.Rating = model.Rating;
                entity.FirstAscent = DateTimeOffset.UtcNow;

                return _ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBoulder(int id)
        {
            var entity =
                _ctx
                    .Boulders
                    .Single(e => e.BoulderId == id && e.OwnerId == _userId);

            _ctx.Boulders.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }


        public void SetUserId(Guid userId) => _userId = userId;
    }
}
