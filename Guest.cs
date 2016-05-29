using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class Guest
    {
        private string passportFirstName;
        private string passportSecondName;
        private string passportNumber;
        private string dataInComing;
        private string dataofLeave;
        private int numberofHotelRoom;
        public string Name;
        public string this[int n]{
            get
            {
                if (n == 0) return passportFirstName;
                if (n == 1) return passportSecondName;
                if (n == 2) return passportNumber;
                if (n == 3) return dataInComing;
                if (n == 4) return dataofLeave;
                if (n == 5) return numberofHotelRoom+"";
                return "";
            }
        }
        public int NumberofHotelRoom
        {
            set { numberofHotelRoom = value; }
            get { return numberofHotelRoom; }
        }
        public string PassportFirstName
        {
            get { return passportFirstName; }
        }
          public string PassportSecondName
        {
            get { return passportSecondName; }
        }
          public string PassportNumber
        {
            get { return passportNumber; }
        }
        public string DataInComing
        {
            get { return dataInComing; }
        }
        public string DataofLeave
        {
            set { dataofLeave = value; }
            get { return dataofLeave; }
        }
        public Guest(string PassportFirstName, string PassportSecondName, string PassportNumber, string DataInComing, string DataofLeave, int numberofroom)
        {
            passportFirstName = PassportFirstName;
            passportSecondName = PassportSecondName;
            passportNumber = PassportNumber;
            dataInComing = DataInComing;
            dataofLeave = DataofLeave;
            numberofHotelRoom = numberofroom;
        }
        public Guest(string Name, string DataInComing, string DataofLeave, int numberofroom)
        {
            this.Name = Name;
            dataInComing = DataInComing;
            dataofLeave = DataofLeave;
            numberofHotelRoom = numberofroom;
        }
    }
}
