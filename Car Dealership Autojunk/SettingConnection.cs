using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Car_Dealership_Autojunk.Properties;

namespace Car_Dealership_Autojunk
{
    public partial class SettingConnection : Form
    {
        public string ConnectionString
        {
            get
            {
                return textBox1.Text;
            }
        }

        public SettingConnection()
        {
            InitializeComponent();
            textBox1.Text = Settings.Default["StringWay"].ToString(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Default["StringWay"] = textBox1.Text;
            Settings.Default.Save();
            textBox1.Text = Settings.Default["StringWay"].ToString();
        }

        private void SettingConnection_Load(object sender, EventArgs e)
        {

        }
    }
}
