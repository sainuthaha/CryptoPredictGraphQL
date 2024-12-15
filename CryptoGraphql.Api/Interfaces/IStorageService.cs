using CryptoPredict.Api.Models;

namespace CryptoPredict.Api.Interfaces
{
	public interface IStorageService
	{
		Task<UserScoreData?> PostUserScoreData(UserScoreData userScoreData);
		Task<UserScoreData?> GetUserScoreData(string userId);
	}
}
