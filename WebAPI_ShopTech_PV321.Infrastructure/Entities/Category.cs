﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_ShopTech_PV321.Infrastructure.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        //public ICollection<Product> Products { get; set; }
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
