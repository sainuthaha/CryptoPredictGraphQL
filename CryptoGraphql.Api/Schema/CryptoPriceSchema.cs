using GraphQL.Types;

public class CryptoPriceSchema : Schema
{
    public CryptoPriceSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<CryptoPriceQuery>();
        Mutation = serviceProvider.GetRequiredService<UserScoreDataMutation>();
    }
}

