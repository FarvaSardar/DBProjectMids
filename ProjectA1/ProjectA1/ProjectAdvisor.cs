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

namespace ProjectA1
{
    public partial class ProjectAdvisor : Form
    {
        SqlConnection con = new SqlConnection("Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True;");
       
        public ProjectAdvisor()
        {
            InitializeComponent();
        }

        string conStr = "Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True";


        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void ProjectAdvisor_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projectAdvisorDataSet.ProjectAdvisor' table. You can move, or remove it, as needed.
            this.projectAdvisorTableAdapter1.Fill(this.projectAdvisorDataSet.ProjectAdvisor);
            // TODO: This line of code loads data into the 'projectAAdvisor.ProjectAdvisor' table. You can move, or remove it, as needed.
            //this.projectAdvisorTableAdapter.Fill(this.ProjectAdvisorDataSet.ProjectAdvisor);


            SqlConnection con = new SqlConnection(conStr);
            string query = "select Id from Advisor";
            string query1 = "select Id from Project";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataReader dbr, dbr1;

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
            con.Close();




            try
            {
                con.Open();
                dbr1 = cmd1.ExecuteReader();
                while ( dbr1.Read())
                {
                    comboBox2.Items.Add(dbr1[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);


            bool isExists = false;
            con.Open();
            string query2 = "SELECT AdvisorId , count(AdvisorId) FROM ProjectAdvisor group by AdvisorId having count(AdvisorId) >= 3 ";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            SqlDataReader dbr = cmd2.ExecuteReader();
            while (dbr.Read())
            {
                string id = comboBox2.Text;
                if (id == Convert.ToString(dbr[0]))
                {
                    isExists = true;
                    MessageBox.Show("Project ID already exixts. Cannot add data again corresponding to that ID.");
                    comboBox2.SelectedItem = null;
                    break;
                }
            }
            con.Close();

            bool isExistss = false;
            con.Open();
            string query3 = "SELECT ProjectId , count(ProjectId) FROM ProjectAdvisor group by ProjectId having count(ProjectId) >= 3";
            SqlCommand cmd3 = new SqlCommand(query3, con);
            SqlDataReader dbrr = cmd3.ExecuteReader();
            while (dbrr.Read())
            {
                string id = comboBox1.Text;
                if (id == Convert.ToString(dbrr[1]))
                {
                    isExistss = true;
                    MessageBox.Show("Advisor ID already exixts. Cannot add data again corresponding to that ID.");
                    comboBox1.SelectedItem = null;
                    break;
                }
            }
            con.Close();


            con.Open();
            if (!isExistss && !isExists)
            {
                string query1 = "insert into ProjectAdvisor(AdvisorId, ProjectId, AdvisorRole, AssignmentDate) values ( '" + comboBox1.Text + "' , '" + comboBox2.Text + "',(select Id from Lookup where Value= '" + comboBox3.Text + "' ),  '" + (dateTimePicker1.Value) + "') ";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dbr1;
                try
                {
                    dbr1 = cmd1.ExecuteReader();
                    MessageBox.Show("Project Advisor assigned successfully.");
                    comboBox1.SelectedItem = null;
                    comboBox2.SelectedItem = null;
                    comboBox3.SelectedItem = null;
                    dateTimePicker1.Value = DateTimePicker.MinimumDateTime;
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

            con.Open();
            dataGridView1.Show();
            //SqlConnection con = new SqlConnection(conStr);
            string query = "Select * from ProjectAdvisor";
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

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            SqlConnection con = new SqlConnection(conStr);
            string query = "Select * from ProjectAdvisor";
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bool isExists = false;
            //SqlConnection con = new SqlConnection(conStr);
            //con.Open();
            //string query = "Select * from ProjectAdvisor";
            //SqlCommand cmd = new SqlCommand(query, con);
            //SqlDataReader dbr = cmd.ExecuteReader();
            //while (dbr.Read())
            //{
            //    string id = comboBox1.Text;
            //    if (id == Convert.ToString(dbr[0]))
            //    {
            //        isExists = true;
            //        MessageBox.Show("ID already exixts. Cannot add data again corresponding to that ID.");
            //        comboBox1.Items[comboBox1.SelectedIndex] = string.Empty;
            //        //comboBox1.SelectedIndex = -1;
            //        break;
            //    }
            //}
            //con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //string id = comboBox1.Text;
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            string query = "update ProjectAdvisor set AdvisorId = '" + this.comboBox1.Text + "' , ProjectId = '" + this.comboBox2.Text + "'  ";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record is updated successfully.");

            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            comboBox3.SelectedItem = null;
            dateTimePicker1.Value = DateTimePicker.MinimumDateTime;
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from ProjectAdvisor", sqlcon);
                DataTable t = new DataTable();
                sqlDa.Fill(t);
                dataGridView1.DataSource = t;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            int Id2 = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            int Id1= Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            if (e.ColumnIndex == 5)
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    
                    string query1 = "Delete from ProjectAdvisor where ProjectId = @Id2 AND AdvisorId= @Id1 ";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    this.dataGridView1.Rows.RemoveAt(e.RowIndex);
                    cmd1.Parameters.Add(new SqlParameter("@Id2", Id2));
                    cmd1.Parameters.Add(new SqlParameter("@Id1", Id1));
                    cmd1.ExecuteNonQuery();
                    comboBox1.SelectedItem = null;
                    comboBox2.SelectedItem = null;
                    comboBox3.SelectedItem = null;
                    dateTimePicker1.Value = DateTimePicker.MinimumDateTime;
                    con.Close();
                }
            }

            if (e.ColumnIndex == 4)
            {
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();

            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Main m = new Main();
            m.Show();
            this.Hide();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value == DateTimePicker.MinimumDateTime)
            {
                dateTimePicker1.Value = DateTime.Now; // This is required in order to show current month/year when user reopens the date popup.
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = " ";
            }
            else
            {
                dateTimePicker1.Format = DateTimePickerFormat.Short;
            }
        }
    }
}
