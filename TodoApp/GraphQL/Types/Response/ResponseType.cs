using Business.Entities;
using GraphQL.Types;
using TodoApp.Models;

namespace TodoApp.GraphQL.Types
{
    public class ResponseType : ObjectGraphType<ResponseModel>
    {
        public ResponseType()
        {
            Field<BooleanGraphType>()
                .Name("Success")
                .Resolve(context => context.Source.Success);
                
        }
    }
}
