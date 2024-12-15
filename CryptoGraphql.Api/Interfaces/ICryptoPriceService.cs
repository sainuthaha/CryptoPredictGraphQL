using CryptoPredict.Api.Models;

namespace CryptoPredict.Api.Interfaces
{
    public interface ICryptoPriceService
	{
        Task<float> GetBtcCurrentPrice();

        Task<MarketRange> GetBtcMarketRange(long fromEpoch, long toEpoch);

		Task<MarketRange> GetEthMarketRange(long fromEpoch, long toEpoch);

	}
}
