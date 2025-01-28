class Program{
    static void Main(String[] args) {
        List<House> houses = new List<House>();

        while(true){
            Console.WriteLine("1 - New house\n2 - Monitor house\n3 - Exit\nPress key of desired action");
            string userChoice = Console.ReadLine();
            switch (userChoice){
                case "1":
                    addHouse(houses);
                    break;
                case "2":
                    monitorHouse(houses);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid input...");
                    break;
            }
    }

    static void addHouse(List<House> houses){
        Console.WriteLine("Enter house name: ");
        string nameInput = Console.ReadLine();
        House house = new House(nameInput);
        Console.WriteLine("Enter the number of rooms: ");
        int roomNum = int.Parse(Console.ReadLine());
        for (int i = 0; i<roomNum; i++){
            Console.Write($"Enter the name of the {i + 1}. room ");
            string roomName = Console.ReadLine();

            Console.Write($"Enter the number of windows in the {roomName}: ");
            int windowNum = int.Parse(Console.ReadLine());
            
            Console.Write($"Enter the number of doors in the {roomName}: ");
            int doorNum = int.Parse(Console.ReadLine());

            house.rooms.Add(new Room(roomName, windowNum, doorNum));   
        }
        houses.Add(house);
        Console.WriteLine("New house added.");
    }

    static void monitorHouse(List<House> houses){
        if (houses.Count == 0){
            Console.WriteLine("Error. No houses on record. Create a new house or exit the program.");
            return;
        }

        Console.WriteLine("Available houses: ");
        for(int i = 0; i < houses.Count; i++){
            Console.WriteLine($"{i + 1}. {houses[i].houseName}");
        }
        Console.WriteLine("Select a house: ");
        int houseChoice = int.Parse(Console.ReadLine())-1;
        if (houseChoice < 1 || houseChoice >= houses.Count()){
            Console.WriteLine("Invalid choice. House you've picked does not exist.");
            return;
        }
        House selectedHouse = houses[houseChoice];
        displayHouseStatus(selectedHouse);
    }
    static void displayHouseStatus(House house){
        Console.WriteLine($"Monitoring {house.houseName}");
        foreach (var room in house.rooms)
        {
            Console.WriteLine($"\n_______________________");
            Console.WriteLine($"| Room: {room.roomName,-10} |");
            Console.WriteLine($"_______________________");
            Console.WriteLine($"| Temp: {room.temperature}°C   |");
            Console.WriteLine($"| CO2: {room.co2Level} ppm |");
            Console.WriteLine($"| Door: {(room.isDoorOpen ? "Open " : "Closed")}    |");
            Console.WriteLine($"| Doors: {room.numOfDoors}        |");
            Console.WriteLine($"| Windows: {room.numOfWindows}      |");
            Console.WriteLine($"_______________________");
        }
    }

}
}