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

    public partial class Form2 : Form
    {
        private OleDbConnection connection = new OleDbConnection();

        public Form2(Form1 f1_)
        {
            InitializeComponent();
            connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Projects\\C#\\AccessLoginApp\\AccessLoginApp\\EmployeeInfo.accdb; Persist Security Info=False;";
            txt_welcome.Text = "Welcome " + f1_.userName + "!";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO EmployeeData (FirstName, LastName, Pay) VALUES ('" + txt_fname.Text + "', '" + txt_lname.Text + "', '" + txt_pay.Text + "')";
                command.ExecuteNonQuery();
                MessageBox.Show("Data saved.");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "UPDATE EmployeeData SET FirstName='" + txt_fname.Text + "', LastName='" + txt_lname.Text + "', Pay='" + txt_pay.Text + "' WHERE EID = " + txt_eid.Text;
                command.CommandText = query;
                command.ExecuteNonQuery();
                MessageBox.Show("Data edited.");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "DELETE FROM EmployeeData WHERE EID = " + txt_eid.Text;
                command.CommandText = query;
                command.ExecuteNonQuery();
                MessageBox.Show("Data deleted.");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "SELECT * FROM EmployeeData";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["FirstName"].ToString());
                    listBox1.Items.Add(reader["FirstName"].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "SELECT * FROM EmployeeData where FirstName = '" + comboBox1.Text + "'";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    txt_eid.Text = reader["EID"].ToString();
                    txt_fname.Text = reader["FirstName"].ToString();
                    txt_lname.Text = reader["LastName"].ToString();
                    txt_pay.Text = reader["Pay"].ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "SELECT * FROM EmployeeData where FirstName = '" + listBox1.Text + "'";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    txt_eid.Text = reader["EID"].ToString();
                    txt_fname.Text = reader["FirstName"].ToString();
                    txt_lname.Text = reader["LastName"].ToString();
                    txt_pay.Text = reader["Pay"].ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
            }
        }

        private void btn_loadTable_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "SELECT EID, FirstName, LastName, Pay FROM EmployeeData";
                command.CommandText = query;
                
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
            }

        }

        private void btn_loadChart_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "SELECT * FROM EmployeeData";
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    chart1.Series["Pay"].Points.AddXY(reader["FirstName"].ToString(), reader["Pay"].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txt_eid.Text = row.Cells["EID"].Value.ToString();
                txt_fname.Text = row.Cells["FirstName"].Value.ToString();
                txt_lname.Text = row.Cells["LastName"].Value.ToString();
                txt_pay.Text = row.Cells["Pay"].Value.ToString();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            
            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                cell = selectedCell;
                break;
            }

            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                txt_eid.Text = row.Cells["EID"].Value.ToString();
                txt_fname.Text = row.Cells["FirstName"].Value.ToString();
                txt_lname.Text = row.Cells["LastName"].Value.ToString();
                txt_pay.Text = row.Cells["Pay"].Value.ToString();
            }
        }
    }
}
