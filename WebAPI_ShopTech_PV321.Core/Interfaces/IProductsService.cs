using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_ShopTech_PV321.Core.DTOs;

namespace WebAPI_ShopTech_PV321.Core.Interfaces
{
    public interface IProductsService
    {
        //functional 
        List<CategoryDto> GetAllCategories();
        List<ProductDto> GetAll();
        ProductDto GetById(int? id);
        ProductDto GetEditProduct(int? id);
        void Create(CreateProductDto productDto);
        void Edit(ProductDto productDto);
        void Delete(int? id);
    }
}
