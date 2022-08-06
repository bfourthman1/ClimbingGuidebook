using ClimbingGuidebook.Data;
using ClimbingGuidebook.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Services.UserServices
{
    public class UserService : IUserService
    {

        private Guid _userId;
        private readonly ApplicationDbContext _ctx;
        public UserService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public bool CreateUser(UserCreate model)
        {
            var entity =
                new User()
                {
                   Name = model.Name,
                   Location = model.Location,
                   Age = model.Age,
                   Sex = model.Sex,
                };


            _ctx.Users.Add(entity);
            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<UserListItem> GetUsers()
        {
            var query =
                _ctx
                .Users.Where(e => e.OwnerId == _userId)
                .Select(
                    e =>
                       new UserListItem
                       {
                          Name = e.Name,
                          Location = e.Location,
                          Age = e.Age,
                          Sex = e.Sex,
                       }
                   );
            return query.ToArray();
        }


        //public UserDetail GetUserById(int id) 
        public UserDetail GetUserById(Guid id)
        {
            var entity = _ctx
                 .Users
                 //.Single(e => e.Id == id && e.OwnerId == _userId);
                 .Single(e => e.OwnerId == id);
            return
                new UserDetail
                {
                   Name = entity.Name,
                   Location = entity.Location,
                   Age = entity.Age,
                   Sex = entity.Sex,
                };
        }

        public bool UpdateUser(UserEdit model)
        {
            {
                var entity =
                    _ctx
                        .Users
                        //.Single(e => e.Id == model.Id && e.OwnerId == _userId); 
                        .Single(e => e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Location = model.Location;
                entity.Age = model.Age;
                entity.Sex = model.Sex;

                return _ctx.SaveChanges() == 1;
            }
        }
        //public bool DeleteUser(int id)
        public bool DeleteUser(Guid id)
        {
            var entity =
                _ctx
                    .Users
                    //.Single(e => e.Id == id && e.OwnerId == _userId);
                    .Single(e => e.OwnerId == _userId);

            _ctx.Users.Remove(entity);
             
            return _ctx.SaveChanges() == 1;
        }
        public void SetUserId(Guid userId) => _userId = userId;

    }
}
