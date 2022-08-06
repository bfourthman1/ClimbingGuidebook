using ClimbingGuidebook.Models.BoulderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Services.BoulderServices
{
    public interface IBoulderService
    {
        public IEnumerable<BoulderListItem> GetBoulders();
        public bool CreateBoulder(BoulderCreate model);
        void SetUserId(Guid userId);
        public BoulderDetail GetBoulderById(int id);
        public bool DeleteBoulder(int id);
        public bool UpdateBoulder(BoulderEdit model);
    }
}
