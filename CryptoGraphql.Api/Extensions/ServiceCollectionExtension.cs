using CryptoPredict.Api.Interfaces;
using CryptoPredict.Api.Services;
using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;

namespace CryptoPredict.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
		private sealed record HttpClientOptions
		{
			public required Uri BaseAddress { get; init; }
		}

		public static async Task<TResponse> GetResponseAsync<TResponse>(
			this HttpClient client,
			string url
		)
		{
			var response = await client.GetAsync(url);
			return await ValidateResponseAsync<TResponse>(response);
		}

		private static async Task<TResponse> ValidateResponseAsync<TResponse>(
			HttpResponseMessage response
		)
		{
			var responseString = await response.Content.ReadAsStringAsync();
			var responseObj = JsonConvert.DeserializeObject<TResponse>(responseString);
			return responseObj is null
				? throw new System.Text.Json.JsonException("Unable to deserialize response")
				: responseObj;
		}

		public static IServiceCollection AddStorageService(this IServiceCollection services, IConfiguration config)
		{
			services.AddSingleton<IStorageService, StorageService>((serviceProvider) =>
			{
				string storageConnectionString = config.GetValue<string>("ConnectionString") ?? throw new InvalidOperationException("ConnectionString must be set in the configuration.");
				CloudTableClient tableClient = null; // Initialize to null
				if (CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
				{
					tableClient = storageAccount.CreateCloudTableClient();
				}
				else
				{
					throw new InvalidOperationException("Invalid storage connection string.");
				}
				return new StorageService(tableClient);
			});

			return services;
		}

		public static IServiceCollection AddHttpClient<TClient, TImplementation>(
            this IServiceCollection services,
            IConfiguration config,
            string configSectionName = "HttpClient"
        )
            where TClient : class
            where TImplementation : class, TClient
        {
            var serviceName = typeof(TImplementation).Name;
            var section = config.GetSection(configSectionName).GetSection(serviceName);
            var options = section.Get<HttpClientOptions>();
            if (options is null)
            {
                throw new ArgumentException(
                    nameof(configSectionName),
                    "Config section cannot be empty"
                );
            }

            var builder = services
                .AddHttpClient<TClient, TImplementation>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = options.BaseAddress;
					client.DefaultRequestHeaders.Add("accept","application/json");
				})
                ;
            return services;
        }
    }
}
