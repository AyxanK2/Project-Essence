using AutoMapper;
using Essence.Data.DTO;
using Essence.Data.DTO.Color;
using Essence.Data.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Size = Essence.Data.Models.Size;
using Color = Essence.Data.Models.Color;
using Essence.Data.DTO.Category;
using Essence.Data.DTO.Product;

namespace Essence.Data.Mappers
{
    public class MapConfig :Profile
    {
        public MapConfig()
        {
            CreateMap<Size, SizeGetDTO>().ReverseMap();
            CreateMap<SizePostDTO, Size>();

            CreateMap<Color, ColorGetDTO>().ReverseMap();
            CreateMap<ColorPostDTO, Color>();

            CreateMap<Category, CategoryGetDTO>()
                .ForMember(x => x.TopCategory, dest => dest.MapFrom(x => x.TopCategory != null ? x.TopCategory.Name : ""));
            CreateMap<CategoryPutDTO, Category>().ReverseMap();
            CreateMap<CategoryPostDTO, Category>();

            CreateMap<Brand, BrandGetDTO>();
            CreateMap<BrandPutDTO, Brand>().ReverseMap();
            CreateMap<BrandPostDTO, Brand>();

            CreateMap<Product, ProductGetDTO>()
                .ForMember(x => x.TopCategory, dest => dest.MapFrom(x => x.TopCategory.Name))
                .ForMember(x => x.SubCategory, dest => dest.MapFrom(x => x.SubCategory.Name))
                .ForMember(x => x.Brand, dest => dest.MapFrom(x => x.Brand.Name))
                .ForMember(x => x.Colors, dest => dest.MapFrom(x => x.ProductColors.Select(x => new ProductColorGetDTO
                {
                    Id = x.ColorId,
                    Name = x.Color.Name,
                    Sizes = x.ProductColorSizes
                                .Select(x => new ProductColorSizeDTO
                                {
                                    Id = x.Id,
                                    SizeName = x.Size.ShortName,
                                    ProductName = x.ProductColor.Product.Name,
                                    ColorName = x.ProductColor.Color.Name,
                                    Count = x.Count
                                })
                                .ToList()
                }).ToList()))
                .ForMember(x => x.Images, dest => dest.MapFrom(x => x.ProductImages.Select(x => x.Image).ToList()));
            CreateMap<ProductPostDTO, Product>();
            CreateMap<ProductColorPutDTO, ProductColor>().ReverseMap();
            CreateMap<ProductColorSize, ProductColorSizeDTO>().ReverseMap();
            CreateMap<ProductPutDTO, Product>().ReverseMap()
                .ForMember(x => x.Colors, dest => dest.MapFrom(x => x.ProductColors.Select(x => new ProductColorGetDTO
                {
                    Id = x.Id,
                    Name = x.Color.Name,
                    Sizes = x.ProductColorSizes
                                .Select(x => new ProductColorSizeDTO
                                {
                                    Id = x.Id,
                                    SizeName = x.Size.ShortName,
                                    ProductName = x.ProductColor.Product.Name,
                                    ColorName = x.ProductColor.Color.Name,
                                    Count = x.Count
                                })
                                .ToList()
                }).ToList()))
                .ForMember(x => x.Images, dest => dest.MapFrom(x => x.ProductImages.Select(x => x.Image).ToList()));
        }
    }
}
