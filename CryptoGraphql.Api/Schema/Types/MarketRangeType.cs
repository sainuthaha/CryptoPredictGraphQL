using CryptoPredict.Api.Models;
using GraphQL.Types;

namespace CryptoPredict.Api.Schema.Types
{
    public class MarketRangeType : ObjectGraphType<MarketRange>
    {
        public MarketRangeType()
        {
            Field<ListGraphType<PricePointType>>("prices");
            Field<ListGraphType<MarketCapPointType>>("marketCaps");
            Field<ListGraphType<VolumePointType>>("totalVolumes");
        }
    }

    public class PricePointType : ObjectGraphType<PricePoint>
    {
        public PricePointType()
        {
            Field(x => x.Timestamp);
            Field(x => x.Price);
        }
    }

    public class MarketCapPointType : ObjectGraphType<MarketCapPoint>
    {
        public MarketCapPointType()
        {
            Field(x => x.Timestamp);
            Field(x => x.MarketCap);
        }
    }

    public class VolumePointType : ObjectGraphType<VolumePoint>
    {
        public VolumePointType()
        {
            Field(x => x.Timestamp);
            Field(x => x.Volume);
        }
    }
}