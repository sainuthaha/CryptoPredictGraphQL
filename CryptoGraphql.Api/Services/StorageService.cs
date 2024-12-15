using CryptoPredict.Api.Extensions;
using CryptoPredict.Api.Interfaces;
using CryptoPredict.Api.Models;
using Microsoft.Azure.Cosmos.Table;

namespace CryptoPredict.Api.Services
{
	public class StorageService : IStorageService
	{
		private readonly CloudTableClient tableClient;

		private async Task<T> CreateOrUpdateInStorageAsync<T>(CloudTable table, T entity, IDictionary<string, EntityProperty> keyValuePairs = null) where T : TableEntity
		{
			if (string.IsNullOrEmpty(entity.PartitionKey))
			{
				entity.PartitionKey = Guid.NewGuid().ToString();
			}

			if (string.IsNullOrEmpty(entity.RowKey))
			{
				entity.RowKey = Guid.NewGuid().ToString();
			}

			table.CreateIfNotExists();

			TableOperation upsertOperation = default;
			DynamicTableEntity dynamicTableEntity = new DynamicTableEntity(entity.PartitionKey, entity.RowKey);
			dynamicTableEntity.Properties = keyValuePairs;
			upsertOperation = (keyValuePairs?.Count > 0) ?
				TableOperation.InsertOrReplace(dynamicTableEntity) :
				TableOperation.InsertOrReplace(entity);
			var result = await table.ExecuteAsync(upsertOperation);

			return result.Result as T;
		}
		private async Task<List<DynamicTableEntity>> GetDataFromStorageAsync(CloudTable table, TableQuery query)
		{
			var entities = new List<DynamicTableEntity>();
			TableContinuationToken continuationToken = null;
			do
			{
				var queryResult = await table.ExecuteQuerySegmentedAsync(query, continuationToken);

				continuationToken = queryResult.ContinuationToken;
				entities.AddRange(queryResult.Results);

			} while (continuationToken != null);

			return entities;
		}
		public StorageService(CloudTableClient tableClient)
		{
			this.tableClient = tableClient;
		}

		public async Task<UserScoreData?> GetUserScoreData(string userId)
		{
			var table = this.tableClient.GetTableReference("ScoreData");

			var userScoreDataFilter = TableQuery.GenerateFilterCondition("UserId", QueryComparisons.Equal, userId);

			var tableQuery = new TableQuery().Where(userScoreDataFilter);

			var entities = await this.GetDataFromStorageAsync(table, tableQuery);

			return entities.Select(item => item.MapToUserScoreData()).FirstOrDefault();
		}

		public async Task<UserScoreData?> PostUserScoreData(UserScoreData userScoreData)
		{
			var table = this.tableClient.GetTableReference("ScoreData");
			var entity = await GetUserScoreData(userScoreData.UserId);
			if (entity != null)
			{
				userScoreData.PartitionKey = entity.PartitionKey;
				userScoreData.RowKey = entity.RowKey;
			}
			return await this.CreateOrUpdateInStorageAsync(table,userScoreData);
		}
	}
}
