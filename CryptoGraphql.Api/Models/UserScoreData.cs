using Microsoft.Azure.Cosmos.Table;

namespace CryptoPredict.Api.Models
{
	public class UserScoreData: TableEntity
	{
        public required string UserId { get; init; }
		public long Score { get; init; }
		public DateTime GuessTime { get; init; }
		public double GuessPrice { get; init; }
	}
}
