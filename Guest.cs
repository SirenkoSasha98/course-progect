using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    class Guest
    {
        private string passportFirstName;
        private string passportSecondName;
        private string passportNumber;
        private string dataInComing;
        private string dataofLeave;
        private int numberofHotelRoom;
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
        public Guest(string PassportFirstName, string PassportSecondName, string PassportNumber, string DataInComing, string DataofLeave)
        {
            passportFirstName = PassportFirstName;
            passportSecondName = PassportSecondName;
            passportNumber = PassportNumber;
            dataInComing = DataInComing;
            dataofLeave = DataofLeave;
        }
    }
}
