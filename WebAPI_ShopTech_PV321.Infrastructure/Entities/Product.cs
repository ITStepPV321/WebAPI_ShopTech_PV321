using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_ShopTech_PV321.Infrastructure.Entities
{
    public class Product
    {
            [Key]
            public int Id { get; set; }
            public string? Title { get; set; }

             public string? Description { get; set; }

           
            public decimal Price { get; set; } = 0;
                    public string? ImagePath { get; set; }

        
            [ForeignKey("Category")]
            public int CategoryId { get; set; }
            //navigagion property
            public Category? Category { get; set; }
        }

    
}
