// Observer pattern

// To run this code, .NET 5 or later is required.

// Provider interface
public interface ISubject
{
    public void Register(ISubscriber subscriber);
    public void Remove(ISubscriber subscriber);
    public void Notify();
}

// Provider class
public sealed class WeatherData : ISubject
{
    float _temperature;
    float _humidity;
    float _pressure;

    public float Temperature { get { return _temperature; } }
    public float Humidity { get { return _humidity; } }
    public float Pressure { get { return _pressure; } }

    List<ISubscriber> _subscribers;

    public WeatherData()
    {
        _subscribers = new List<ISubscriber>();
    }

    public void Register(ISubscriber subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void Remove(ISubscriber subscriber)
    {
        _subscribers.Remove(subscriber);
    }

    public void Notify()
    {
        foreach (var subscriber in _subscribers)
            subscriber.Update();
    }

    public void SetData(float temperature, float humidity, float pressure)
    {
        _temperature = temperature;
        _humidity = humidity;
        _pressure = pressure;
        Notify();
    }
}

// Observer interface
public interface ISubscriber
{
    public void Update();
}

// Display feature interface
public interface IDisplay
{
    public void Display();
}

// Observer class
public sealed class Subscriber : ISubscriber, IDisplay
{
    public WeatherData WeatherData { init; get; }
    private float _temperature;
    private float _humidity;

    public Subscriber(WeatherData weatherData)
    {
        WeatherData = weatherData;
        WeatherData.Register(this);
    }

    public void Update()
    {
        _temperature = WeatherData.Temperature;
        _humidity = WeatherData.Humidity;
        Display();
    }

    public void Display()
    {
        Console.WriteLine($"Current status: Temperature {_temperature}, Humidity: {_humidity}");
    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        var weatherData = new WeatherData();
        var subscriber = new Subscriber(weatherData);

        weatherData.SetData(120.0f, 40.4f, 30.4f);
        weatherData.SetData(118.0f, 40.2f, 30.3f);
        weatherData.SetData(121.0f, 40.1f, 30.2f);
    }
}
