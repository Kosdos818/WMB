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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WMB
{
    public partial class Form2 : Form
    {
        
        
        // SqlConnection объект для установки соединения с базой данных
        SqlConnection connection;

        public Form2()
        {
            InitializeComponent();
            // Подключение к базе данных
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\kosty\\Documents\\DB.mdf;Integrated Security=True;Connect Timeout=30"); // Замените <connection_string> на ваше соединение
            //connection = new SqlConnection("Data Source=DESKTOP-O0A12TV\\SQLEXPRESS;Initial Catalog=DB;Integrated Security=True;Encrypt=True"); // Замените <connection_string> на ваше соединение

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);


        }



        private void Form2_Load(object sender, EventArgs e)//Form1_Load
        {
            
            // Заполнение ComboBox данными из таблицы Grib
            string query = "SELECT Name FROM Grib";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader["Name"].ToString());
            }

            connection.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /////////Цвет:Начало2
            if (label4.Text == "Нет")
            {
                label2.ForeColor = Color.Red;
                label4.ForeColor = Color.Red;
            }
            else if (label4.Text == "Да")
            {
                label2.ForeColor = Color.Green;
                label4.ForeColor = Color.Green;
            }
            /////////Цвет:Конец2

            string selectedItem = comboBox1.SelectedItem.ToString();

            pictureBox1.Image = GetImageByName(selectedItem);
            pictureBox2.Image = GetImageByName1(selectedItem);

            // Получение выбранного элемента из ComboBox
            string selectedGrib = comboBox1.SelectedItem.ToString();
            //MessageBox.Show(selectedGrib);
            // Запрос для получения информации о выбранном элементе из таблицы Grib
            string query = "SELECT Name, Des, Food FROM Grib WHERE Name = @Name";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", selectedGrib);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                // Вывод информации в TextBox
                label5.Text = reader["Name"].ToString();
                label6.Text = reader["Des"].ToString();
                label4.Text = reader["Food"].ToString();
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
            object resourceObject = resourceManager.GetObject(name + "11");

            if (resourceObject != null && resourceObject is Image)
            {
                return (Image)resourceObject;
            }

            return null; // Если ресурс не найден, возвращаем null
        }
        private Image GetImageByName1(string name1)
        {
            // Получаем текущую сборку, в которой находится класс
            Assembly assembly1 = Assembly.GetExecutingAssembly();

            // Создаем экземпляр ResourceManager для доступа к ресурсам внутри сборки
            ResourceManager resourceManager1 = new ResourceManager(assembly1.GetName().Name + ".Properties.Resources", assembly1);

            // Ищем ресурс с именем, соответствующим выбранному элементу в comboBox
            object resourceObject1 = resourceManager1.GetObject(name1 + "22");

            if (resourceObject1 != null && resourceObject1 is Image)
            {
                return (Image)resourceObject1;
            }

            return null; // Если ресурс не найден, возвращаем null
        }
    }
}
