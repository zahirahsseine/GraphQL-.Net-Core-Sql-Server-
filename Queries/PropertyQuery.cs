using GraphQL.Types;
using GraphqlIt.DataAccess;
using GraphqlIt.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
namespace GraphqlIt.Queries
{
    public class PropertyQuery:ObjectGraphType
    {
        public PropertyQuery(IRepositoryProperty repositoryProperty)
        {
            Field<ListGraphType<PorpertyType>>(
                "properties",
                resolve: context => repositoryProperty.GetAll()
                ) ;
            Field<PorpertyType>(
                "property",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "Id" }),
                resolve: context => repositoryProperty.GetId(context.GetArgument<int>("id"))
                );

        }

    }
}
