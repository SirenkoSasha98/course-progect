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
        public Hotel Kharkiv = new Hotel(2, 8, "Kharkiv");
        public int countofguests=0;
        public int countofroomsononefloor = 4;
        public Form1()
        {
            InitializeComponent();
            GetHotelAndGuests();
            CHeckGuests();
            Updatedata();
            GetGuestReservators();
            comboBox3.SelectedIndex = 0;//выбор этажа4
            textBox4.Text = DateTime.Today.ToString().Substring(0, 10);
            this.tabControl1.SelectedTab = tabPage5;
            DrawHotel();
            SaveDatainFile();
        }


        public void SendTickets(Guest guest, string money)
        {
            using (StreamWriter writer = new StreamWriter("tickets.txt", true))
            {
                writer.WriteLine("Ticket:"+Kharkiv.Name+","+guest.PassportFirstName+ ","+ guest.PassportSecondName + ","+ guest.PassportNumber+","+guest.DataInComing +","+guest.DataofLeave +"," + money );
            }
        }//сохранение квитанций


        public void DrawHotel()
        {
            Pen penblack = new Pen(Color.Black, 1);
            Graphics graphic = pictureBox1.CreateGraphics();
            Rectangle[] rectangles = {new Rectangle(20,10,250,140),new Rectangle(20,180,250,140),new Rectangle(300,10,260,140),new Rectangle(300,180,260,140)};
            graphic.DrawRectangles(penblack,rectangles);
            graphic.DrawRectangle(penblack,20,10,540,310);
            DrawHotelFloor(graphic);
        }
        public void DrawHotelFloor(Graphics graphic)
        {
            Pen penred = new Pen(Color.Red,2);
            Pen pengreen = new Pen(Color.Green, 2);
            int currentFloor;
            if (int.TryParse(comboBox3.Text, out currentFloor))
            {
                label17.Text = (currentFloor - 1) * countofroomsononefloor + 1+"";
                label18.Text = (currentFloor - 1) * countofroomsononefloor + 2 +"";
                label19.Text = (currentFloor - 1) * countofroomsononefloor + 3 + "";
                label20.Text = (currentFloor - 1) * countofroomsononefloor + 4 + "";
                if (Kharkiv.listRoom[int.Parse(label17.Text) - 1].EmptyOrNot)
                {
                    button5_resettle.Visible = true;
                    graphic.DrawRectangle(pengreen, new Rectangle(20, 10, 250, 140));
                    button5_resettle.Text = "Заселиться";
                }
                else
                {
                    button5_resettle.Visible = false;
                    graphic.DrawRectangle(penred, new Rectangle(20, 10, 250, 140));
                }
                if (Kharkiv.listRoom[int.Parse(label18.Text) - 1].EmptyOrNot)
                {
                    button6_resettle.Visible = true;
                    button6_resettle.Text = "Заселиться";
                    graphic.DrawRectangle(pengreen, new Rectangle(300, 10, 260, 140));
                }
                else
                {
                    button6_resettle.Visible = false;
                    graphic.DrawRectangle(penred, new Rectangle(300, 10, 260, 140));
                }
                if (Kharkiv.listRoom[int.Parse(label19.Text) - 1].EmptyOrNot)
                {
                    button7_resettle.Visible = true;
                    button7_resettle.Text = "Заселиться";
                    graphic.DrawRectangle(pengreen, new Rectangle(20, 180, 250, 140));
                }
                else
                {
                    button7_resettle.Visible = false;
                    graphic.DrawRectangle(penred, new Rectangle(20, 180, 250, 140));
                }
                if (Kharkiv.listRoom[int.Parse(label20.Text) - 1].EmptyOrNot)
                {
                    button8_resettle.Visible = true;
                    graphic.DrawRectangle(pengreen, new Rectangle(300, 180, 260, 140));
                    button8_resettle.Text = "Заселиться";
                }
                else
                {
                    button8_resettle.Visible = false;
                    graphic.DrawRectangle(penred, new Rectangle(300, 180, 260, 140));
                }
            }
        }

        public void GetHotelAndGuests()
        {
            StreamReader reader = new StreamReader("Course_Project.txt");
            string[] stringSeparators = new string[] {"thiswasinformatioonabouthotel"};
            string[] Rooms = reader.ReadToEnd().Split(stringSeparators, StringSplitOptions.None);
            string[] EmptyornotRooms = Rooms[0].Split(',');
            bool[] emptynot = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                if (Convert.ToInt32(EmptyornotRooms[i]) == 1) { emptynot[i] = true; }
                else emptynot[i] = false;
            }
            Kharkiv.listRoom.Add(new HotelRoom(1, 1, 1, "Standart", 250, emptynot[0]));
            Kharkiv.listRoom.Add(new HotelRoom(1, 2, 1, "Standart", 250, emptynot[1]));
            Kharkiv.listRoom.Add(new HotelRoom(1, 3, 1, "Luxe", 400, emptynot[2]));
            Kharkiv.listRoom.Add(new HotelRoom(3, 4, 1, "Standart", 400, emptynot[3]));
            Kharkiv.listRoom.Add(new HotelRoom(2, 5, 2, "Standart", 350, emptynot[4]));
            Kharkiv.listRoom.Add(new HotelRoom(2, 6, 2, "Luxe", 500, emptynot[5]));
            Kharkiv.listRoom.Add(new HotelRoom(2, 7, 2, "Luxe", 500, emptynot[6]));
            Kharkiv.listRoom.Add(new HotelRoom(2, 8, 2, "Luxe", 500, emptynot[7]));
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
        }//создание гостиницы
        public void CHeckGuests()
        {
            for (int i = 0; i < Kharkiv.listRoom.Count; i++)
            {
                if (Kharkiv.listRoom[i].Guests!=null && (ConvertInInt(DateTime.Today.ToString().Substring(0, 10)) > ConvertInInt(Kharkiv.listRoom[i].Guests.DataofLeave)))
                {
                    Kharkiv.listRoom[i].EmptyOrNot = true;
                    Kharkiv.listRoom[i].guests = null;
                    countofguests--;
                }
            }
        }
                       
        public void GetGuestReservators()//создание зарезервированных гостей
        {
            using (StreamReader reader = new StreamReader("reservation.txt"))
            {
                string[] stringSeparators = new string[] { ",guest_reservator" };
                string[] Rooms = reader.ReadToEnd().Split(stringSeparators, StringSplitOptions.None);
                string[] datetime;
                for (int i = 0; i < Rooms.Length-1; i++)
                {
                    datetime =  Rooms[i].Split(',');
                    int NumbOfRoom = int.Parse(datetime[0]);
                    for (int j = 1; j < datetime.Length; j++)
                    {
                        string[] dateaboutoneguest = datetime[j].Split(':');
                        Kharkiv.listRoom[NumbOfRoom - 1].guest_reservator.Add(new Guest(dateaboutoneguest[0], dateaboutoneguest[1], dateaboutoneguest[2], NumbOfRoom));
                    }
                }
            }
        }

        public void Updatedata()
        {
            for (int i = 0; i < Kharkiv.listRoom.Count; i++)
            {
                dataGridView2.RowCount = Kharkiv.listRoom.Count + 1;
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = Kharkiv.listRoom[i][j];
                }
            }
            //int countofguests2 = 0;
            int freerooms = 0;
            for (int i = 0; i < Kharkiv.listRoom.Count; i++)
            {
                //dataGridView3.RowCount = countofguests;
                dataGridView1.RowCount = Kharkiv.listRoom.Count - countofguests + 1;
                /*if (Kharkiv.listRoom[i].Guests != null)
                {
                    for (int j = 0; j < dataGridView3.ColumnCount; j++)
                    {
                        if (j == 5) { dataGridView3.Rows[countofguests2].Cells[j].Value = i + 1; }
                        else { dataGridView3.Rows[countofguests2].Cells[j].Value = Kharkiv.listRoom[i].Guests[j]; }
                    }
                    countofguests2++;
                }*/
                //else
                if (Kharkiv.listRoom[i].Guests == null)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        dataGridView1.Rows[freerooms].Cells[j].Value = Kharkiv.listRoom[i][j];
                    }
                    freerooms++;
                }
            }
            UpdateDataGridView4();
        }
        public void ResetTextboxs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            //textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7_cash.Clear();
            textBox4.Text = DateTime.Today.ToString().Substring(0, 10);

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        public bool Check_Name_secondName(string name,string secondName)
        {
            int param;
            bool one = true;
            bool two = true;
            for (int i = 0; i < name.Length; i++)
            {
                if (int.TryParse(name[i].ToString(), out param) || name[i] == ' ')
                {
                    one = false;
                }
            }
            for (int i = 0; i < secondName.Length; i++)
            {
                if (int.TryParse(secondName[i].ToString(), out param) || secondName[i] == ' ')
                {
                    two = false;
                }
            }
            if (one && two) return true;
            return false;
        }
        private void button1_Click(object sender, EventArgs e)//заселение!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            if (Check_Name_secondName(textBox1.Text, textBox2.Text))
            {
                try
                {
                    Kharkiv.listRoom[Convert.ToInt32(textBox6.Text) - 1].Guests = new Guest(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, Convert.ToInt32(textBox6.Text));
                    Kharkiv.listRoom[Convert.ToInt32(textBox6.Text) - 1].EmptyOrNot = false;
                   
                    countofguests++;
                    SendTickets(Kharkiv.listRoom[Convert.ToInt32(textBox6.Text) - 1].Guests,textBox6.Text);
                    Updatedata();
                    ResetTextboxs();
                    SaveDatainFile();   
                }
                catch (FormatException)
                {
                    DialogResult result = MessageBox.Show("Заполните все поля");
                }
                catch (ArgumentOutOfRangeException)
                {
                    DialogResult result = MessageBox.Show("Комната выбрана неправильно");
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Поле \"Имя\" или \"Фамилия\" возможно содержат цифру или пробел");
            }
          //SaveDatainFile();
        }
        private void button2_Click_1(object sender, EventArgs e)//обновление
        {
            Updatedata();
        }

        public void SaveDatainFile()
        {
            using (StreamWriter writer = new StreamWriter("Course_Project.txt", false))
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
        }// about hotel
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDatainFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           // textBox7_search.Clear();
            Updatedata();     
        }
        public void FuncforSearch(int i,int counter,DataGridView datagridview)
        {
            datagridview.Rows.Add();
            for (int j = 0; j < datagridview.ColumnCount; j++)
            {
                if (j == 5) { datagridview.Rows[counter].Cells[j].Value = i + 1; }
                else { datagridview.Rows[counter].Cells[j].Value = Kharkiv.listRoom[i].Guests[j]; }
            }
        }

        public void SerchGuestByData(string data,DataGridView datagridView,TextBox textBox)
        {
            int counter = 0;
            datagridView.Rows.Clear();
            switch (data)
            {
                case "Имя":
                    counter = 0;
                    for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                    {
                        if (Kharkiv.listRoom[i].Guests != null && Kharkiv.listRoom[i].Guests.PassportFirstName == textBox.Text)
                        {
                            FuncforSearch(i, counter, datagridView);
                            counter++;
                        }
                    }
                    break;
                case "Фамилия":
                    counter = 0;
                    for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                    {
                        if (Kharkiv.listRoom[i].Guests != null && Kharkiv.listRoom[i].Guests.PassportSecondName == textBox.Text)
                        {
                            FuncforSearch(i, counter, datagridView);
                            counter++;
                        }
                    }
                    break;
                case "Паспортный код":
                    counter = 0;
                    for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                    {
                        if (Kharkiv.listRoom[i].Guests != null && Kharkiv.listRoom[i].Guests.PassportNumber == textBox.Text)
                        {
                            FuncforSearch(i, counter, datagridView);
                            counter++;
                        }
                    }
                    break;
                case "Дата приезда":
                    counter = 0;
                    for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                    {
                        if (Kharkiv.listRoom[i].Guests != null && Kharkiv.listRoom[i].Guests.DataInComing == textBox.Text)
                        {
                            FuncforSearch(i, counter, datagridView);
                            counter++;
                        }
                    }
                    break;
                case "Дата отъезда":
                    counter = 0;
                    for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                    {
                        if (Kharkiv.listRoom[i].Guests != null && Kharkiv.listRoom[i].Guests.DataofLeave == textBox.Text)
                        {
                            FuncforSearch(i, counter, datagridView);
                            counter++;
                        }
                    }
                    break;
                case "Номер комнаты":
                    counter = 0;
                    for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                    {
                        if (Kharkiv.listRoom[i].Guests != null && Kharkiv.listRoom[i].Guests.NumberofHotelRoom + "" == textBox.Text)
                        {
                            FuncforSearch(i, counter, datagridView);
                            counter++;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        
        //private void textBox7_search_TextChanged(object sender, EventArgs e)//поиск
        //{
        //    SerchGuestByData(comboBox1.Text, dataGridView3, textBox7_search);
        //}



        public bool Check_date(string date1, string date2)
        {
            string[] datacome = date1.Split('.');
            string[] datago = date2.Split('.');
            int param;
            bool one = true;
            bool two = true;
            bool three = true;
            if (int.Parse(datacome[0]) > 31 || int.Parse(datacome[1]) > 12 || int.Parse(datago[0]) > 31 || int.Parse(datago[1]) > 12) three = false;
            for (int i = 0; i < datacome.Length; i++)
            {
                if (!int.TryParse(datacome[i].ToString(), out param))
                {
                    one = false;
                }
            }
            for (int i = 0; i < datago.Length; i++)
            {
                if (!int.TryParse(datago[i].ToString(), out param))
                {
                    two = false;
                }
            }
            if (one && two && three) return true;
            return false;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            int resultnumbroom;
            try
            {
                string[] datacome = textBox4.Text.Split('.');
                string[] datago = textBox5.Text.Split('.');
                if (int.TryParse(textBox6.Text, out resultnumbroom) && (resultnumbroom > Kharkiv.CountofRooms || !Kharkiv.listRoom[resultnumbroom - 1].EmptyOrNot))
                {
                    DialogResult result = MessageBox.Show("Возможно комната под этим номером занята или введено не число.Возможно введено число больше количества комнат");
                }
                else if (!String.IsNullOrEmpty(textBox6.Text))
                {
                   // string[] datacome = textBox4.Text.Split('.');
                   // string[] datago = textBox5.Text.Split('.');
                    if (!Check_date(textBox4.Text, textBox5.Text) || textBox4.Text.Length != 10 || textBox5.Text.Length != 10)
                    {
                        DialogResult result = MessageBox.Show("Дата указана в неверном формате или же присуттвуют нечисловые символы. Возможно указаны несуществующие дни, месяца");
                    }
                    else if (int.Parse(DateTime.Today.ToString().Substring(6, 4)) > int.Parse(datacome[2]) || int.Parse(DateTime.Today.ToString().Substring(6, 4)) > int.Parse(datago[2]))
                    {
                        DialogResult result = MessageBox.Show("Сегодня " + DateTime.Today.ToString() + " Вы указываете дату в прошлом");
                    }
                    else if (!CheckDateForReserve(textBox4.Text, textBox5.Text, resultnumbroom))
                    {
                        DialogResult result = MessageBox.Show("Вы yказываете время, которое уже забронировано. Пожалуйста укажите свободную дату.");
                    }
                    else
                    {
                        int sumofdayliveinhotel = (Convert.ToInt32(datago[2]) - Convert.ToInt32(datacome[2])) * 30 * 12 + (Convert.ToInt32(datago[1]) - Convert.ToInt32(datacome[1])) * 30 + (Convert.ToInt32(datago[0]) - Convert.ToInt32(datacome[0]));
                        int cashtopay = sumofdayliveinhotel * Kharkiv.listRoom[Convert.ToInt32(textBox6.Text) - 1].PriseforDay;
                        if (cashtopay <= 0) { DialogResult result = MessageBox.Show("Неверно указана дата: дата прибытия позже даты отъезда "); }
                        else
                        {
                            textBox7_cash.Text = cashtopay + " грн.";
                        }
                    }
                }

                
            }
            catch (IndexOutOfRangeException)
            {
                DialogResult result = MessageBox.Show("Дата введена в неправильном формате.");
            }
            catch (FormatException)
            {
                DialogResult result = MessageBox.Show("Дата введена в неправильном формате.(нечисловые символы или дата отъезда раньше даты прибития)");
            }
        }
        public int FuncforCountingDatatime(string one, string two, int numberofroom)//вот эта функция есть то зачем я ту написал??
        {
            string[] datacome = one.Split('.');
            string[] datago = two.Split('.');
            int sumofdayliveinhotel = (Convert.ToInt32(datago[2]) - Convert.ToInt32(datacome[2])) * 30 * 12 + (Convert.ToInt32(datago[1]) - Convert.ToInt32(datacome[1])) * 30 + (Convert.ToInt32(datago[0]) - Convert.ToInt32(datacome[0]));
            int cashtopay = sumofdayliveinhotel * Kharkiv.listRoom[numberofroom - 1].PriseforDay;
            return cashtopay;
        }
        public void ChackForGoOut(string text)
        {

        }
        private void button4_goOut_Click(object sender, EventArgs e)
        {
            
             if(!String.IsNullOrEmpty(textBox8_stay.Text) && !String.IsNullOrEmpty(textBox7_goOut.Text)){
                 DialogResult result = MessageBox.Show("Вы продливаете и уезжаете одновременно? Пожалуйста заполните только одно поле.");
             }
             else if(!String.IsNullOrEmpty(textBox8_stay.Text)){
                 string[] text = textBox8_stay.Text.Split(',');
                 int numbofroom = Convert.ToInt32(text[0].ToString());
                 if (numbofroom > Kharkiv.CountofRooms || numbofroom <= 0)
                 {
                     DialogResult result = MessageBox.Show("Комнаты с таким номером нет");
                 }
                 else if (FuncforCountingDatatime(Kharkiv.listRoom[numbofroom - 1].Guests.DataofLeave, text[1], numbofroom) <= 0)
                 {
                     DialogResult result = MessageBox.Show("Введенная Вами дата является недейтсвительной");
                 }
                 else
                 {
                     textBox8_stay.Text = "С Вас " + FuncforCountingDatatime(Kharkiv.listRoom[numbofroom - 1].Guests.DataofLeave, text[1], numbofroom);
                     Kharkiv.listRoom[numbofroom - 1].Guests.DataofLeave = text[1];
                     SendTickets(Kharkiv.listRoom[numbofroom - 1].Guests, textBox8_stay.Text);
                 }
             }
             else if (!String.IsNullOrEmpty(textBox7_goOut.Text))
             {
                 string[] text = textBox7_goOut.Text.Split(',');
                 Kharkiv.listRoom[Convert.ToInt32(text[0]) - 1].EmptyOrNot = true;
                 textBox7_goOut.Text = "Ваши " + FuncforCountingDatatime(text[1], Kharkiv.listRoom[Convert.ToInt32(text[0]) - 1].Guests.DataofLeave, Convert.ToInt32(text[0])); 
                 Kharkiv.listRoom[Convert.ToInt32(text[0]) - 1].Guests = null;
                 Updatedata();
                 SaveDatainFile();
             }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateDataGridView4();
        }
        public void UpdateDataGridView4()
        {
            int countofguests2 = 0;
            for (int i = 0; i < Kharkiv.listRoom.Count; i++)
            {
                dataGridView4.RowCount = countofguests;
                if (Kharkiv.listRoom[i].Guests != null)
                {
                    for (int j = 0; j < dataGridView4.ColumnCount; j++)
                    {
                        if (j == 5) { dataGridView4.Rows[countofguests2].Cells[j].Value = i + 1; }
                        else { dataGridView4.Rows[countofguests2].Cells[j].Value = Kharkiv.listRoom[i].Guests[j]; }
                    }
                    countofguests2++;
                }
            }
        }
        private void textBox7_search2_TextChanged(object sender, EventArgs e)
        {
                SerchGuestByData(comboBox2.Text, dataGridView4, textBox7_search2);
        }


        ///графическое отображение
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Graphics graphic = pictureBox1.CreateGraphics();
            DrawHotelFloor(graphic);
            DrawHotel();
        }




        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchGoOutToday( dataGridView4, comboBox2);
        }
        public void SearchGoOutToday(DataGridView dataGridView,ComboBox comboBox)
        {
            if (comboBox.Text == "Отъезд сегодня")
            {
                dataGridView.Rows.Clear();
                int counter = 0;
                string time = DateTime.Today + "";
                string timetoday = time.Substring(0, 10);
                for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                {
                    if (Kharkiv.listRoom[i].Guests != null && Kharkiv.listRoom[i].Guests.DataofLeave == timetoday)
                    {
                        FuncforSearch(i, counter, dataGridView);
                        counter++;
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // SearchGoOutToday(dataGridView3, comboBox1);
        }
       
        //печать квитанции
        public void PrintCheck(HotelRoom room,string prise)
        {
            using (StreamWriter writer = new StreamWriter("checks.txt", true))
            {
                writer.WriteLine("Квитанция на оплату услуг гостиницы"+Kharkiv.Name);
                writer.WriteLine("Номер №                            "+room.NumberofHotelRoom);
                writer.WriteLine("Этаж                               "+room.NumberofFloor);
                writer.WriteLine("Количество мест в комнате          "+room.NumberofPlace);
                writer.WriteLine("Время проживания          "+room.guests.DataInComing+"-"+room.guests.DataofLeave);
                writer.WriteLine("Цуна:                              "+prise);
                writer.WriteLine(DateTime.Today.ToString());
                writer.WriteLine("------------------------------------");
            }
        }
        //
        
        ///бронирование
        ///

        /////////////////!!!!!!!!!!!!!!!!!хотел написать метод получающий на вход дату и переводящий ее в число

        public void CheckFoReservationDate(string datacom, string dateleave, int numberofroom)//зачем мне эта функция???
        {
            string[] datacome = datacom.Split('.');
            string[] datago = dateleave.Split('.');
            
            if (Kharkiv.listRoom[numberofroom - 1].guest_reservator != null)
            {
                for (int i = 0; i < Kharkiv.listRoom[numberofroom - 1].guest_reservator.Count; i++)
                {
                    //if(Kharkiv.listRoom[numberofroom - 1].guest_reservator.)
                        int sumofdayliveinhotel = (Convert.ToInt32(datago[2]) - Convert.ToInt32(datacome[2])) * 30 * 12 + (Convert.ToInt32(datago[1]) - Convert.ToInt32(datacome[1])) * 30 + (Convert.ToInt32(datago[0]) - Convert.ToInt32(datacome[0]));
                        int cashtopay = sumofdayliveinhotel * Kharkiv.listRoom[numberofroom - 1].PriseforDay;
                        if (cashtopay <= 0) { DialogResult result = MessageBox.Show("Неверно указана дата: дата прибытия позже даты отъезда "); }
                        else
                        {
                            textBox7_reserve.Text = cashtopay + "";
                        }
                }
            }
            //return true;
        }

        public bool CheckDateForReserve(string datecome,string dateleave, int numberofroom)
        {
            
            int counter=0;
            foreach (Guest guest in Kharkiv.listRoom[numberofroom - 1].guest_reservator)
            {
                if ((ConvertInInt(guest.DataInComing) > ConvertInInt(dateleave)) || (ConvertInInt(datecome)>(ConvertInInt(guest.DataofLeave))))
                {
                    counter++;
                }
            }
            if(counter==Kharkiv.listRoom[numberofroom - 1].guest_reservator.Count) return true;
            return false;
        }
        public int ConvertInInt(string date)
        {
            string[] dateday= date.Split('.');
            return  Convert.ToInt32(dateday[2]) * 30 * 12 + Convert.ToInt32(dateday[1]) * 30 + Convert.ToInt32(dateday[0]);
        }

        public void ReservatingRoom(int numberofroom, string name, string dataincome, string datainleave)
        {
            if (!Check_Name_secondName(name, name))//проверка имен
            {
                DialogResult result = MessageBox.Show("Поле \"Имя\" возможно содержат цифру или пробел");

            }
            else if (!Check_date(textBox8_reservetionDatecome.Text, textBox7_reservationDateleave.Text))
            {
                DialogResult result = MessageBox.Show("Ошибка в написании даты.");
            }
            else if (!CheckDateForReserve(dataincome, datainleave, numberofroom))
            {
                DialogResult result = MessageBox.Show("Вы yказываете время, которое уже забронировано. Пожалуйста укажите свободную дату.");
            }
            else if (ConvertInInt(DateTime.Today.ToString().Substring(0, 10)) > ConvertInInt(textBox8_reservetionDatecome.Text))
            {
                DialogResult result = MessageBox.Show("Дата указана в прошлом");
            }
            else
            {
                Kharkiv.listRoom[numberofroom-1].guest_reservator.Add(new Guest(name, dataincome, datainleave, numberofroom));
                CheckFoReservationDate(dataincome,datainleave,numberofroom);
                ReservationRoomSaveinfile();
                ReservatedRoom(numberofroom);
                ResetTextboxinreserve();
            }
        }

        public void ResetTextboxinreserve()
        {
            textBox8_reservetionDatecome.Clear();
            textBox7_reservationDateleave.Clear();
            textBox7_reservtionname.Clear();
            //textBox7_reserve.Clear();
        }
        public void ReservatedRoom(int numberofroom)
        {
            dataGridView3.Rows.Clear();
            dataGridView3.Rows.Add();
            textBox8_roomreservate.Text = numberofroom+"";
            if (Kharkiv.listRoom[numberofroom - 1].guest_reservator.Count == 0)
            {
                dataGridView3.Rows[0].Cells[0].Value = numberofroom;
                dataGridView3.Rows[0].Cells[1].Value = "Этот номер еще никем не забронирован";
            }
            else
            {
                dataGridView3.RowCount = Kharkiv.listRoom[numberofroom - 1].guest_reservator.Count+1;
                for (int i = 0; i < Kharkiv.listRoom[numberofroom - 1].guest_reservator.Count; i++)
                {
                    if (Kharkiv.listRoom[numberofroom - 1].guest_reservator[i] != null)
                    {
                        //for (int j = 0; j < dataGridView3.ColumnCount; j++)
                        //{ 
                            dataGridView3.Rows[i].Cells[0].Value = Kharkiv.listRoom[numberofroom - 1].guest_reservator[i].NumberofHotelRoom;
                            dataGridView3.Rows[i].Cells[1].Value = Kharkiv.listRoom[numberofroom - 1].guest_reservator[i].DataInComing + "-" + Kharkiv.listRoom[numberofroom - 1].guest_reservator[i].DataofLeave;
                            dataGridView3.Rows[i].Cells[2].Value = Kharkiv.listRoom[numberofroom - 1].guest_reservator[i].Name;
                        //}
                    }
                }
            }

            
        }//отображение бронированных номер(а также с какого по какое время)

        private void button5_reserve_Click(object sender, EventArgs e)
        {
            int currentfloor = int.Parse(comboBox3.Text);
            ReservatedRoom((currentfloor-1)*4 +1);
            this.tabControl1.SelectedTab = tabPage3;
        }

        private void button6_reserve_Click(object sender, EventArgs e)
        {
            int currentfloor = int.Parse(comboBox3.Text);
            ReservatedRoom((currentfloor - 1) * 4 + 2);
            this.tabControl1.SelectedTab = tabPage3;
        }

        private void button7_reserve_Click(object sender, EventArgs e)
        {
            int currentfloor = int.Parse(comboBox3.Text);
            ReservatedRoom((currentfloor - 1) * 4 + 3);
            this.tabControl1.SelectedTab = tabPage3;
        }

        private void button8_reserve_Click(object sender, EventArgs e)
        {
            int currentfloor = int.Parse(comboBox3.Text);
            ReservatedRoom((currentfloor - 1) * 4 + 4);
            this.tabControl1.SelectedTab = tabPage3;
        }

        public void ReservationRoomSaveinfile()//ЗАПИСЬ В ФАЙЛ БРОНИ НОМЕРОВ
        {
            using (StreamWriter writer = new StreamWriter("reservation.txt", false))
            {
                for (int i = 0; i < Kharkiv.listRoom.Count; i++)
                {
                    if (Kharkiv.listRoom[i].guest_reservator.Count != 0)
                    {
                        string str = "";
                        for (int j = 0; j < Kharkiv.listRoom[i].guest_reservator.Count; j++)
                        {
                            str += Kharkiv.listRoom[i].guest_reservator[j].Name+":"+Kharkiv.listRoom[i].guest_reservator[j].DataInComing + ":" + Kharkiv.listRoom[i].guest_reservator[j].DataofLeave+",";
                        }
                        str = i + 1 + "," + str+"guest_reservator";
                        writer.Write(str);
                    }
                }
            }
        }

        private void button3_Reservation_Click(object sender, EventArgs e)//кнопка бронирования
        {
              try//a потом нужно посмотреть обновления
              {
                ReservatingRoom(int.Parse(textBox8_roomreservate.Text), textBox7_reservtionname.Text, textBox8_reservetionDatecome.Text, textBox7_reservationDateleave.Text);
                
              }
               catch (FormatException)
              {
                  DialogResult result = MessageBox.Show("Заполнены не все поля");
              }
              catch (ArgumentOutOfRangeException)
              {
                  DialogResult result = MessageBox.Show("Комната выбрана неправильно");
              }
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)//выход из программы
        {
            Application.Exit();
        }

        ///из графики заселение
        private void button5_resettle_Click(object sender, EventArgs e)
        {

            this.tabControl1.SelectedTab = tabPage2;
        }
        private void button6_resettle_Click(object sender, EventArgs e)
        {

            this.tabControl1.SelectedTab = tabPage2;
        }
        private void button7_resettle_Click(object sender, EventArgs e)
        {

            this.tabControl1.SelectedTab = tabPage2;
        }
        private void button8_resettle_Click(object sender, EventArgs e)
        {

            this.tabControl1.SelectedTab = tabPage2;
        }
    }
}
