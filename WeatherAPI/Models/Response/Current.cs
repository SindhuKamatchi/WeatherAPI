namespace WeatherAPI.Models.Response
{
    public class Current
    {
        public float temp_f { get; set; } 

        public Condition condition { get; set; }
    }
}
