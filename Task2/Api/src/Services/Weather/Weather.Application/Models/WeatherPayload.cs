using Newtonsoft.Json;

namespace Weather.Application.Models;
public class WeatherPayload
{
    [JsonProperty("id")]
    public int ApiId { get; set; }

    [JsonProperty("coord")]
    public Coordinates Coordinates { get; set; } = null!;

    [JsonProperty("weather")]
    public List<Weather> Weather { get; set; } = null!;

    [JsonProperty("base")]
    public string Base { get; set; } = null!;

    [JsonProperty("main")]
    public Main Main { get; set; } = null!;

    [JsonProperty("visibility")]
    public int Visibility { get; set; }

    [JsonProperty("wind")]
    public Wind Wind { get; set; } = null!;

    [JsonProperty("clouds")]
    public Clouds Clouds { get; set; } = null!;

    [JsonProperty("dt")]
    public long Dt { get; set; }

    [JsonProperty("sys")]
    public Sys Sys { get; set; } = null!;

    [JsonProperty("timezone")]
    public int Timezone { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("cod")]
    public int Cod { get; set; }
}

public class Coordinates
{
    [JsonProperty("lon")]
    public double Longitude { get; set; }

    [JsonProperty("lat")]
    public double Latitude { get; set; }
}

public class Weather
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("main")]
    public string Main { get; set; } = null!;

    [JsonProperty("description")]
    public string Description { get; set; } = null!;

    [JsonProperty("icon")]
    public string Icon { get; set; } = null!;
}

public class Main
{
    [JsonProperty("temp")]
    public double Temp { get; set; }

    [JsonProperty("feels_like")]
    public double FeelsLike { get; set; }

    [JsonProperty("temp_min")]
    public double TempMin { get; set; }

    [JsonProperty("temp_max")]
    public double TempMax { get; set; }

    [JsonProperty("pressure")]
    public int Pressure { get; set; }

    [JsonProperty("humidity")]
    public int Humidity { get; set; }

    [JsonProperty("sea_level")]
    public int? SeaLevel { get; set; }

    [JsonProperty("grnd_level")]
    public int? GrndLevel { get; set; }
}

public class Wind
{
    [JsonProperty("speed")]
    public double Speed { get; set; }

    [JsonProperty("deg")]
    public int Deg { get; set; }
}

public class Clouds
{
    [JsonProperty("all")]
    public int All { get; set; }
}

public class Sys
{
    [JsonProperty("type")]
    public int Type { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; } = null!;

    [JsonProperty("sunrise")]
    public long Sunrise { get; set; }

    [JsonProperty("sunset")]
    public long Sunset { get; set; }
}