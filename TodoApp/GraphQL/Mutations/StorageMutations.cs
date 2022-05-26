using GraphQL.Types;
using Business.Repositories;
using TodoApp.GraphQL.Types;
using TodoApp.GraphQL.DTO;
using Business.Entities;
using GraphQL;
using AutoMapper;
using TodoApp.Models;
using System.ComponentModel.DataAnnotations;
using TodoApp.Infrastructure;

namespace TodoApp.GraphQL.Queries
{
    public class StorageMutations : ObjectGraphType
    {
        public readonly StorageControl storageControl;

        public readonly IMapper mapper;

        public StorageMutations(StorageControl storageControl, IMapper mapper)
        {
            this.storageControl = storageControl;
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

                    bool isValidStringValue = Enum.TryParse(
                        storageSwitchInput.Type,
                        true,
                        out Enums.StorageType parsedStorageTypeValue
                    );

                    bool isDefinedStorageType = Enum.IsDefined(
                        typeof(Enums.StorageType),
                        parsedStorageTypeValue
                    );

                    if (!isValidStringValue || !isDefinedStorageType)
                        throw new ExecutionError("Storage type is not valid");

                    storageControl.Type = parsedStorageTypeValue;

                    var storageModel = mapper.Map<StorageModel>(storageSwitchInput);

                    return storageModel;
                });
        }
    }
}
