

using GraphQL.Utilities;
using GraphqlIt.Mutations;
using GraphqlIt.Queries;
using System;

namespace GraphqlIt.Schema
{
    public class GraphQlSchema:GraphQL.Types.Schema
    {
        public GraphQlSchema(IServiceProvider resolver) : base(resolver)
        {
            Query = resolver.GetRequiredService<PropertyQuery>();
            Mutation = resolver.GetRequiredService<PropertyMutation>();
        }
    }
}
