public abstract class Sensor
{
    public string Name { get; protected set; }
    public string Unit { get; protected set; }
    public double OptimalMin { get; protected set; }
    public double OptimalMax { get; protected set; }
    public abstract double GetValue();

    protected Sensor(string name, string unit, double min, double max)
    {
        Name = name;
        Unit = unit;
        OptimalMin = min;
        OptimalMax = max;
    }
}

public class TemperatureSensor : Sensor
{
    public TemperatureSensor() : base("Temperature", "°C", 18, 25) { }
    public override double GetValue() => new Random().Next(15, 35);
}

public class CO2Sensor : Sensor
{
    public CO2Sensor() : base("CO2", "ppm", 300, 1000) { }
    public override double GetValue() => new Random().Next(200, 2000);
}

public class DoorSensor : Sensor
{
    public DoorSensor() : base("Door", "Status", 0, 0) { }
    public override double GetValue() => new Random().Next(0, 2);
}

public class CustomSensor : Sensor
{
    public CustomSensor(string name, string unit, double min, double max) 
        : base(name, unit, min, max) { }
    public override double GetValue() => new Random().Next((int)OptimalMin - 10, (int)OptimalMax + 10);
}