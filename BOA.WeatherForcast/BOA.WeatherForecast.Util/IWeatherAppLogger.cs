namespace BOA.WeatherForecast.Util
{
    public interface IWeatherAppLogger<T>
    {
        void LogError(string message, params object[] arg);
    }
}