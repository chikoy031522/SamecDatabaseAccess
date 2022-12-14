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
    public partial class Form1 : Form
    {
        private OleDbConnection connection = new OleDbConnection();

        public Form1()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\GccWin\source\repos\SamecDatabaseAccess\bin\Debug\SamecDatabase.accdb";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                txtConnection.Text = "CONNECTED";
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "select * from tblLogin where UserName='" + txtUsername.Text + "' and Password='" + txtPassword.Text + "'";
            OleDbDataReader reader= command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = count + 1;
            }
            if (count == 1)
            {
                //MessageBox.Show("Username and password is correct");
                connection.Close();
                connection.Dispose();
                this.Hide();
                Form2 f2 = new Form2(txtUsername.Text);
                f2.ShowDialog();
            }
            else if (count > 1)
            {
                MessageBox.Show("Duplicate Username and password");
            }
            else
            {
                MessageBox.Show("Please Enter Correct Username and Password");
            }
                connection.Close();
        }
    }
}
