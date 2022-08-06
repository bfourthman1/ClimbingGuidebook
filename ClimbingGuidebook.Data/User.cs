using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Data
{
    public enum Gender { M, F }
    public class User
    {
        
        [Key, ForeignKey("AspNetUsers")]
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }
        public Gender Sex { get; set; }  
        public virtual ICollection<Route> Routes { get; set; } = new List<Route>();
        public virtual ICollection<Boulder> Boulders { get; set; } = new List<Boulder>();
        public virtual ICollection<Ascent> Ascents { get; set; } = new List<Ascent>();
    }
}
