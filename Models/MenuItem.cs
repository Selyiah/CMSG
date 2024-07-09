﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSGTechnical.Domain.Interfaces;

namespace CMSGTechnical.Domain.Models
{
    public class MenuItem : IEntity
    {
        public int Id { get; set; }
        public int Order { get; set; } = 0;
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }  // New property for category
        public ICollection<MenuItem> ChildItems { get; set; } = new List<MenuItem>();
    }
}

