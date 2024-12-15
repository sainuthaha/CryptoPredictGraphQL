using GraphQL.Types;
using CryptoPredict.Api.Models;

public class UserScoreDataType : ObjectGraphType<UserScoreData>
{
    public UserScoreDataType()
    {
        Name = "UserScoreData";
        Field(x => x.UserId).Description("The ID of the user.");
        Field(x => x.Score).Description("The score of the user.");
        Field(x => x.GuessTime).Description("The time of the guess.");
        Field(x => x.GuessPrice).Description("The price guessed by the user.");
    }
}