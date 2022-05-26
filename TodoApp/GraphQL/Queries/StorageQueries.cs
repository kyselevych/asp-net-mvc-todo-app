using GraphQL.Types;
using Business.Repositories;
using TodoApp.GraphQL.Types;
using Business.Entities;
using GraphQL;
using TodoApp.Infrastructure;
using AutoMapper;
using TodoApp.Models;

namespace TodoApp.GraphQL.Queries
{
    public class StorageQueries : ObjectGraphType
    {
        private readonly StorageControl storageControl;

        private readonly IMapper mapper;

        public StorageQueries(StorageControl storageControl, IMapper mapper)
        {
            this.storageControl = storageControl;
            this.mapper = mapper;

            Field<StorageType>()
                .Name("Current")
                .Resolve(context =>
                {
                    var storageModel = mapper.Map<StorageModel>(storageControl);

                    return storageModel;
                });

            Field<ListGraphType<StorageType>, IEnumerable<StorageModel>>()
                .Name("List")
                .Resolve(context =>
                {
                    var storageModelList = Enum.GetValues(typeof(Enums.StorageType))
                        .Cast<Enums.StorageType>()
                        .Select(element => new StorageModel() { Type = element });

                    return storageModelList;
                });
        }
    }
}
