namespace WebAPI_ShopTech_PV321.Core.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } = 0;
        public string? ImagePath { get; set; }
		//public IFormFile? Image { get; set; }
		public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
