using AutoMapper;

using Microsoft.EntityFrameworkCore;
using WebAPI_ShopTech_PV321.Core.DTOs;
using WebAPI_ShopTech_PV321.Core.Interfaces;
using WebAPI_ShopTech_PV321.Infrastructure.Data;
using WebAPI_ShopTech_PV321.Infrastructure.Entities;

namespace WebAPI_ShopTech_PV321.Core.Sevices
{
    public class ProductsService : IProductsService
    {
        private readonly ShopTechAPI_PV321 _context;
        private readonly IMapper _mapper;
        
        public ProductsService(ShopTechAPI_PV321 context,
                                IMapper mapper
                               )
        {
            _context=context;
            _mapper=mapper;
        
        }
        public void Create(CreateProductDto productDto)
        {
			
			//};
			// save image and get file path!!!!
			//if (productDto.Image != null)
			//	productDto.ImagePath = _fileService.SaveProductImage(productDto.Image).Result;
			// 2 - using auto mapper
			var product =_mapper.Map<Product>(productDto);   // ProductDto=>Product(Entity)
            _context.Products.Add(product);
            _context.SaveChanges(true);
        }

        public void Delete(int? id)
        {
            var productDto = GetById(id);
            if (productDto != null)
            { 
				//if (productDto.ImagePath != null)
				//	_fileService.DeleteProductImage(productDto.ImagePath);
				var product= _mapper.Map<Product>(productDto);
                //_products.Remove(product);
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public void Edit(ProductDto productDto)
        {
            var productOld = GetById(productDto.Id); 
            if (productOld != null)
            {
				
				// save image and get file path
				//if (productDto.Image != null)
				//	productDto.ImagePath = _fileService.EditProductImage(productDto.ImagePath, productDto.Image).Result;

				var product = _mapper.Map<Product>(productDto );
                _context.Products.Update(product);
                _context.SaveChanges();
            }
        }

        public List<ProductDto> GetAll()
        {
            var products= _context.Products.Include(p=>p.Category).ToList();
           
            return _mapper.Map<List<ProductDto>>(products);
        }

        public List<CategoryDto> GetAllCategories()
        {var categories= _context.Categories.ToList();
            return categories.Select(c => new CategoryDto()
            {    Id=c.Id,
                Name  = c.Name,    
                Description = c.Description,

            }).ToList();
        }

        public ProductDto? GetById(int? id)
        {
            Product? product = _context.Products.FirstOrDefault(product => product.Id == id);
            if (product == null) { 
            return null;
            }

           
            return _mapper.Map<ProductDto>(product);
        }

        public ProductDto GetEditProduct(int? id)
        {
            var productDto = GetById(id);
            if (productDto == null)
            {
                return null;
            }
            return productDto;
        }
    }
}
