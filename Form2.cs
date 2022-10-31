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

namespace SamecDatabaseAccess
{
    public partial class Form2 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Form2(string UserName)
        {
            InitializeComponent();
            label5.Text = "Hi! Welcome: "+UserName;
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\GccWin\source\repos\SamecDatabaseAccess\bin\Debug\SamecDatabase.accdb";
            load_table();
            load_combo();
            load_dues();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {                           
                
                //MessageBox.Show(query);
                OleDbDataAdapter da = new OleDbDataAdapter("select * from SamecDB where IDNumber='" +txtIdNumber.Text+ "'",connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("ID Number Already Exist");
                }

                else
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand("insert into SamecDB (IDNumber,LastName,FirstName,MiddleName,BirthDate,BirthPlace,TelNumber,InductionDate) values('" +txtIdNumber.Text +
                                          "','" + txtLastName.Text + "','" + txtFirstName.Text + "','" + txtMiddleName.Text + "','" + dateDob.Text + "','" + txtBplace.Text +
                                          "','" + txtTelNumber.Text + "','" + dateInduction.Text + "')",connection);
                    
                    command.ExecuteNonQuery();
                    connection.Close();
                    txtIdNumber.Text = "";
                    MessageBox.Show("Member Saved");

                }

            }



            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
            load_table();
            load_combo();


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "update SamecDB set LastName='" + txtLastName.Text +
                                      "',FirstName='" + txtFirstName.Text + "',MiddleName='" + txtMiddleName.Text +
                                      "',BirthDate='" + dateDob.Text + "',BirthPlace='" + txtBplace.Text +
                                      "',TelNumber='" + txtTelNumber.Text +
                                      "',InductionDate='" + dateInduction.Text + "' where IDNumber='" + txtIdNumber.Text + "'";
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Member Updated Successfully");
                /*connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "update SamecDB set LastName='" + txtLastName.Text + "' ,FirstName='" + txtFirstName.Text + "' ,MiddleName='" + txtMiddleName.Text +
                               "',BirthDate='" + dateDob.Text + "',BirthPlace='" + txtBplace.Text + "',TelNumber=" + txtTelNumber.Text +
                               ",InductionDate='" + dateInduction.Text + "' where IDNumber=" + txtIdNumber.Text + "";
                MessageBox.Show(query);
                command.CommandText =query;

                command.ExecuteNonQuery();
                MessageBox.Show("Member Edit Successfully");
                connection.Close();
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
            load_table();
            load_combo();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText="delete from SamecDB where IDNumber='"+txtIdNumber.Text+"'";
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Member Deleted Successfully");
                /*connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "delete from SamecDB where IDNumber='"+txtIdNumber.Text+"'";
                //MessageBox.Show(query);
                command.CommandText = query;

                command.ExecuteNonQuery();
                MessageBox.Show("Member Deleted");
                connection.Close();
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
            load_table();
            load_combo();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            /*try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select * from SamecDB";
                //MessageBox.Show(query);
                command.CommandText = query;

                OleDbDataReader reader= command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["LastName"].ToString());
                    listBox1.Items.Add(reader["LastName"].ToString());
                }
                
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }*/
        }
        void load_combo()
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select * from SamecDB";
                //MessageBox.Show(query);
                command.CommandText = query;

                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["LastName"].ToString());
                    listBox1.Items.Add(reader["LastName"].ToString());
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select * from SamecDB where LastName='" + comboBox1.Text + "'";
                //MessageBox.Show(query);
                command.CommandText = query;

                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    txtIdNumber.Text = reader["IDNumber"].ToString();
                    txtLastName.Text = reader["LastName"].ToString();
                    txtFirstName.Text = reader["FirstName"].ToString();
                    txtMiddleName.Text = reader["MiddleName"].ToString();
                    dateDob.Text = reader["BirthDate"].ToString();
                    txtBplace.Text = reader["BirthPlace"].ToString();
                    txtTelNumber.Text = reader["TelNumber"].ToString();
                    dateInduction.Text = reader["InductionDate"].ToString();
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select * from SamecDB where LastName='" + listBox1.Text + "'";
                //MessageBox.Show(query);
                command.CommandText = query;

                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    txtIdNumber.Text = reader["IDNumber"].ToString();
                    txtLastName.Text = reader["LastName"].ToString();
                    txtFirstName.Text = reader["FirstName"].ToString();
                    txtMiddleName.Text = reader["MiddleName"].ToString();
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }
        void load_table()
        {
            try
            {
                connection.Open();
                OleDbCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from SamecDB";
                command.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
                dGViewMembers.DataSource = dt;
                connection.Close();

                /*connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select IDNumber,LastName,FirstName,MiddleName,BirthDate,BirthPlace,TelNumber,InductionDate from SamecDB";
                //MessageBox.Show(query);
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dGViewMembers.DataSource = dt;

                connection.Close();
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);                
            }

        }

        private void BtnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select IDNumber,LastName,FirstName,MiddleName,BirthDate,BirthPlace,TelNumber,InductionDate from SamecDB";
                //MessageBox.Show(query);
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dGViewMembers.DataSource = dt;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           /* if (e.RowIndex>=0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtIdNumber.Text = row.Cells[0].Value.ToString();
                txtLastName.Text = row.Cells[1].Value.ToString();
                txtFirstName.Text = row.Cells[2].Value.ToString();
                txtMiddleName.Text = row.Cells[3].Value.ToString();
                
            }*/
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            foreach(DataGridViewCell selectedCell in dGViewMembers.SelectedCells)
            {
                cell =selectedCell;
                break;
            }
            if (cell!=null)
            {
                DataGridViewRow row = cell.OwningRow;
                txtIdNumber.Text = row.Cells[0].Value.ToString();
                txtLastName.Text = row.Cells[1].Value.ToString();
                txtFirstName.Text = row.Cells[2].Value.ToString();
                txtMiddleName.Text = row.Cells[3].Value.ToString();
                dateDob.Text = row.Cells[4].Value.ToString();
                txtBplace.Text = row.Cells[5].Value.ToString();
                txtTelNumber.Text = row.Cells[6].Value.ToString();
                dateInduction.Text = row.Cells[7].Value.ToString();
            }
            
                

            
        }

        private void dateInduction_ValueChanged(object sender, EventArgs e)
        {
                     
        }
        void load_dues()
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select * from tblDues";
                //MessageBox.Show(query);
                command.CommandText = query;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dGridViewDues.DataSource = dt;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {            
            DataTable dt = new DataTable();            
            if (txtSearchIDNumber.Text.Length>0)
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from SamecDB where IDNumber like '" + txtSearchIDNumber.Text + "%'", connection);
                da.Fill(dt);
            }
            else if (txtSearchLastName.Text.Length > 0)
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from SamecDB where LastName like '" + txtSearchLastName.Text + "%'", connection);
                da.Fill(dt);
            }
            else if (txtSearchTelNumber.Text.Length > 0)
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from SamecDB where TelNumber like '" + txtSearchTelNumber.Text + "%'", connection);
                da.Fill(dt);
            }
            dGViewMembers.DataSource = dt;
        }

        private void txtSearchIDNumber_TextChanged(object sender, EventArgs e)
        {
            txtSearchLastName.Text = "";
            txtSearchTelNumber.Text = "";
        }

        private void txtSearchLastName_TextChanged(object sender, EventArgs e)
        {
            txtSearchTelNumber.Text = "";
            txtSearchIDNumber.Text = "";
        }

        private void txtSearchTelNumber_TextChanged(object sender, EventArgs e)
        {
            txtSearchIDNumber.Text = "";
            txtSearchLastName.Text = "";
        }
    }
}
