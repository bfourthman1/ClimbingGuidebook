using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Data
{
    public enum Difficulty { _6, _7, _8, _9, _10a, _10b, _10c, _10d, _11a, _11b, _11c, _11d, _12a, 
        _12b, _12c, _12d, _13a, _13b, _13c, _13d, _14a, _14b, _14c, _14d, _15a, _15b, _15c, _15d } 
    public class Route
    {
        [Key]
        public int RouteId { get; set; }
        [ForeignKey("AspNetUsers")]
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Difficulty Grade{ get; set; }
        [Range (0, 4)]
        public int Rating { get; set; }
        public DateTimeOffset FirstAscent { get; set; }
        public virtual ICollection<Ascent> Ascents { get; set; } = new List<Ascent> ();
    }
}
