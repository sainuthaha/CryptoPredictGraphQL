namespace CryptoPredict.Api.Models
{
	public class BtcPrice
	{
		public required Bitcoin bpi { get; set; }
	}

	public class Bitcoin
	{
		public required USD usd;
	}

	public class USD
	{
		public required float rate_float;
	
	}
	

}
