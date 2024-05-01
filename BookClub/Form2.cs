using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookClub
{
    public partial class Form2 : Form
    {
       
        public static Guid id;
        DataBase dataBase = new DataBase();
        SqlCommand command;
        Form1 log { get { return this.Owner as Form1; } }
        public Form2()
        {
            InitializeComponent();

        }
        public Form2(Guid guid)
        {
            InitializeComponent();
            id = guid;

        }
        public void loadBook()
        {
            dataBase.openConnection();
            string query = $"SELECT  Id_book,Title as Название, Author as Автор FROM Book";
            SqlDataAdapter adapter = new SqlDataAdapter(query, dataBase.getConnection());
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Columns[0].Visible = false;
            dataBase.closeConnection();
            dataGridView1.ClearSelection();
        }

        public void loadReadBooks()
        {
            dataBase.openConnection();
            string query = $"SELECT  ReadBooks.read_id,Book.Title as Название, Book.Author as Автор FROM ReadBooks inner join Book on ReadBooks.Id_book=Book.Id_book Where Id_user='{id}'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, dataBase.getConnection());
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.Columns[0].Visible = false;
            dataBase.closeConnection();
            dataGridView2.ClearSelection();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
           
            loadBook();
            loadReadBooks();
            dataBase.openConnection();
            string loginuery = $"Select Username From Users where Id_user='{id}'";
            SqlCommand cmd1 = new SqlCommand(loginuery, dataBase.getConnection());
            label1.Text = cmd1.ExecuteScalar().ToString();
            dataBase.closeConnection();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Получаем значение ячейки с ID (предполагая, что ID находится в первой ячейке)
            Guid bookId = Guid.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            string sql = $"INSERT INTO ReadBooks(Id_user,Id_book) VALUES ('{id}','{bookId}')";
            dataBase.openConnection();
            command = new SqlCommand(sql, dataBase.getConnection());
            command.ExecuteNonQuery();
            dataBase.closeConnection();
            loadReadBooks();
            tabControl1.SelectedTab = tabPage2;
            MessageBox.Show("Книга прочитана");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Получаем значение ячейки с ID (предполагая, что ID находится в первой ячейке)
            Guid readId = Guid.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            string sql = $"DELETE FROM ReadBooks where read_id='{readId}'";
            dataBase.openConnection();
            command = new SqlCommand(sql, dataBase.getConnection());
            command.ExecuteNonQuery();
            dataBase.closeConnection();
            loadReadBooks();
            MessageBox.Show("Прочитанная книга удалена");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.ToLower(); // Получаем текст из TextBox и убираем пробелы в начале и конце
            if (string.IsNullOrEmpty(searchText))
            {
                // Если поисковый запрос пуст, то сбрасываем фильтрацию и отображаем все записи в DataGridView
                dataGridView1.ClearSelection();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Visible = true;
                }
            }
            else
            {
                // Иначе выполняем фильтрацию поисковым запросом
                dataGridView1.ClearSelection();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    bool isVisible = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchText))
                        {
                            isVisible = true;
                            break;
                        }
                    }
                    row.Visible = isVisible;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox2.Text.ToLower(); // Получаем текст из TextBox и убираем пробелы в начале и конце
            if (string.IsNullOrEmpty(searchText))
            {
                // Если поисковый запрос пуст, то сбрасываем фильтрацию и отображаем все записи в DataGridView
                dataGridView2.ClearSelection();
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    row.Visible = true;
                }
            }
            else
            {
                // Иначе выполняем фильтрацию поисковым запросом
                dataGridView2.ClearSelection();
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    bool isVisible = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchText))
                        {
                            isVisible = true;
                            break;
                        }
                    }
                    row.Visible = isVisible;
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверяем, если выбрана первая вкладка
            if (tabControl1.SelectedIndex == 1)
            {
                // Выводим сообщение
                MessageBox.Show("Почитай книгу и почувствуй вкус Bounty");
            }
        }
    }
}
