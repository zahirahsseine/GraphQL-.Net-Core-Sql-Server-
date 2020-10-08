using GraphQL.Types;
using GraphqlIt.DataAccess;
using GraphqlIt.Types;
using GraphQL;
using GraphqlIt.DataBase;

namespace GraphqlIt.Mutations
{
    public class PropertyMutation: ObjectGraphType
    {
        public PropertyMutation(IRepositoryProperty repositoryProperty)
        {
            Field<PorpertyType>(
                "addProperty",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<PropertyInputType>> { Name = "property" }),
                resolve:
                context =>
                {
                    var property = context.GetArgument<Property>("property");
                    return repositoryProperty.Add(property);

                }
                );
        }  
    }
}
