using CryptoPredict.Api.Interfaces;
using CryptoPredict.Api.Models;
using Microsoft.Azure.Cosmos.Table;

namespace CryptoPredict.Api.Services
{
	public class UserScoreDataService : IUserScoreDataService
	{
		private readonly IStorageService storageService;

		public UserScoreDataService(IStorageService storageService)
		{
			this.storageService = storageService;
		}
		public async Task<UserScoreData?> GetUserScoreData(string userId)
		{
			return await storageService.GetUserScoreData(userId);
		}

		public async Task<UserScoreData> PostUserScoreData(UserScoreData userScoreData)
		{
			return await storageService.PostUserScoreData(userScoreData);
		}
	}
}
