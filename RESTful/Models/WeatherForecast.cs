/******************************************
 * AUTHOR:          Shah-MI
 * CREATION:       2023-08-12
 ******************************************/

namespace RESTful.Models;

public class WeatherForecast
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public string? Summary { get; set; }

    public List<Link> Links { get; set; }

    public void Edit(DateOnly date, int temperatureC)
    {
        Date = date;
        TemperatureC = temperatureC;
    }
}

public class CreateWeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }
}

public class UpdateWeatherForecast
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }
}