using CryptoPredict.Api.Extensions;
using CryptoPredict.Api.Interfaces;
using CryptoPredict.Api.Models;


namespace CryptoPredict.Api.Services
{
	public class CryptoPriceService : ICryptoPriceService
	{
		private readonly Random _random = new Random();
		private readonly HttpClient httpClient;
		private readonly string apikey;

		public CryptoPriceService(HttpClient httpClient, IConfiguration configuration)
		{
			this.httpClient = httpClient;
			this.apikey = configuration.GetValue<string>("CoinGecko:ApiKey") ?? throw new ArgumentNullException(nameof(apikey));
		}

		public async Task<float> GetBtcCurrentPrice()
		{
			await Task.Delay(500);
			float randomPrice = (float)(_random.NextDouble() * (35000 - 25000) + 25000);
			return randomPrice;
		}

		public async Task<MarketRange> GetBtcMarketRange(long fromEpoch, long toEpoch)
		{
			var fromUnix = fromEpoch;
			var toUnix = toEpoch;
			var btcEndPoint = $"/api/v3/coins/bitcoin/market_chart/range?vs_currency=usd&from={fromUnix}&to={toUnix}&x_cg_demo_api_key={apikey}";
			var response = await httpClient.GetResponseAsync<MarketRange>(btcEndPoint);
			return response;
		}

		public async Task<MarketRange> GetEthMarketRange(long fromEpoch, long toEpoch)
		{
			var fromUnix = fromEpoch;
			var toUnix = toEpoch;
			var ethEndpoint = $"/api/v3/coins/ethereum/market_chart/range?vs_currency=usd&from={fromUnix}&to={toUnix}&x_cg_demo_api_key={apikey}";
			Console.WriteLine(ethEndpoint);
			var response = await httpClient.GetResponseAsync<MarketRange>(ethEndpoint);
			return response;
		}
	}
}
