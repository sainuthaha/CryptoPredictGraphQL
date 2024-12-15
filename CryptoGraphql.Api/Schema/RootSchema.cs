/*using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

public class RootSchema : Schema
{
    public RootSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        // Set up the Query property to include both CryptoPriceQuery and UserScoreQuery
         Query = new ObjectGraphType();

        // Add CryptoPriceQuery field
        ((ObjectGraphType)Query).Field<CryptoPriceQuery>(
            "cryptoPriceQuery", // Name of the query field
            resolve: context => serviceProvider.GetRequiredService<CryptoPriceQuery>()
        );
        
        // Add UserScoreQuery field
         ((ObjectGraphType)Query).Field<UserScoreDataQuery>(
            "userScoreQuery", // Name of the query field
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" }
            ), // Define the 'userId' argument
            resolve: context => 
            {
                // Resolve the UserScoreDataQuery and pass the 'userId' argument
                var userId = context.GetArgument<string>("userId");
                var userScoreQuery = serviceProvider.GetRequiredService<UserScoreDataQuery>();
                return userScoreQuery.ResolveUserScoreData(userId); // Call the resolver method
            }
        );
    }
}
*/