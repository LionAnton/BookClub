using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BookClub
{
    public partial class Form1 : Form
    {
        DataBase dataBase=new DataBase();
        DataTable table;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loginUser = log.Text;
            var passUser = pass.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            table = new DataTable();
            dataBase.openConnection();
            SqlCommand command = new SqlCommand($"Select Id_user, Username From Users Where Username=N'{loginUser}'", dataBase.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
                if (table.Rows.Count == 1)
                {
                    Form2 form2 = new Form2(Guid.Parse(table.Rows[0][0].ToString()));
                    //Form2 form2 = new Form2();
                    form2.Owner = this;
                    form2.Show();
                    this.Hide();
                    log.Text = "";
                    pass.Text = "";
                }
                else
                {
                    // Логин не существует, создаем нового пользователя
                    string newUsername = log.Text.Trim();
                    string newPassword = pass.Text.Trim();

                    // Здесь можно выполнить операции добавления нового пользователя в базу данных
                    // Например, использовать SQL-запрос для вставки новой записи в таблицу пользователей
                    string sql = $"Insert into Users(Username,Password) VALUES(N'{newUsername}',N'{newPassword}')";
                    dataBase.openConnection();
                    command = new SqlCommand(sql, dataBase.getConnection());
                    command.ExecuteNonQuery();
                    dataBase.closeConnection();
                    // Выводим сообщение с новым логином и паролем
                    MessageBox.Show($"Создан новый пользователь:\nЛогин: {newUsername}\nПароль: {newPassword}");

                    // Очищаем поля ввода
                    log.Text = "";
                    pass.Text = "";
                }
           
        }
    }
}
