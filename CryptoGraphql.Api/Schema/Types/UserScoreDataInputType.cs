using GraphQL.Types;
using CryptoPredict.Api.Models;

public class UserScoreDataInputType : InputObjectGraphType<UserScoreData>
{
    public UserScoreDataInputType()
    {
        Name = "UserScoreDataInput";
        Field(x => x.UserId);
        Field(x => x.Score);
        Field(x => x.GuessTime);
        Field(x => x.GuessPrice);
    }
}