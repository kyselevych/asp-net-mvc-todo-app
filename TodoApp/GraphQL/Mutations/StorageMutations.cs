using GraphQL.Types;
using Business.Repositories;
using TodoApp.GraphQL.Types;
using TodoApp.GraphQL.DTO;
using Business.Entities;
using GraphQL;
using AutoMapper;
using TodoApp.Models;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.GraphQL.Queries
{
    public class StorageMutations : ObjectGraphType
    {
        public readonly IConfiguration configuration;

        public readonly IMapper mapper;

        public StorageMutations(IConfiguration configuration, IMapper mapper)
        {
            this.configuration = configuration;
            this.mapper = mapper;

            Field<StorageType, StorageModel>()
                .Name("Switch")
                .Argument<NonNullGraphType<StorageSwitchInputType>, StorageSwitchInput>(
                    "StorageSwitchInputType",
                    "Argument StorageSwitchInput for switch storage type"
                )
                .Resolve(context =>
                {
                    var storageSwitchInput = context.GetArgument<StorageSwitchInput>(
                        "StorageSwitchInputType"
                    );

                    var validator = new StorageSwitchInputValidator();
                    var isValidStorageSwitchInput = validator.Validate(storageSwitchInput);

                    if (!isValidStorageSwitchInput.IsValid)
                        throw new ExecutionError("Type is not valid");

                    configuration["TypeStorage"] = storageSwitchInput.Type;

                    var storageModel = mapper.Map<StorageModel>(storageSwitchInput);

                    return storageModel;
                });
        }
    }
}
