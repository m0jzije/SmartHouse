public class House
{
    public string Name { get; }
    public List<Room> Rooms { get; } = new List<Room>();

    public House(string name) => Name = name;
    public void AddRoom(Room room) => Rooms.Add(room);
}