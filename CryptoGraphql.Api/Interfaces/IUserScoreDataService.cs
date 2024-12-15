using CryptoPredict.Api.Models;

namespace CryptoPredict.Api.Interfaces
{
	public interface IUserScoreDataService
	{
		Task<UserScoreData> PostUserScoreData(UserScoreData userScoreData);
		Task<UserScoreData?> GetUserScoreData(string userId);
	}
}
