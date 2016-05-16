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

        public Form1()
        {
            InitializeComponent();
            //Kharkiv = new Hotel(2, 6, "Kharkiv");
            Kharkiv.listRoom.Add(new HotelRoom(1, 1, 1, "Standart", 250));
            Kharkiv.listRoom.Add(new HotelRoom(1, 2, 1, "Standart", 250));
            Kharkiv.listRoom.Add(new HotelRoom(1, 3, 1, "Luxe", 400));
            Kharkiv.listRoom.Add(new HotelRoom(3, 4, 2, "Standart", 400));
            Kharkiv.listRoom.Add(new HotelRoom(2, 5, 2, "Standart", 350));
            Kharkiv.listRoom.Add(new HotelRoom(2, 6, 2, "Luxe", 500));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*StreamReader reader = new StreamReader("111.txt");
            string[] Room;
            string[] Rooms = reader.ReadToEnd().Split('\n');
            for (int i = 0; i < Rooms.Length; i++)
            {
                Room = Rooms[i].Split(' ');
                dataGridView2.RowCount = Rooms.Length;
                for(int j = 0;j<dataGridView2.ColumnCount;j++){
                    dataGridView2.Rows[i].Cells[j].Value = Room[j];
                }
            }*/
             for (int i = 0; i < Kharkiv.listRoom.Count; i++)
            {
                //Room = Rooms[i].Split(' ');
                dataGridView2.RowCount = Kharkiv.listRoom.Count;
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = Kharkiv.listRoom[i][j%6];
                }
            }

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kharkiv.listRoom[Convert.ToInt32(textBox6.Text)].Guests = new Guest(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
            Kharkiv.listRoom[Convert.ToInt32(textBox6.Text)].EmptyOrNot = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < Kharkiv.listRoom.Count; i++)
            {
                //Room = Rooms[i].Split(' ');
                dataGridView2.RowCount = Kharkiv.listRoom.Count;
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = Kharkiv.listRoom[i][j%6];
                }
            }


        }
    }
}

