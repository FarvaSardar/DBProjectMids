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
using System.Text.RegularExpressions;

namespace ProjectA1
{
    public partial class Student : Form
    {
        SqlConnection con = new SqlConnection("Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;

        public Student()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        string conStr = "Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True";
        private void Students_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projectADataSet7.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter1.Fill(this.projectADataSet7.Student);

            comboBox1.Items.Remove(comboBox1.SelectedItem);
            SqlConnection con = new SqlConnection(conStr);
            string query = "select Id from Person";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dbr;

            try
            {
                con.Open();
                dbr = cmd.ExecuteReader();
                while (dbr.Read())
                {
                    comboBox1.Items.Add(dbr[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Student", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void ClearData()
        {
           // comboBox1.ValueMember = "";
            textBox1.Text = "";
            ID = 0;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                string query1 = "insert into Student(Id, RegistrationNo) values ('"+ comboBox1.Text +"', '"+ textBox1.Text.ToString() + "' )";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dbr1;
                try
                {
                    dbr1 = cmd1.ExecuteReader();
                    MessageBox.Show("saved");
                    comboBox1.Text = "";
                    textBox1.Text = "";
                    while (dbr1.Read())
                    {
                    }
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
            }
            con.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            SqlConnection con = new SqlConnection(conStr);
            string query = "Select * from Student";
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource source = new BindingSource();
                source.DataSource = dt;
                dataGridView1.DataSource = source;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isExists = false;
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            string query = "Select * from Student";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dbr = cmd.ExecuteReader();
            while (dbr.Read())
            {
                string id = comboBox1.Text;
                if(id == Convert.ToString(dbr[0]))
                {
                    isExists = true;
                    MessageBox.Show("ID already exixts. Cannot add data again corresponding to that ID.");
                    comboBox1.Items[comboBox1.SelectedIndex] = string.Empty;
                    //comboBox1.SelectedIndex = -1;
                    break;
                }
            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            int Id1 = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            if (e.ColumnIndex == 2)
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    this.dataGridView1.Rows.RemoveAt(e.RowIndex);
                    string query1 = "Delete from Student where Id = @Id1";
                    string query2 = "Delete from GroupStudent where StudentId = @Id1";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd1.Parameters.Add(new SqlParameter("@Id1", Id1));
                    cmd2.Parameters.Add(new SqlParameter("@Id1", Id1));
                    cmd2.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
            }

            if (e.ColumnIndex == 2)
            {
                //comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main m = new Main();
            m.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Person p = new Person();
            p.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" )
            {
                cmd = new SqlCommand("update Student set RegistrationNo=@regno where ID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@regno", textBox1.Text);
               
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Edited Successfully");
                con.Close();
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
        //    if (ID != 0)
        //    {
        //        cmd = new SqlCommand("delete Student where ID=@id", con);
        //        SqlCommand cmd1 = new SqlCommand("delete GroupStudent where ID=@id", con);
        //        con.Open();
        //        cmd1.Parameters.AddWithValue("@id", ID);
        //        cmd.Parameters.AddWithValue("@id", ID);
        //        cmd1.ExecuteNonQuery();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //        MessageBox.Show("Record Deleted Successfully!");
        //        DisplayData();
        //        ClearData();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please Select Record to Delete");
        //    }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            textBox1.MaxLength = 13;
        }
    }
}
