public class Room {

    private Sensor _sensor = new Sensor();
    public string roomName {get ; set ; }
    public int numOfDoors { get ; set ; }
    public int numOfWindows { get ; set ; }
    public double temperature => _sensor.getTemperature();
    public double co2Level => _sensor.getCo2();
    public bool isDoorOpen => _sensor.isDoorOpen();
    public bool isWindowOpen => _sensor.isWindowOpen();


    public Room (string Name, int Doors, int Windows){
        roomName = Name;
        numOfDoors = Doors;
        numOfWindows = Windows;
    }
}