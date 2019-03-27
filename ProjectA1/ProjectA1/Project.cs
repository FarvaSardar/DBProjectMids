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
    public partial class Project : Form
    {
        SqlConnection con = new SqlConnection("Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True; MultipleActiveResultSets=true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;
        public Project()
        {
            InitializeComponent();
        }
        string conStr = "Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True; MultipleActiveResultSets=true";
        private void Project_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projectDataSet.Project' table. You can move, or remove it, as needed.
            this.projectTableAdapter.Fill(this.projectDataSet.Project);
            // TODO: This line of code loads data into the 'projectADataSet4.Evaluation' table. You can move, or remove it, as needed.
            //this.projectTableAdapter.Fill(this.projectADataSet2.Project);

        }

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Project", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            
            ID = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                string query1 = "insert into Project(Description, Title) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "')";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dbr1;
                try
                {
                    dbr1 = cmd1.ExecuteReader();
                    MessageBox.Show("saved");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    
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
            string query = "Select * from Project";
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
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" )
            {
                cmd = new SqlCommand("update Project set Description=@desc,Title=@title where ID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@desc", textBox1.Text);
                cmd.Parameters.AddWithValue("@title", textBox2.Text);
               
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

        private void button4_Click(object sender, EventArgs e)
        {
        //    if (ID != 0)
        //    {
        //        cmd = new SqlCommand("delete Project where ID=@id", con);
        //        SqlCommand cmd1 = new SqlCommand("delete GroupProject where ID=@id", con);
        //        con.Open();
        //        cmd1.Parameters.AddWithValue("@id", ID);
        //        cmd1.ExecuteNonQuery();
        //        cmd.Parameters.AddWithValue("@id", ID);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Main m = new Main();
            m.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            int Id1 = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            if (e.ColumnIndex == 3)
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string query1 = "Delete from GroupProject where ProjectId = @Id1 ";
                    string query2 = "Delete from Project where Id = @Id1 ";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    this.dataGridView1.Rows.RemoveAt(e.RowIndex);
                    cmd1.Parameters.Add(new SqlParameter("@Id1", Id1));
                    cmd1.ExecuteNonQuery();
                    cmd2.Parameters.Add(new SqlParameter("@Id1", Id1));
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
            }

            if (e.ColumnIndex == 2)
            {
                //comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                //comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                //dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();

            }
        }
    }
}
   