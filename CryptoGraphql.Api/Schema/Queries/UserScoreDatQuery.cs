using GraphQL.Types;
using GraphQL;
using CryptoPredict.Api.Interfaces;
using System.Threading.Tasks;

public class UserScoreDataQuery : ObjectGraphType
{
    private readonly IUserScoreDataService _userScoreDataService;

    public UserScoreDataQuery(IUserScoreDataService userScoreDataService)
    {
        _userScoreDataService = userScoreDataService;

        FieldAsync<UserScoreDataType>(
            "userScoreData", // Field name
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" }
            ),
            resolve: async context =>
            {
                var userId = context.GetArgument<string>("userId");
                // Get user score data asynchronously
                return await _userScoreDataService.GetUserScoreData(userId); 
            }
        );
    }
}
