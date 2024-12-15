using CryptoPredict.Api.Interfaces;
using CryptoPredict.Api.Schema.Types;
using GraphQL;
using GraphQL.Types;

public class CryptoPriceQuery : ObjectGraphType
{
    public CryptoPriceQuery(ICryptoPriceService cryptoPriceService,IUserScoreDataService userScoreDataService)
    {
        Field<FloatGraphType>(
            "currentPrice",
            resolve: context => cryptoPriceService.GetBtcCurrentPrice().Result);

        Field<MarketRangeType>(
            "btcMarketRange",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<LongGraphType>> { Name = "fromEpoch" },
                new QueryArgument<NonNullGraphType<LongGraphType>> { Name = "toEpoch" }
            ),
            resolve: context =>
            {
                var fromEpoch = context.GetArgument<long>("fromEpoch");
                var toEpoch = context.GetArgument<long>("toEpoch");
                return cryptoPriceService.GetBtcMarketRange(fromEpoch, toEpoch).Result;
            });
        Field<MarketRangeType>(
            "ethMarketRange",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<LongGraphType>> { Name = "fromEpoch" },
                new QueryArgument<NonNullGraphType<LongGraphType>> { Name = "toEpoch" }
            ),
            resolve: context =>
            {
                var fromEpoch = context.GetArgument<long>("fromEpoch");
                var toEpoch = context.GetArgument<long>("toEpoch");
                return cryptoPriceService.GetEthMarketRange(fromEpoch, toEpoch).GetAwaiter().GetResult();
            });

        FieldAsync<UserScoreDataType>(
            "userScoreData", // Field name
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" }
            ),
            resolve: async context =>
            {
                var userId = context.GetArgument<string>("userId");
                // Get user score data asynchronously
                return await userScoreDataService.GetUserScoreData(userId); 
            }
        );
    }
}