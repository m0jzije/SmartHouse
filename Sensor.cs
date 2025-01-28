public class Sensor{
    public double getTemperature() => new Random().Next(10, 40);
    public double getCo2() => new Random().Next(200, 2000);
    public bool isDoorOpen() => new Random().Next(0,2) == 1;

    public bool isWindowOpen() => new Random().Next(0,2) == 1;
}