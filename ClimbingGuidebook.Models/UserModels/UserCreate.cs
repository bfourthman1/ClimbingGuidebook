using ClimbingGuidebook.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Models.UserModels
{
    public class UserCreate
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public Gender Sex { get; set; }
    }
}
