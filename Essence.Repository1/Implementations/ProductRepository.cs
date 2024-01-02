using Essence.Data.DTO.Product;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Microsoft.EntityFrameworkCore;
using Slugify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task AddColors(Product product, List<int> colorIds)
        {
            product.ProductColors = new List<ProductColor>();
            List<int> productColorIds = product.ProductColors.Select(x => x.ColorId).ToList();
            List<int> addTagIds = colorIds.FindAll(x => !productColorIds.Contains(x));
            List<int> removeTagIds = productColorIds.FindAll(x => !colorIds.Contains(x));
            product.ProductColors.RemoveAll(x => removeTagIds.Contains(x.ColorId));

            foreach (int colorId in colorIds)
            {
                product.ProductColors.Add(new ProductColor
                {
                    ColorId = colorId,
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task AddSizes(ProductColor productColor, List<int> sizeIds)
        {
            productColor.ProductColorSizes = new List<ProductColorSize>();
            List<int> productColorIds = productColor.ProductColorSizes.Select(x => x.SizeId).ToList();
            List<int> addTagIds = sizeIds.FindAll(x => !productColorIds.Contains(x));
            List<int> removeTagIds = productColorIds.FindAll(x => !sizeIds.Contains(x));
            productColor.ProductColorSizes.RemoveAll(x => removeTagIds.Contains(x.SizeId));

            foreach (int sizeId in sizeIds)
            {
                productColor.ProductColorSizes.Add(new ProductColorSize
                {
                   SizeId = sizeId,
                });
            }
        }

        public async Task<List<Product>> GetAllProductWithDetails()
        {
            return await _context.Products
                .Where(x => x.DeletedAt == null)
                .Include(x => x.SubCategory)
                .Include(x => x.TopCategory)
                .Include(x => x.Brand)
                .Include(x => x.ProductColors)
                .ThenInclude(x => x.Color)
                .Include(x => x.ProductColors)
                .ThenInclude(x => x.ProductColorSizes)
                .ThenInclude(x => x.Size)
                .Include(x => x.ProductImages)
                .ToListAsync();
        }

        public async Task<ProductColor> getProductColor(int productId, int colorId)
        {
            return await _context.ProductColors
                .Include(x=>x.Product)
                .Include(x=>x.Color)
                .Include(x=>x.ProductColorSizes)
                .ThenInclude(x=>x.Size)
                .FirstOrDefaultAsync(x=>x.ProductId == productId && x.ColorId == colorId);
        }

        public async Task<Product> GetProductDetails(int id)
        {
            return await _context.Products
                .Include(x => x.SubCategory)
                .Include(x => x.TopCategory)
                .Include(x => x.Brand)
                .Include(x => x.ProductColors)
                .ThenInclude(x => x.Color)
                .Include(x => x.ProductColors)
                .ThenInclude(x => x.ProductColorSizes)
                .ThenInclude(x => x.Size)
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProductColorSize> getProductColorSize(int id)
        {
            return await _context.ProductColorSizes
                .Include(x=>x.ProductColor)
                .ThenInclude(x=>x.Product)
                .ThenInclude(x=>x.ProductImages)
                .Include(x=>x.ProductColor)
                .ThenInclude(x=>x.Color)
                .Include(x=>x.Size)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Product product, ProductPutDTO model)
        {
            SlugHelper helper = new SlugHelper();
            product.SubCategoryId = model.SubCategoryId;
            product.TopCategoryId = model.TopCategoryId;
            product.BrandId = model.BrandId;
            product.Name = model.Name;
            product.Slug = helper.GenerateSlug(model.Name);
            product.Description = model.Description;
            await _context.SaveChangesAsync();
        }

        public void UpdateProductSize(ProductColorSize productColorSize, ProductColorSizeDTO model)
        {
            productColorSize.Count = model.Count;
        }

        public async Task DeleteProductImages(Product product)
        {
            _context.ProductImages.RemoveRange(product.ProductImages);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductBySlug(string slug)
        {
            return await _context.Products
                .Where(x => x.DeletedAt == null)
                .Include(x => x.SubCategory)
                .Include(x => x.TopCategory)
                .Include(x => x.Brand)
                .Include(x => x.ProductColors)
                .ThenInclude(x => x.Color)
                .Include(x => x.ProductColors)
                .ThenInclude(x => x.ProductColorSizes)
                .ThenInclude(x => x.Size)
                .Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Slug == slug);
        }
    }
}
