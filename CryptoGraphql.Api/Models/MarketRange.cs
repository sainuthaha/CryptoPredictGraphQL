using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CryptoPredict.Api.Models
{
	// Generic converter to deserialize arrays of [timestamp, value] pairs into appropriate objects
	public class MarketPointConverter<TPoint> : JsonConverter<List<TPoint>> where TPoint : new()
	{
		public override List<TPoint> ReadJson(JsonReader reader, Type objectType, List<TPoint> existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var points = new List<TPoint>();
			var array = JArray.Load(reader);

			foreach (var item in array)
			{
				var timestamp = item[0]?.Value<long>() ?? 0;
				var value = item[1]?.Value<double>() ?? 0.0;

				// Create a new instance of TPoint and set the properties dynamically
				var point = new TPoint();
				typeof(TPoint).GetProperty("Timestamp")?.SetValue(point, timestamp);
				typeof(TPoint).GetProperty("Price")?.SetValue(point, value);
				typeof(TPoint).GetProperty("MarketCap")?.SetValue(point, value);
				typeof(TPoint).GetProperty("Volume")?.SetValue(point, value);

				points.Add(point);
			}

			return points;
		}

		public override void WriteJson(JsonWriter writer, List<TPoint>? value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}

	public class MarketRange
	{
		[JsonConverter(typeof(MarketPointConverter<PricePoint>))]
		public required List<PricePoint> Prices { get; set; }

		[JsonConverter(typeof(MarketPointConverter<MarketCapPoint>))]
		public required List<MarketCapPoint> MarketCaps { get; set; }

		[JsonConverter(typeof(MarketPointConverter<VolumePoint>))]
		public required List<VolumePoint> TotalVolumes { get; set; }
	}

	public class PricePoint
	{
		public long Timestamp { get; set; }
		public double Price { get; set; }
	}

	public class MarketCapPoint
	{
		public long Timestamp { get; set; }
		public double MarketCap { get; set; }
	}

	public class VolumePoint
	{
		public long Timestamp { get; set; }
		public double Volume { get; set; }
	}
}
