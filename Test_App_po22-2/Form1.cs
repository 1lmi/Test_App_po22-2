using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Test_App_po22_2
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private string ConnectionString = @"Data Source=DESKTOP-386MA7J\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e) // кнопка для перехода к тесту
        {

            // Сохраняем данные ученика
            string name = studentName.Text;
            string surname = textBox2.Text;
            string group = comboBox1.Text;

            // Проверяем, заполнены ли все поля
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname) && !string.IsNullOrEmpty(group))
            {
                // Создаем соединение с базой данных
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    // Открываем соединение
                    connection.Open();

                    // Создаем SQL-запрос для вставки данных в таблицу Person_info
                    string query = "INSERT INTO [dbo].[Person_info] (Name, Family, PersonGroup) VALUES (@Name, @Family, @PersonGroup)";

                    // Создаем объект SqlCommand для выполнения запроса
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Добавляем параметры к запросу
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Family", surname);
                        command.Parameters.AddWithValue("@PersonGroup", group);

                        // Выполняем запрос
                        command.ExecuteNonQuery();
                    }
                }

                // Переходим на новую форму
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide(); // Скрываем текущую форму
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e) //имя
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) //фамилия
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //выбор группы
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDataSet.Group". При необходимости она может быть перемещена или удалена.
            this.groupTableAdapter.Fill(this.testDataSet.Group);

        }
    }
}
