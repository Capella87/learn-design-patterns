public struct WeatherData
{
    private float _temperature;
    private float _humidity;
    private float _pressure;

    public float Temperature { get { return _temperature; } }
    public float Humidity { get { return _humidity; } }
    public float Pressure { get { return _pressure; } }

    internal WeatherData(float temp, float humid, float press)
    {
        _temperature = temp;
        _humidity = humid;
        _pressure = press;
    }
}

public class WeatherDataMonitor : IObservable<WeatherData>
{
    private WeatherData _data;
    private List<IObserver<WeatherData>> _observers;

    public WeatherDataMonitor()
    {
        _observers = new List<IObserver<WeatherData>>();
    }

    public IDisposable Subscribe(IObserver<WeatherData> observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);

        return new Unsubscriber(_observers, observer);
    }
    
    private class Unsubscriber : IDisposable
    {
        private List<IObserver<WeatherData>> _observers;
        private IObserver<WeatherData> _observer;

        public Unsubscriber(List<IObserver<WeatherData>> observers, IObserver<WeatherData> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (!(_observer == null))
                _observers.Remove(_observer);
        }
    }

    public void UpdateData(WeatherData data)
    {
        _data = data;

        foreach (var observer in _observers)
            observer.OnNext(_data);
    }
}

public class WeatherReporter : IObserver<WeatherData>
{
    private IDisposable _unsubscriber;
    private bool first = true;
    private WeatherData _latest;

    public virtual void Subscribe(IObservable<WeatherData> provider)
    {
        _unsubscriber = provider.Subscribe(this);
    }

    public virtual void Unsubscribe()
    {
        _unsubscriber.Dispose();
    }

    public virtual void OnCompleted()
    {

    }
    
    public virtual void OnError(Exception error)
    {

    }

    public virtual void OnNext(WeatherData value)
    {
        Console.WriteLine("Current weather:");
        Console.WriteLine($"Temperature: {value.Temperature} C");
        Console.WriteLine($"Humidity: {value.Humidity} %");
        Console.WriteLine($"Pressure: {value.Pressure} hPa\n");

        if (first)
        {
            _latest = value;
            first = false;
        }
    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        var w = new WeatherData(31.5f, 80, 1013);
        var provider = new WeatherDataMonitor();
        var weatherStation = new WeatherReporter();
        weatherStation.Subscribe(provider);

        provider.UpdateData(w);

        var x = new WeatherData(30.4f, 81, 1012);
        provider.UpdateData(x);
    }
}