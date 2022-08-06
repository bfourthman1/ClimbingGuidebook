﻿using ClimbingGuidebook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingGuidebook.Models.UserModels
{
    public class UserListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }
        public Gender Sex { get; set; }
    }
}
