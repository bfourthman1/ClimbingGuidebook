using ClimbingGuidebook.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Models.BoulderModels
{
    public class BoulderDetail
    {
        public int BoulderId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Difficulty { get; set; }
        [Range(0, 4)]
        public int Rating { get; set; }
        public DateTimeOffset FirstAscent { get; set; }
    }
}
