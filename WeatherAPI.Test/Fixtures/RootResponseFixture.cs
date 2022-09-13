using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Models.Response;

namespace WeatherAPI.Test.Fixtures
{
    public class RootResponseFixture
    {
        public static RootResponse ValidRootResponse => new RootResponse()
        {
            location = new Location()
            {
                name = "London",
                localtime = "2022-09-10 15:56"
            },
            current = new Current()
            {
                condition = new Condition()
                {
                    text = "Partly cloudy"
                },
                temp_f = 69.8f,
            },
        };

        public static RootResponse ErrorRootResponse => new RootResponse()
        {
            error = new Error()
            {
                code = "Data Missing",
                message = "Call to weather API failed"
            }
        };

    }
}
