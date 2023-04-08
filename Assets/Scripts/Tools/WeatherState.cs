using System.Collections.Generic;

public enum WeatherState{
    Empty,
    Sunny,
    Rainy,
    Windy,
    AirPollution,
    UrbanHeatIsland,
    Rainstorms,
    Snow
}

public enum SeasonState{
    Spring,
    Summer,
    Autumm,
    Winter

}

public class WeatherStateConstants{
    public static readonly List<WeatherState> SpringWeatherStates = new List<WeatherState>(){WeatherState.Sunny,WeatherState.Sunny,WeatherState.Rainy,WeatherState.Rainy,WeatherState.Windy,WeatherState.Windy};
    public static readonly List<WeatherState> SummerWeatherStates = new List<WeatherState>(){WeatherState.Sunny,WeatherState.Sunny,WeatherState.Rainstorms,WeatherState.Rainy,WeatherState.Windy,WeatherState.Windy};
    public static readonly List<WeatherState> AutummWeatherStates = new List<WeatherState>(){WeatherState.Sunny,WeatherState.Sunny,WeatherState.Sunny,WeatherState.Rainy,WeatherState.Rainy,WeatherState.Windy};
    public static readonly List<WeatherState> WinterWeatherStates = new List<WeatherState>(){WeatherState.Sunny,WeatherState.Sunny,WeatherState.Snow,WeatherState.Snow,WeatherState.Windy,WeatherState.Windy};
}
