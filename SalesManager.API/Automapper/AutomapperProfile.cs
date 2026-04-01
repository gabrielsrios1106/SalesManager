using AutoMapper;
using DataTransferObjects.Departments;
using DataTransferObjects.Products;
using DataTransferObjects.StockMovement;
using DataTransferObjects.Clients;
using Models;
using DataTransferObjects.Utils;

namespace SalesManager.API.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Product, ProductGetDTO>().ReverseMap();
            CreateMap<Product, ProductPutDTO>().ReverseMap();
            CreateMap<Product, ProductPostDTO>().ReverseMap();

            CreateMap<Department, DepartmentGetDTO>().ReverseMap();
            CreateMap<Department, DepartmentPutDTO>().ReverseMap();
            CreateMap<Department, DepartmentPostDTO>().ReverseMap();

            CreateMap<Client, ClientGetDTO>().ReverseMap();
            CreateMap<Client, ClientPutDTO>().ReverseMap();
            CreateMap<Client, ClientPostDTO>().ReverseMap();

            CreateMap<StockMovement, StockMovementGetDTO>().ReverseMap();
            CreateMap<StockMovement, StockMovementPurchasePostDTO>().ReverseMap();

            CreateMap<User, UserPostDTO>().ReverseMap();
        }
    }
}
