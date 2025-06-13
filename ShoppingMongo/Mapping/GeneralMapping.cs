using AutoMapper;
using ShoppingMongo.Dtos.CategoryDtos;
using ShoppingMongo.Dtos.CustomerDtos;
using ShoppingMongo.Dtos.ProdcutDetailImageDtos;
using ShoppingMongo.Dtos.ProductDtos;
using ShoppingMongo.Entities;

namespace ShoppingMongo.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();
            CreateMap<Category, ResultCategoryDto>().ReverseMap();

            CreateMap<CreateCustomerDto, Customer>().ReverseMap();
            CreateMap<UpdateCustomerDto, Customer>().ReverseMap();
            CreateMap<GetCustomerByIdDto, Customer>().ReverseMap();
            CreateMap<ResultCustomerDto, Customer>().ReverseMap();

            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            CreateMap<GetProductByIdDto, Product>().ReverseMap();
            CreateMap<ResultProductDto, Product>().ReverseMap();

            CreateMap<ResultProductDetailImageDto, ProductDetailImage>().ReverseMap();
            CreateMap<GetProductDetailImageDto, ProductDetailImage>().ReverseMap();
            CreateMap<UpdateProductDetailImageDto, ProductDetailImage>().ReverseMap();
            CreateMap<CreateProductDetailImageDto, ProductDetailImage>().ReverseMap();
        }
    }
}
