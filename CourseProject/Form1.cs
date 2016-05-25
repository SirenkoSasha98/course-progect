using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CourseProject
{
    public partial class Form1 : Form
    {
        public Hotel Kharkiv = new Hotel(2, 6, "Kharkiv");
        public int countofguests=0;
        public Form1()
        {
            InitializeComponent();
            GetHotelAndGuests();
            Updatedata();
        }
        public void GetHotelAndGuests()
        {
            StreamReader reader = new StreamReader("Course_Project.txt");
            string[] stringSeparators = new string[] {"thiswasinformatioonabouthotel"};
            string[] Rooms = reader.ReadToEnd().Split(stringSeparators, StringSplitOptions.None);
            string[] EmptyornotRooms = Rooms[0].Split(',');
            bool[] emptynot = new bool[6];
            for (int i = 0; i < 6; i++)
            {
                if (Convert.ToInt32(EmptyornotRooms[i]) == 1) { emptynot[i] = true; }
                else emptynot[i] = false;
            }
            Kharkiv.listRoom.Add(new HotelRoom(1, 1, 1, "Standart", 250, emptynot[0]));
            Kharkiv.listRoom.Add(new HotelRoom(1, 2, 1, "Standart", 250, emptynot[1]));
            Kharkiv.listRoom.Add(new HotelRoom(1, 3, 1, "Luxe", 400, emptynot[2]));
            Kharkiv.listRoom.Add(new HotelRoom(3, 4, 2, "Standart", 400, emptynot[3]));
            Kharkiv.listRoom.Add(new HotelRoom(2, 5, 2, "Standart", 350, emptynot[4]));
            Kharkiv.listRoom.Add(new HotelRoom(2, 6, 2, "Luxe", 500, emptynot[5]));
            string[] Separators = new string[] {"Guest"};
            string[] guests = Rooms[1].Split(Separators, StringSplitOptions.None);
            //Console.WriteLine(String.IsNullOrWhiteSpace(guests[t]));
            for (int y = 0; y < guests.Length-1;y++ )
            {
                countofguests++;
                if (y - 1 > Kharkiv.listRoom.Count) break; 
                string[] guest = guests[y].Split(',');
                Console.WriteLine(guest.Length);
                Kharkiv.listRoom[Convert.ToInt32(guest[5]) - 1].guests = new Guest(guest[0], guest[1], guest[2], guest[3], guest[4], Convert.ToInt32(guest[5]));
                Kharkiv.listRoom[Convert.ToInt32(guest[5]) - 1].EmptyOrNot = false;
                
            }
            reader.Close();
        }
        public void Updatedata()
        {
            for (int i = 0; i < Kharkiv.listRoom.Count; i++)
            {
                dataGridView2.RowCount = Kharkiv.listRoom.Count;
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = Kharkiv.listRoom[i][j];
                }
            }
            int countofguests2 = 0;
            int freerooms = 0;
            for (int i = 0; i < Kharkiv.listRoom.Count; i++)
            {
                dataGridView3.RowCount = countofguests;
                dataGridView1.RowCount = Kharkiv.listRoom.Count - countofguests;
                if (Kharkiv.listRoom[i].Guests != null)
                {
                    for (int j = 0; j < dataGridView3.ColumnCount; j++)
                    {
                        if (j == 5) { dataGridView3.Rows[countofguests2].Cells[j].Value = i + 1; }
                        else { dataGridView3.Rows[countofguests2].Cells[j].Value = Kharkiv.listRoom[i].Guests[j]; }
                    }
                    countofguests2++;
                }
                else
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        dataGridView1.Rows[freerooms].Cells[j].Value = Kharkiv.listRoom[i][j];
                    }
                    freerooms++;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//заселение
        {
            Kharkiv.listRoom[Convert.ToInt32(textBox6.Text) - 1].Guests = new Guest(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, Convert.ToInt32(textBox6.Text));
            Kharkiv.listRoom[Convert.ToInt32(textBox6.Text)-1].EmptyOrNot = false;
            countofguests++;

            string[] datacome = textBox4.Text.Split('.');
            string[] datago = textBox5.Text.Split('.');
            int sumofdayliveinhotel = (Convert.ToInt32(datago[2]) - Convert.ToInt32(datacome[2]))*30*12 + (Convert.ToInt32(datago[1]) - Convert.ToInt32(datacome[1]))*30 + (Convert.ToInt32(datago[0]) - Convert.ToInt32(datacome[0]));
            int cashtopay = sumofdayliveinhotel * Kharkiv.listRoom[Convert.ToInt32(textBox6.Text) - 1].PriseforDay;
            textBox7_cash.Text = cashtopay + " грн.";
            Updatedata();
        }

        private void button2_Click_1(object sender, EventArgs e)//обновление
        {
            Updatedata();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("222.txt", false))
            {
                for (int i = 0; i < Kharkiv.CountofRooms; i++)
                {
                    if (i == Kharkiv.CountofRooms - 1)
                    {
                        if (Kharkiv.listRoom[i].EmptyOrNot) writer.Write("1");
                        else writer.Write("0");
                    }
                    else
                    {
                        if (Kharkiv.listRoom[i].EmptyOrNot) writer.Write("1,");
                        else writer.Write("0,");
                    }
                }
                writer.Write("thiswasinformatioonabouthotel");
                for (int i = 0; i < Kharkiv.CountofRooms; i++)
                {
                    if (!Kharkiv.listRoom[i].EmptyOrNot)
                    {
                        for (int t = 0; t < 6; t++)
                        {
                            if (t == 5) { writer.Write(Kharkiv.listRoom[i].Guests[t]); }
                            else { writer.Write(Kharkiv.listRoom[i].Guests[t] + ","); }
                        }
                        writer.Write("Guest");
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Updatedata();
            Form1 myForm = new Form1();
            myForm.Show();
        }
        public void FuncforSearch(int i,int counter)
        {
            for (int j = 0; j < dataGridView3.ColumnCount; j++)
            {
                dataGridView3.Rows.Add();
                if (j == 5) { dataGridView3.Rows[counter].Cells[j].Value = i + 1; }
                else { dataGridView3.Rows[counter].Cells[j].Value = Kharkiv.listRoom[i].Guests[j]; }
            }
        }

        private void textBox7_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Equals(Keys.Enter))
            {
                int counter = 0;
                switch (comboBox1.Text)
                {

                    case "Имя":
                        textBox7_search.ResetText();
                        counter = 0;
                        for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                        {
                            if (Kharkiv.listRoom[i].Guests != null && Kharkiv.listRoom[i].Guests.PassportFirstName == textBox7_search.Text)
                            {
                                FuncforSearch(i, counter);
                                counter++;
                            }
                        }
                        break;
                    case "Фамилия":
                        textBox7_search.ResetText();
                        counter = 0;
                        for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                        {
                            if (Kharkiv.listRoom[i].Guests != null && Kharkiv.listRoom[i].Guests.PassportFirstName == textBox7_search.Text)
                            {
                                FuncforSearch(i, counter);
                                counter++;
                            }
                        }
                        break;
                    case "Паспортный код":
                        textBox7_search.ResetText();
                        counter = 0;
                        for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                        {
                            if (Kharkiv.listRoom[i].Guests != null && Kharkiv.listRoom[i].Guests.PassportFirstName == textBox7_search.Text)
                            {
                                FuncforSearch(i, counter);
                                counter++;
                            }
                        }
                        break;
                    case "Дата приезда":
                        textBox7_search.ResetText();
                        counter = 0;
                        for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                        {
                            if (Kharkiv.listRoom[i].Guests != null && Kharkiv.listRoom[i].Guests.PassportFirstName == textBox7_search.Text)
                            {
                                FuncforSearch(i, counter);
                                counter++;
                            }
                        }
                        break;
                    case "Дата отъезда":
                        counter = 0;
                        textBox7_search.ResetText();
                        for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                        {
                            if (Kharkiv.listRoom[i].Guests != null && Kharkiv.listRoom[i].Guests.PassportFirstName == textBox7_search.Text)
                            {
                                FuncforSearch(i, counter);
                                counter++;
                            }
                        }
                        break;
                    case "Номер комнаты":
                        counter = 0;
                        textBox7_search.ResetText();
                        for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                        {
                            if (Kharkiv.listRoom[i].Guests != null && Kharkiv.listRoom[i].Guests.PassportFirstName == textBox7_search.Text)
                            {
                                FuncforSearch(i, counter);
                                counter++;
                            }
                        }
                        break;
                    default:
                        textBox7_search.Text = "Выберите категорию";
                        break;

                }
            }
        }

        private void textBox7_search_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
