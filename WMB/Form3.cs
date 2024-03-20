using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WMB
{
    public partial class Form3 : Form
    {


        // SqlConnection объект для установки соединения с базой данных
        SqlConnection connection;

        public Form3()
        {
            InitializeComponent();
            // Подключение к базе данных
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\kosty\\Documents\\DB.mdf;Integrated Security=True;Connect Timeout=30"); // Замените <connection_string> на ваше соединение
            //connection = new SqlConnection("Data Source=DESKTOP-O0A12TV\\SQLEXPRESS;Initial Catalog=DB;Integrated Security=True;Encrypt=True"); // Замените <connection_string> на ваше соединение

            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);

            

           
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Grib". При необходимости она может быть перемещена или удалена.
            //this.gribTableAdapter.Fill(this.dBDataSet.Grib);
            // Заполнение ComboBox данными из таблицы Grib
            string query = "SELECT Name FROM Berry";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox2.Items.Add(reader["Name"].ToString());//comboBox2.Items.Add(reader["Name"].ToString());
                
            }

            connection.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            /////////Цвет:Начало2
            if (label3.Text == "Нет")
            {
                label3.ForeColor = Color.Red;
                label4.ForeColor = Color.Red;
            }
            else if (label3.Text == "Да")
            {
                label3.ForeColor = Color.Green;
                label4.ForeColor = Color.Green;
            }
            /////////Цвет:Конец2

            string selectedItem = comboBox2.SelectedItem.ToString();
        
            pictureBox1.Image = GetImageByName(selectedItem);
            pictureBox2.Image = GetImageByName2(selectedItem);

            // Получение выбранного элемента из ComboBox
            string selectedGrib = comboBox2.SelectedItem.ToString();
            //MessageBox.Show(selectedGrib);
            // Запрос для получения информации о выбранном элементе из таблицы Grib
            string query = "SELECT Name, Des, Food FROM Berry WHERE Name = @Name";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", selectedGrib);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                // Вывод информации в label
               label1.Text = reader["Name"].ToString();
               label2.Text = reader["Des"].ToString();
               label3.Text = reader["Food"].ToString();
            }

         connection.Close();
        }

        private Image GetImageByName(string name)
        {
            // Получаем текущую сборку, в которой находится класс
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Создаем экземпляр ResourceManager для доступа к ресурсам внутри сборки
            ResourceManager resourceManager = new ResourceManager(assembly.GetName().Name + ".Properties.Resources", assembly);

            // Ищем ресурс с именем, соответствующим выбранному элементу в comboBox
            object resourceObject = resourceManager.GetObject(name + "1");

            if (resourceObject != null && resourceObject is Image)
            {
                return (Image)resourceObject;
            }

            return null; // Если ресурс не найден, возвращаем null
        }
        private Image GetImageByName2(string name)
        {
            // Получаем текущую сборку, в которой находится класс
            Assembly assembly2 = Assembly.GetExecutingAssembly();

            // Создаем экземпляр ResourceManager для доступа к ресурсам внутри сборки
            ResourceManager resourceManager2 = new ResourceManager(assembly2.GetName().Name + ".Properties.Resources", assembly2);

            // Ищем ресурс с именем, соответствующим выбранному элементу в comboBox
            object resourceObject2 = resourceManager2.GetObject(name + "11");

            if (resourceObject2 != null && resourceObject2 is Image)
            {
                return (Image)resourceObject2;
            }

            return null; // Если ресурс не найден, возвращаем null
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }
        
        }
    }

