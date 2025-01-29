public class Room
{
    public string Name { get; }
    public int NumberOfDoors { get; }
    public int NumberOfWindows { get; }
    public List<Sensor> Sensors { get; } = new List<Sensor>();

    public Room(string name, int doors, int windows)
    {
        Name = name;
        NumberOfDoors = doors;
        NumberOfWindows = windows;
    }

    public void AddSensor(Sensor sensor) => Sensors.Add(sensor);
}