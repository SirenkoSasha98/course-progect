using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class HotelRoom
    {
        private bool emptyOrNot;
        private int numberofPlace;//двухспальные, одно,а может и больше
        private int numberofHotelRoom;//номер комнаты 331 например
        private int numberofFloor;
        private string classofRoom;//luxe or standart
        private int priseforDay;
        public Guest guests;
        public string this[int n]{
            get
            {
                if (n == 5)
                {
                    if (emptyOrNot) return "Свободный";
                    else return "Занятый";
                }
                else if (n == 0)
                {
                    return "" + NumberofPlace;
                }
                else if (n == 1)
                {
                    return NumberofHotelRoom + "";
                }
                else if (n == 2)
                {
                    return NumberofFloor + "";
                }
                else if (n == 3)
                {
                    return ClassofRoom;
                }
                else if (n == 4)
                {
                    return PriseforDay + "";
                }
                else
                {
                    return "";
                }
            }
        }
        public int PriseforDay
        {
            set {priseforDay = value; }
            get {return priseforDay;}
        }
        public Guest Guests
        {
            set { guests = value; }
            get { return guests; }
        }
        public bool EmptyOrNot{
            set { emptyOrNot = value; }
            get { return emptyOrNot; }
        }
        public int NumberofPlace
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
        public HotelRoom(int NumberofPlace, int NumberofHotelRoom,int NumberofFloor, string ClassofRoom,int prise, bool emptyornot)
        {
            emptyOrNot = emptyornot;
            numberofPlace = NumberofPlace;
            numberofHotelRoom = NumberofHotelRoom;
            //if (ClassofRoom != "Standart" || ClassofRoom!="Luxe") throw new //exception
            numberofFloor = NumberofFloor;
            classofRoom = ClassofRoom;
            priseforDay = prise;
            guests = null;
        }
    }
}
