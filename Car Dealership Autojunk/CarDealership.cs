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
    public partial class CarDealership : Form
    {
        private SqlDataAdapter _adapter = null;

        private DataTable _table;

        private string _connectionString = null;

        private SqlConnection _sqlConnection;

        

        public CarDealership()
        {
            InitializeComponent();
        }

        private async void CarDealership_Load(object sender, EventArgs e)
        {

            SettingConnection Connect = new SettingConnection();

            try
            {
                _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Connect.ConnectionString + "\\AutoJunk.mdf;Integrated Security=True";

                _sqlConnection = new SqlConnection(_connectionString);

                await _sqlConnection.OpenAsync();

                DonwloadDataBase("SELECT Id AS Номер, CarBrand AS [Марка машины], CarModel AS [Модель машины], CarSupller AS Поставщик, CountryManufacturer AS [Страна производитель] FROM [TradingFloor]");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Укажите строку подключения.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarDealership_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void DonwloadDataBase(string SqlRequest)
        {

            _adapter = new SqlDataAdapter(SqlRequest, _sqlConnection);

            _table = new DataTable();

            _adapter.Fill(_table);

            showDataBase.DataSource = _table;
        }

        private void ShowTableWorkers_Click(object sender, EventArgs e)
        {
            DonwloadDataBase("SELECT Id AS Номер, NameWorker AS Имя, SecondNameWorker AS Фамилия, PatronymicWorker AS Отчество, PostWorker AS Должность FROM [Workers]");
        }

        private void ShowTableBuyers_Click(object sender, EventArgs e)
        {
            DonwloadDataBase("SELECT Id AS Номер, BuyerName AS Имя, BuyerSecondName AS Фамилия, BuyerPatronymic AS Отчество, PassportSeries AS [Серия паспорта], PassportNumber AS [Номер паспорта], NumberPhone AS [Номер телефона] FROM [Buyers]");
        }

        private void ShowTableOrders_Click(object sender, EventArgs e)
        {
            DonwloadDataBase("SELECT Id AS Номер, BuyerName AS Имя, BuyerSecondName AS Фамилия, PhoneNumber AS [Номер телефона], OrderPrice AS Цена, CarModel AS Модель, CarColor AS Цвет, Quantity AS Количество FROM [OrderCar]");
        }

        private void ShowTableTradingFloor_Click(object sender, EventArgs e)
        {
            DonwloadDataBase("SELECT Id AS Номер, CarBrand AS [Марка машины], CarModel AS [Модель машины], CarSupller AS Поставщик, CountryManufacturer AS [Страна производитель] FROM [TradingFloor]");
        }

        private void ShowTableCars_Click(object sender, EventArgs e)
        {
            DonwloadDataBase("SELECT Id AS Номер, CarModel AS [Модель машины] , EngineCapacity AS [Объём двигателя], CarPrice AS Цена, CarRetailPrice AS [Розничная цена], YearRelease AS [Год выпуска] FROM [Cars]");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) == false) return;

            if (e.KeyChar == Convert.ToChar(Keys.Back)) return;

            e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) == true) return;

            if (e.KeyChar == Convert.ToChar(Keys.Back)) return;

            e.Handled = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                Size = new Size(816, 441);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                Size = new Size(816, 354);
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                Size = new Size(816, 337);
            } 
            else if (tabControl1.SelectedIndex == 4)
            {
                Size = new Size(816, 361);
            }
            else
            {
                Size = new Size(816, 489);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text)
                && !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrEmpty(textBox7.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO OrderCar (BuyerName, BuyerSecondName, PhoneNumber, OrderPrice, CarModel, CarColor, Quantity) " +
                    "VALUES(@Name,@SecondName,@Phone,@Price,@Model,@Color,@Count)", _sqlConnection);

                command.Parameters.AddWithValue("Name", textBox1.Text);
                command.Parameters.AddWithValue("SecondName", textBox2.Text);
                command.Parameters.AddWithValue("Phone", textBox3.Text);
                command.Parameters.AddWithValue("Price", textBox4.Text);
                command.Parameters.AddWithValue("Model", textBox5.Text);
                command.Parameters.AddWithValue("Color", textBox6.Text);
                command.Parameters.AddWithValue("Count", textBox7.Text);

                await command.ExecuteNonQueryAsync();

                textBox1.Text = null;
                textBox2.Text = null;
                textBox3.Text = null;
                textBox4.Text = null;
                textBox5.Text = null;
                textBox6.Text = null;
                textBox7.Text = null;

                MessageBox.Show("Запись успешно добавлена!", "Успешно!");
            }
            else
            {
                MessageBox.Show("Поля не могу быть пустыми", "Заполните все поля", MessageBoxButtons.OK);
            }
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrEmpty(textBox10.Text)
            && !string.IsNullOrEmpty(textBox11.Text) && !string.IsNullOrEmpty(textBox12.Text) && !string.IsNullOrEmpty(textBox13.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Buyers (BuyerName, BuyerSecondName, BuyerPatronymic, PassportSeries, PassportNumber, NumberPhone) " +
                "VALUES (@Name, @SecondName, @Patronymic, @Series, @Number, @Phone)", _sqlConnection);

                command.Parameters.AddWithValue("Name", textBox8.Text);
                command.Parameters.AddWithValue("SecondName", textBox9.Text);
                command.Parameters.AddWithValue("Patronymic", textBox10.Text);
                command.Parameters.AddWithValue("Series", textBox11.Text);
                command.Parameters.AddWithValue("Number", textBox12.Text);
                command.Parameters.AddWithValue("Phone", textBox13.Text);

                await command.ExecuteNonQueryAsync();

                textBox8.Text = null;
                textBox9.Text = null;
                textBox10.Text = null;
                textBox11.Text = null;
                textBox12.Text = null;
                textBox13.Text = null;

                MessageBox.Show("Запись успешно добавлена!", "Успешно!"); 
            }
            else
            {
                MessageBox.Show("Поля не могу быть пустыми", "Заполните все поля", MessageBoxButtons.OK);
            }
        }

        private async void button3_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox14.Text) && !string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrEmpty(textBox16.Text)
            && !string.IsNullOrEmpty(textBox17.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO TradingFloor (CarBrand, CarModel, CarSupller, CountryManufacturer)" +
                    " VALUES (@Brand, @Model, @Supller, @Manufacturer)", _sqlConnection);

                command.Parameters.AddWithValue("Brand", textBox14.Text);
                command.Parameters.AddWithValue("Model", textBox15.Text);
                command.Parameters.AddWithValue("Supller", textBox16.Text);
                command.Parameters.AddWithValue("Manufacturer", textBox17.Text);

                await command.ExecuteNonQueryAsync();

                TextBoxCleaner(textBox14, textBox15, textBox16, textBox17);

                MessageBox.Show("Запись успешно добавлена!", "Успешно!");
            }
            else
            {
                MessageBox.Show("Поля не могу быть пустыми", "Заполните все поля", MessageBoxButtons.OK);
            }
        }

        private void TextBoxCleaner(params TextBox[] textBoxes)
        {
            for (int i = 0; i < textBoxes.Length; i++)
            {
                textBoxes[i].Text = null;
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox18.Text) && !string.IsNullOrEmpty(textBox19.Text) && !string.IsNullOrEmpty(textBox20.Text)
            && !string.IsNullOrEmpty(textBox21.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Workers(NameWorker, SecondNameWorker, PatronymicWorker, PostWorker) " +
                    "VALUES (@Name, @SecondName, @Patronymic, @Post)", _sqlConnection);

                command.Parameters.AddWithValue("Name", textBox18.Text);
                command.Parameters.AddWithValue("SecondName", textBox19.Text);
                command.Parameters.AddWithValue("Patronymic", textBox20.Text);
                command.Parameters.AddWithValue("Post", textBox21.Text);

                await command.ExecuteNonQueryAsync();

                TextBoxCleaner(textBox18, textBox19, textBox20, textBox21);

                MessageBox.Show("Запись успешно добавлена!", "Успешно!");
            }
            else
            {
                MessageBox.Show("Поля не могу быть пустыми", "Заполните все поля", MessageBoxButtons.OK);
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox22.Text) && !string.IsNullOrEmpty(textBox23.Text) && !string.IsNullOrEmpty(textBox24.Text)
            && !string.IsNullOrEmpty(textBox25.Text) && !string.IsNullOrEmpty(textBox26.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Cars(CarModel, EngineCapacity, CarPrice, CarRetailPrice, YearRelease) " +
                    "VALUES (@Model, @Capacity, @Price, @RetailPrice, @Release)", _sqlConnection);

                command.Parameters.AddWithValue("Model", textBox22.Text);
                command.Parameters.AddWithValue("Capacity", textBox23.Text);
                command.Parameters.AddWithValue("Price", textBox24.Text);
                command.Parameters.AddWithValue("RetailPrice", textBox25.Text);
                command.Parameters.AddWithValue("Release", textBox26.Text);

                await command.ExecuteNonQueryAsync();

                TextBoxCleaner(textBox22, textBox23, textBox24, textBox25, textBox26);

                MessageBox.Show("Запись успешно добавлена!", "Успешно!");
            }
            else
            {
                MessageBox.Show("Поля не могу быть пустыми", "Заполните все поля", MessageBoxButtons.OK);
            }

        }

        private async void button10_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox52.Text) && !string.IsNullOrEmpty(textBox53.Text) && !string.IsNullOrEmpty(textBox54.Text)
            && !string.IsNullOrEmpty(textBox55.Text) && !string.IsNullOrEmpty(textBox56.Text) && !string.IsNullOrEmpty(textBox57.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE Cars SET CarModel = @Model, EngineCapacity = @Engine, CarPrice = @Price, CarRetailPrice = @RetailPrice," +
                    " YearRelease = @Release WHERE Id = @Id", _sqlConnection);

                command.Parameters.AddWithValue("Id", textBox52.Text);
                command.Parameters.AddWithValue("Model", textBox53.Text);
                command.Parameters.AddWithValue("Engine", textBox54.Text);
                command.Parameters.AddWithValue("Price", textBox55.Text);
                command.Parameters.AddWithValue("RetailPrice", textBox56.Text);
                command.Parameters.AddWithValue("Release", textBox57.Text);

                await command.ExecuteNonQueryAsync();

                TextBoxCleaner(textBox52, textBox53, textBox54, textBox55, textBox56, textBox57);

                MessageBox.Show("Запись успешно добавлена!", "Успешно!");
            }
            else
            {
                MessageBox.Show("Поля не могу быть пустыми", "Заполните все поля", MessageBoxButtons.OK);
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl2.SelectedIndex == 1)
            {
                Size = new Size(816, 419);
            }
            else if(tabControl2.SelectedIndex == 2)
            {
                Size = new Size(816, 350); 
            }
            else if (tabControl2.SelectedIndex == 3)
            {
                Size = new Size(816, 335); 
            }
            else if (tabControl2.SelectedIndex == 4)
            {
                Size = new Size(816, 367);
            }
            else
            {
                Size = new Size(816, 489);
            }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox27.Text) && !string.IsNullOrEmpty(textBox28.Text) && !string.IsNullOrEmpty(textBox29.Text)
            && !string.IsNullOrEmpty(textBox30.Text) && !string.IsNullOrEmpty(textBox26.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE OrderCar SET BuyerName = @Name, BuyerSecondName = @SecondName, PhoneNumber = @Phone, OrderPrice = @Price, " +
                "CarModel = @Model, CarColor = @Color, Quantity = @Count WHERE Id = @Id", _sqlConnection);

                command.Parameters.AddWithValue("Id", textBox27.Text);
                command.Parameters.AddWithValue("Name", textBox28.Text);
                command.Parameters.AddWithValue("SecondName", textBox29.Text);
                command.Parameters.AddWithValue("Phone", textBox30.Text);
                command.Parameters.AddWithValue("Price", textBox31.Text);
                command.Parameters.AddWithValue("Model", textBox32.Text);
                command.Parameters.AddWithValue("Color", textBox33.Text);
                command.Parameters.AddWithValue("Count", textBox34.Text);

                await command.ExecuteNonQueryAsync();

                TextBoxCleaner(textBox27, textBox28, textBox29, textBox30, textBox31, textBox32, textBox33, textBox34);

                MessageBox.Show("Запись успешно Изменена!", "Успешно!"); 
            }
            else
            {
                MessageBox.Show("Поля не могу быть пустыми", "Заполните все поля", MessageBoxButtons.OK);
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox50.Text) && !string.IsNullOrEmpty(textBox42.Text) && !string.IsNullOrEmpty(textBox43.Text)
            && !string.IsNullOrEmpty(textBox44.Text) && !string.IsNullOrEmpty(textBox45.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE TradingFloor SET CarBrand = @Brand, CarModel = @Model, CarSupller = @Supller, " +
                    "CountryManufacturer = @Manufacturer WHERE Id = @Id", _sqlConnection);

                command.Parameters.AddWithValue("Id", textBox50.Text);
                command.Parameters.AddWithValue("Brand", textBox42.Text);
                command.Parameters.AddWithValue("Model", textBox43.Text);
                command.Parameters.AddWithValue("Supller", textBox44.Text);
                command.Parameters.AddWithValue("Manufacturer", textBox45.Text);

                await command.ExecuteNonQueryAsync();
                
                MessageBox.Show("Запись успешно Изменена!", "Успешно!");

                TextBoxCleaner(textBox50, textBox42, textBox43, textBox44, textBox45);
            }
            else
            {
                MessageBox.Show("Поля не могу быть пустыми", "Заполните все поля", MessageBoxButtons.OK);
            }
        }

        private async  void button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox35.Text) && !string.IsNullOrEmpty(textBox36.Text) && !string.IsNullOrEmpty(textBox37.Text)
            && !string.IsNullOrEmpty(textBox38.Text) && !string.IsNullOrEmpty(textBox39.Text) && !string.IsNullOrEmpty(textBox40.Text) && !string.IsNullOrEmpty(textBox41.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE Buyers SET BuyerName = @Name, BuyerSecondName = @SecondName, BuyerPatronymic = @Patronymic," +
                    "PassportSeries = @Series, PassportNumber = @Number, NumberPhone = @Phone WHERE Id = @Id ", _sqlConnection);

                command.Parameters.AddWithValue("Id", textBox35.Text);
                command.Parameters.AddWithValue("Name", textBox36.Text);
                command.Parameters.AddWithValue("SecondName", textBox37.Text);
                command.Parameters.AddWithValue("Patronymic", textBox38.Text);
                command.Parameters.AddWithValue("Series", textBox39.Text);
                command.Parameters.AddWithValue("Number", textBox40.Text);
                command.Parameters.AddWithValue("Phone", textBox41.Text);

                await command.ExecuteNonQueryAsync();

                MessageBox.Show("Запись успешно Изменена!", "Успешно!");

                TextBoxCleaner(textBox35, textBox36, textBox37, textBox38, textBox39, textBox40, textBox41);
            }
            else
            {
                MessageBox.Show("Поля не могу быть пустыми", "Заполните все поля", MessageBoxButtons.OK);
            }
        }

        private void Panel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Panel.SelectedIndex == 0)
            {
                Size = new Size(816, 489);
            }
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox51.Text) && !string.IsNullOrEmpty(textBox46.Text) && !string.IsNullOrEmpty(textBox47.Text)
            && !string.IsNullOrEmpty(textBox48.Text) && !string.IsNullOrEmpty(textBox49.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE Workers SET NameWorker = @Name, SecondNameWorker = @SecondName, PatronymicWorker = @Patronymic, PostWorker " +
                    "= @Post WHERE Id = @Id", _sqlConnection);

                command.Parameters.AddWithValue("Id", textBox51.Text);
                command.Parameters.AddWithValue("Name", textBox46.Text);
                command.Parameters.AddWithValue("SecondName", textBox47.Text);
                command.Parameters.AddWithValue("Patronymic", textBox48.Text); 
                command.Parameters.AddWithValue("Post", textBox49.Text);

                await command.ExecuteNonQueryAsync();

                MessageBox.Show("Запись успешно Изменена!", "Успешно!");

                TextBoxCleaner(textBox46, textBox47, textBox48, textBox49, textBox51);
            }
            else
            {
                MessageBox.Show("Поля не могу быть пустыми", "Заполните все поля", MessageBoxButtons.OK);
            }
        }
    }
}
