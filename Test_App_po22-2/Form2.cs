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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Test_App_po22_2
{
    public partial class Form2 : System.Windows.Forms.Form
    {
        private int correctAnswersCount = 0; // Переменная для отслеживания количества правильных ответов
        private int currentQuestionIndex = 0; // Индекс текущего вопроса
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadQuestion(); // Загрузка первого вопроса при загрузке формы
        }

        private void LoadQuestion()
        {
            // Создаем соединение с базой данных
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-386MA7J\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"))
            {
                // Открываем соединение
                connection.Open();

                // Создаем SQL-запрос для выборки вопроса и вариантов ответов из таблиц
                string query = "SELECT q.QuestionText, a.AnswerText " +
                               "FROM Questions q " +
                               "JOIN Answers a ON q.QuestionID = a.QuestionID " +
                               "WHERE q.QuestionID = @QuestionID";

                // Создаем объект SqlCommand для выполнения запроса
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Добавляем параметр @QuestionID к запросу
                    command.Parameters.AddWithValue("@QuestionID", currentQuestionIndex + 1);
                    label2.Text = "Вопрос №" + (1 + currentQuestionIndex).ToString();
                    // Выполняем запрос и получаем результаты
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Если есть данные
                        {
                            // Отображаем вопрос
                            label1.Text = reader["QuestionText"].ToString();

                            // Список для хранения вариантов ответов
                            List<string> answerOptions = new List<string>();

                            // Добавляем первый вариант ответа в список
                            answerOptions.Add(reader["AnswerText"].ToString());

                            // Считываем остальные варианты ответов из результата запроса
                            while (reader.Read())
                            {
                                // Добавляем следующий вариант ответа в список
                                answerOptions.Add(reader["AnswerText"].ToString());
                            }

                            // Перемешиваем список вариантов ответов
                            answerOptions = Shuffle(answerOptions);

                            // Присваиваем тексты вариантов ответов кнопкам
                            button1.Text = answerOptions[0];
                            button2.Text = answerOptions[1];
                            button3.Text = answerOptions[2];
                            button4.Text = answerOptions[3];
                        }
                        else
                        {
                            // Если вопросов больше нет, можно выполнить какое-то действие, например, закрыть форму
                            MessageBox.Show("Больше нет вопросов.");
                            this.Close();
                        }
                    }
                }
            }
        }


        // Метод для перемешивания списка
        private List<T> Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentQuestionIndex++; // Увеличиваем индекс текущего вопроса
            LoadQuestion(); // Загружаем следующий вопрос
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}