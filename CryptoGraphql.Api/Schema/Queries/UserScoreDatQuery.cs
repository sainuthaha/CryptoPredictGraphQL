using GraphQL.Types;
using GraphQL;
using CryptoPredict.Api.Interfaces;

public class UserScoreDataQuery : ObjectGraphType
{
    public UserScoreDataQuery(IUserScoreDataService userScoreDataService)
    {
        Field<UserScoreDataType>(
            "userScoreData",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" }
            ),
            resolve: context =>
            {
                var userId = context.GetArgument<string>("userId");
                return userScoreDataService.GetUserScoreData(userId).Result;
            }
        );
    }
}