using Essence.Data.DTO.Product;
using Essence.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Contracts
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task Update(Product product, ProductPutDTO model);
        public Task<List<Product>> GetAllProductWithDetails();
        public Task<Product> GetProductDetails(int id);
        public Task<Product> GetProductBySlug(string slug);
        public Task AddColors(Product product, List<int> colorIds);
        public Task AddSizes(ProductColor productColor, List<int> sizeIds);
        public Task<ProductColor> getProductColor(int productId,int colorId);
        public Task<ProductColorSize> getProductColorSize(int id);
        public void UpdateProductSize(ProductColorSize productColorSize, ProductColorSizeDTO model);
        public Task DeleteProductImages(Product product);
    }
}
