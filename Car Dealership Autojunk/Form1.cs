using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Car_Dealership_Autojunk
{
    public partial class Form1 : Form
    {
        private SqlDataAdapter _adapter = null;

        private DataTable _table;

        private string _connectionString = null;

        private SqlConnection _sqlConnection;

        private CarDealership _carDealership = new CarDealership();

        public Form1()
        { 
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MovingWindowToCenter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Authorization();
        }

        private async void Authorization()
        {
            SettingConnection Connect = new SettingConnection();

            try
            {
                _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Connect.ConnectionString + "\\AutoJunk.mdf;Integrated Security=True";

                _sqlConnection = new SqlConnection(_connectionString);

                await _sqlConnection.OpenAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Укажите строку подключения.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(!string.IsNullOrEmpty(Login.Text) && !string.IsNullOrEmpty(Password.Text))
            {
                try
                {
                    _adapter = new SqlDataAdapter("SELECT COUNT(*) AS Авторизация FROM [Accounts] WHERE Name = '" + Login.Text + "' AND Password = '" + Password.Text + "' ", _sqlConnection);

                    _table = new DataTable();

                    _adapter.Fill(_table);

                    dataGridView1.DataSource = _table;

                    if (Convert.ToInt32(dataGridView1[0, 0].Value) == 1)
                    {
                        _carDealership.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Неверное имя пользователя или пароль!", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Перейдите в настройки и укажите строку подключения", "Укажите строку подключения.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Поля не могут быть пусты", "Заполните все поля", MessageBoxButtons.OK);
            }
            
        }

        private void MovingWindowToCenter()
        {
            int ScreenWight = Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point((ScreenWight / 2) - (this.Width / 2), (ScreenHeight / 2) - (this.Height / 2));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            _sqlConnection.Close();
        }

        private void SettingConnectionString_Click(object sender, EventArgs e)
        {
            SettingConnection Connect = new SettingConnection();

            Connect.Show();

        }
    }
}
