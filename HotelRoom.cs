using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    class HotelRoom
    {
        private bool emptyOrNot;
        private int numberofPlace;//двухспальные, одно,а может и больше
        private int numberofHotelRoom;//номер комнаты 331 например
        private int numberofFloor;
        private string classofRoom;
        private Guest guests;
        public Guest Guests
        {
            set { guests = value; }
            get { return guests; }
        }
        public bool EmptyOrNot{
            set { emptyOrNot = value; }
            get { return emptyOrNot; }
        }
        public int NumberofPalce
        {
            get { return numberofPlace; }
        }
        public int NumberofHotelRoom{
            get { return numberofHotelRoom; }
        }
        public int NumberofFloor
        {
            get { return numberofFloor; }
        }
        public string ClassofRoom
        {
            get { return classofRoom; }
        }
        public HotelRoom(int NumberofPlace, int NumberofHotelRoom,int NumberofFloor, string ClassofRoom )
        {
            emptyOrNot = true;
            numberofPlace = NumberofPlace;
            numberofHotelRoom = NumberofHotelRoom;
           //if (ClassofRoom != "Standart" || ClassofRoom!="Luxe") throw new //exception
            numberofFloor = NumberofFloor;
            classofRoom = ClassofRoom;
            guest = null;
        }
    }
}
