using AutoMapper;
using Business.Entities;
using TodoApp.ViewModels;

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
        }
    }
}
