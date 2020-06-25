using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace AccessLoginApp
{
    public partial class Form1 : Form
    {
        public string userName { get; private set; }
        private OleDbConnection connection = new OleDbConnection();

        public Form1()
        {
            InitializeComponent();
            connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Projects\\C#\\AccessLoginApp\\AccessLoginApp\\EmployeeInfo.accdb; Persist Security Info=False;";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                checkConnectionLabel.Text = "Connected";
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM EmployeeData WHERE Username='" + txt_username.Text + "' AND Password = '" + txt_password.Text + "'";
            OleDbDataReader reader = command.ExecuteReader();

            int count = 0;

            while (reader.Read())
            {
                count = count + 1;
            }

            if (count == 1)
            {
                userName = txt_username.Text;

                connection.Close();
                connection.Dispose();
                this.Hide();
                Form2 f2 = new Form2(this);
                f2.ShowDialog();
                this.Show();
            }

            else if (count > 1)
            {
                MessageBox.Show("Duplicate entry.");
            }
            else
            {
                MessageBox.Show("Username or password incorrect.");
            }
            
            connection.Close();
        }
    }
}
