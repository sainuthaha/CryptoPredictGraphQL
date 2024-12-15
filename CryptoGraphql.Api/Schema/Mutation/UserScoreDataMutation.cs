using GraphQL.Types;
using CryptoPredict.Api.Interfaces;
using CryptoPredict.Api.Models;
using GraphQL;

public class UserScoreDataMutation : ObjectGraphType
{
    public UserScoreDataMutation(IUserScoreDataService userScoreDataService)
    {
        Field<UserScoreDataType>(
            "updateUserScoreData",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<UserScoreDataInputType>> { Name = "userScoreData" }
            ),
            resolve: context =>
            {
                var userScoreData = context.GetArgument<UserScoreData>("userScoreData");
                return userScoreDataService.PostUserScoreData(userScoreData).Result;
            }
        );
    }
}