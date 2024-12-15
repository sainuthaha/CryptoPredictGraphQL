using CryptoPredict.Api.Schema.Types;
using GraphQL.Types;

public class RootQuery : ObjectGraphType
{
    public RootQuery(CryptoPriceQuery cryptoPriceQuery, UserScoreDataQuery userScoreDataQuery)
    {
        AddField(cryptoPriceQuery.Field<FloatGraphType>("currentPrice"));
        AddField(cryptoPriceQuery.Field<MarketRangeType>("btcMarketRange"));
        AddField(cryptoPriceQuery.Field<MarketRangeType>("ethMarketRange"));
        AddField(userScoreDataQuery.Field<UserScoreDataType>("userScoreData"));
    }
}