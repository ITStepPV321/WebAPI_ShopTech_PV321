
namespace WebAPI_ShopTech_PV321.Core.DTOs
{
    public class CreateProductDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImagePath { get; set; }
		//public IFormFile? Image { get; set; }
		public int CategoryId { get; set; }
        
    }
}
