using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class Hotel
    {
        private string name;
        private int countofFloor;
        private int countofRooms;
        public List<HotelRoom> listRoom = new List<HotelRoom>();
        public string Name
        {
            get { return name; }
        }
        public int CountofRooms
        {
            get
            {
                return countofRooms;
            }
        }
        public int CountofFloor
        {
            get { return countofFloor; }
        }
        public Hotel(int CountofFloor,int CountofRooms,string Name)
        {
            countofFloor = CountofFloor;
            countofRooms = CountofRooms;
            name = Name;
        }
    }
}
