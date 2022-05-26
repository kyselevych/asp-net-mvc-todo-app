using AutoMapper;
using Business.Entities;
using TodoApp.ViewModels;
using TodoApp.GraphQL.DTO;
using TodoApp.Models;
using TodoApp.Infrastructure;

namespace TodoApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Tasks
            CreateMap<CreateTaskViewModel, TaskModel>();

            CreateMap<TaskModel, CurrentTaskItemViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<TaskModel, CompletedTaskItemViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            // Categories
            CreateMap<CategoryModel, EditCategoryViewModel>()
                .ReverseMap();

            CreateMap<CreateCategoryViewModel, CategoryModel>();

            CreateMap<CategoryModel, CategoryListItemViewModel>();

            CreateMap<CategoryModel, FilterCategoryListItemViewModel>();

            // GraphQL

            CreateMap<TaskCreateInput, TaskModel>()
                .ReverseMap();

            CreateMap<CategoryCreateInput, CategoryModel>()
                .ReverseMap();

            CreateMap<CategoryUpdateInput, CategoryModel>()
                .ReverseMap();

            CreateMap<StorageSwitchInput, StorageModel>()
                .ReverseMap();

            CreateMap<StorageControl, StorageModel>()
                .ReverseMap();
        }
    }
}
