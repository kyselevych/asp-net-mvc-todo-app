using GraphQL.Types;
using Business.Repositories;
using TodoApp.GraphQL.Types;
using Business.Entities;
using GraphQL;

namespace TodoApp.GraphQL.Queries
{
    public class CategoriesQueries : ObjectGraphType
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesQueries(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;

            Field<ListGraphType<CategoryType>, IEnumerable<CategoryModel>>()
                .Name("List")
                .Resolve(context => categoryRepository.GetList());

            Field<CategoryType, CategoryModel>()
                .Name("Category")
                .Argument<IntGraphType, int>("Id", "Category id")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("Id");

                    return categoryRepository.GetById(id);
                });
        }
    }
}
