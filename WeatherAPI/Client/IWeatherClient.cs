using System.Threading.Tasks;
using WeatherAPI.Models.Response;

namespace WeatherAPI.Client
{
    public interface IWeatherClient
    {
        Task<RootResponse> GetCurrentWeatherAsync(string city, string country);


    }
}
