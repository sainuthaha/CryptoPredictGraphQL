using GraphQL.Types;

public class UserScoreSchema : Schema
{
    public UserScoreSchema(IServiceProvider provider)
        : base(provider)
    {
        Query = provider.GetRequiredService<UserScoreDataQuery>();
        Mutation = provider.GetRequiredService<UserScoreDataMutation>();
    }
}