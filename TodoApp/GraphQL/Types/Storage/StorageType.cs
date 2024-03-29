﻿using GraphQL.Types;
using Business.Entities;
using TodoApp.GraphQL.Types;
using TodoApp.Models;

namespace TodoApp.GraphQL.Types
{
    public class StorageType : ObjectGraphType<StorageModel>
    {
        public StorageType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Type")
                .Resolve(context => context.Source.Type.ToString("G"));

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Number")
                .Resolve(context => (int)context.Source.Type);
        }
    }
}
