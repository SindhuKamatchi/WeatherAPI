namespace WeatherAPI.Models.Response
{
    public class RootResponse
    {
        public Location location { get; set; }

        public Current current { get; set; }

        public Error error { get; set; }

    }
}
