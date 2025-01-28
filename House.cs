using System;
using System.Collections.Generic;

public class House {

    public string houseName { get ; set ; }
    public List<Room> rooms { get ; set ;} = new List<Room>();

    public House(string Name) {
        houseName = Name;
    }


}