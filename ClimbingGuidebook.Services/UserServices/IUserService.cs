using ClimbingGuidebook.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Services.UserServices
{
    public interface IUserService
    {
        public bool UpdateUser(UserEdit model);
        public UserDetail GetUserById(Guid id);
        public IEnumerable<UserListItem> GetUsers();
        public bool CreateUser(UserCreate model);
        public bool DeleteUser(Guid id);
        void SetUserId(Guid userId);
    }
}
