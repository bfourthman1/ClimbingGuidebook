using ClimbingGuidebook.Models.AscentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Services.AscentServices
{
    public interface IAscentService
    {
        public IEnumerable<AscentListItem> GetAscents();
        public bool CreateAscent(AscentCreate model);
        public AscentDetail GetAscentById(int id);
        public bool UpdateAscent(AscentEdit model);
        public bool DeleteAscent(int id);
        public void SetUserId(Guid userId);

    }
}
