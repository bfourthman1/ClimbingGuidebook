using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Data
{
    public class Boulder
    {
        [Key]
        public int BoulderId { get; set; }
        [ForeignKey("AspNetUsers")]
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        [Range(0, 17)]
        public int Difficulty { get; set; }
        [Range(0, 4)]
        public int Rating { get; set; }
        public DateTimeOffset FirstAscent { get; set; }
        public virtual ICollection<Ascent> Ascents { get; set; } = new List<Ascent>();
    }
}
