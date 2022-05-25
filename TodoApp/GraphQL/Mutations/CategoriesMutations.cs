using GraphQL.Types;
using Business.Repositories;
using TodoApp.GraphQL.Types;
using TodoApp.GraphQL.DTO;
using Business.Entities;
using GraphQL;
using AutoMapper;

namespace TodoApp.GraphQL.Queries
{
    public class CategoriesMutations : ObjectGraphType
    {
        private readonly ICategoryRepository categoryRepository;

        private readonly IMapper mapper;

        public CategoriesMutations(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;

            Field<CategoryType>()
                .Name("CreateCategory")
                .Argument<NonNullGraphType<CategoryCreateInputType>, CategoryCreateInput>(
                    "CategoryCreateInputType",
                    "Argument CategoryCreateInputType for CreateCategory"
                )
                .Resolve(context =>
                {
                    var categoryCreateInput = context.GetArgument<CategoryCreateInput>(
                        "CategoryCreateInputType"
                    );

                    var categoryModel = mapper.Map<CategoryModel>(categoryCreateInput);
                    int id = categoryRepository.Create(categoryModel);

                    return categoryRepository.GetById(id);
                });

            Field<CategoryType>()
                .Name("UpdateCategory")
                .Argument<NonNullGraphType<CategoryUpdateInputType>, CategoryUpdateInput>(
                    "CategoryUpdateInputType",
                    "Argument CategoryUpdateInputType for UpdateCategory"
                )
                .Resolve(context =>
                {
                    var categoryUpdateInput = context.GetArgument<CategoryUpdateInput>(
                        "CategoryUpdateInputType"
                    );

                    var categoryModel = mapper.Map<CategoryModel>(categoryUpdateInput);

                    categoryRepository.Update(categoryModel);

                    return categoryModel;
                });

            Field<CategoryType>()
                .Name("DeleteCategory")
                .Argument<NonNullGraphType<IntGraphType>, int>(
                    "Id",
                    "Argument Id for DeleteCategory"
                )
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("Id");
                    var categoryModel = categoryRepository.GetById(id);

                    categoryRepository.Delete(id);

                    return categoryModel;
                });
        }
    }
}
