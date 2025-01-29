using System;
using System.Collections.Generic;

class Program
{
    private static readonly List<House> houses = new List<House>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Smart Home Monitoring System");
            Console.WriteLine("1. Monitor existing houses");
            Console.WriteLine("2. Add new house");
            Console.WriteLine("3. Edit house");
            Console.WriteLine("4. Exit");
            Console.Write("Select option: ");

            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) continue;

            switch (input)
            {
                case "1": MonitorHouses(); break;
                case "2": AddNewHouse(); break;
                case "3": EditHouse(); break;
                case "4": return;
                default: Console.WriteLine("Invalid option!"); break;
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void MonitorHouses()
    {
        if (!ValidateHouses()) return;
        var house = SelectHouse();
        if (house == null) return;

        Console.Clear();
        Console.WriteLine($"Monitoring: {house.Name}");
        
        foreach (var room in house.Rooms)
        {
            Console.WriteLine($"\n┌─{room.Name}─────────────────┐");
            Console.WriteLine($"│ Doors: {room.NumberOfDoors,-2}  Windows: {room.NumberOfWindows,-2} │");
            
            foreach (var sensor in room.Sensors)
            {
                var value = sensor.GetValue();
                var status = value >= sensor.OptimalMin && value <= sensor.OptimalMax ? "OK" : "ALERT";
                Console.WriteLine($"│ {sensor.Name,-12} {value,6}{sensor.Unit} {status,-6}│");
            }
            Console.WriteLine("└───────────────────────────┘");
        }
    }

    static void AddNewHouse()
    {
        Console.Write("Enter house name: ");
        var name = Console.ReadLine();
        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Invalid house name!");
            return;
        }

        var house = new House(name);
        
        int roomCount = GetValidNumber("Number of rooms: ", 1);

        for (int i = 0; i < roomCount; i++)
        {
            Console.Write($"Room {i+1} name: ");
            var roomName = Console.ReadLine();
            while (string.IsNullOrEmpty(roomName))
            {
                Console.WriteLine("Room name cannot be empty!");
                roomName = Console.ReadLine();
            }

            int doors = GetValidNumber("Number of doors: ", 0);
            int windows = GetValidNumber("Number of windows: ", 0);

            var room = new Room(roomName, doors, windows);
            AddDefaultSensors(room);
            house.AddRoom(room);
        }
        
        houses.Add(house);
        Console.WriteLine("House added successfully!");
    }

    static void EditHouse()
    {
        if (!ValidateHouses()) return;
        var house = SelectHouse();
        if (house == null) return;

        Console.WriteLine("1. Add room\n2. Add sensor to room");
        var choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write("Room name: ");
            var name = Console.ReadLine();
            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Room name cannot be empty!");
                name = Console.ReadLine();
            }

            int doors = GetValidNumber("Number of doors: ", 0);
            int windows = GetValidNumber("Number of windows: ", 0);

            var room = new Room(name, doors, windows);
            AddDefaultSensors(room);
            house.AddRoom(room);
            Console.WriteLine("Room added!");
        }
        else if (choice == "2")
        {
            var room = SelectRoom(house);
            if (room == null) return;

            Console.Write("Sensor type (1-4):\n1. Temperature\n2. CO2\n3. Door\n4. Custom\n> ");
            var sensorChoice = Console.ReadLine();

            switch (sensorChoice)
            {
                case "1":
                    room.AddSensor(new TemperatureSensor());
                    break;
                case "2":
                    room.AddSensor(new CO2Sensor());
                    break;
                case "3":
                    room.AddSensor(new DoorSensor());
                    break;
                case "4":
                    Console.Write("Sensor name: ");
                    var sensorName = Console.ReadLine();
                    Console.Write("Measurement unit: ");
                    var unit = Console.ReadLine();
                    double min = GetValidNumber("Optimal minimum: ");
                    double max = GetValidNumber("Optimal maximum: ");
                    room.AddSensor(new CustomSensor(sensorName ?? "Custom", unit ?? "units", min, max));
                    break;
                default:
                    Console.WriteLine("Invalid sensor choice!");
                    return;
            }
            Console.WriteLine("Sensor added!");
        }
    }

    private static bool ValidateHouses()
    {
        if (houses.Count == 0)
        {
            Console.WriteLine("No houses available!");
            return false;
        }
        return true;
    }

    private static House? SelectHouse()
    {
        Console.WriteLine("Available houses:");
        for (int i = 0; i < houses.Count; i++)
            Console.WriteLine($"{i+1}. {houses[i].Name}");
        
        int index = GetValidNumber("Select house: ", 1, houses.Count);
        return houses[index - 1];
    }

    private static Room? SelectRoom(House house)
    {
        Console.WriteLine("Available rooms:");
        for (int i = 0; i < house.Rooms.Count; i++)
            Console.WriteLine($"{i+1}. {house.Rooms[i].Name}");
        
        int index = GetValidNumber("Select room: ", 1, house.Rooms.Count);
        return house.Rooms[index - 1];
    }

    private static void AddDefaultSensors(Room room)
    {
        room.AddSensor(new TemperatureSensor());
        room.AddSensor(new CO2Sensor());
        room.AddSensor(new DoorSensor());
    }

    private static int GetValidNumber(string prompt, int minValue = 0, int maxValue = int.MaxValue)
    {
        int result;
        do
        {
            Console.Write(prompt);
        } while (!int.TryParse(Console.ReadLine(), out result) || result < minValue || result > maxValue);
        return result;
    }
}