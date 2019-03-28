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
    public partial class GroupProject : Form
    {
        SqlConnection con = new SqlConnection("Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True;");

        public GroupProject()
        {
            InitializeComponent();
        }
        string conStr = "Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True";

        private void GroupProject_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'groupProjectDataSet.GroupProject' table. You can move, or remove it, as needed.
            this.groupProjectTableAdapter.Fill(this.groupProjectDataSet.GroupProject);

            SqlConnection con = new SqlConnection(conStr);
            string query = "select Id from [Group]";
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
                    comboBox2.Items.Add(dbr[0]);
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
                while (dbr1.Read())
                {
                    comboBox1.Items.Add(dbr1[0]);
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
            string query2 = "Select * from GroupProject";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            SqlDataReader dbr = cmd2.ExecuteReader();
            while (dbr.Read())
            {
                string id = comboBox1.Text;
                if (id == Convert.ToString(dbr[0]))
                {
                    isExists = true;
                    MessageBox.Show("Project ID already exixts. Cannot add data again corresponding to that ID.");
                    comboBox1.SelectedItem = null;
                    break;
                }
            }
            con.Close();



            bool isExistss = false;
            con.Open();
            string query3 = "Select * from GroupProject";
            SqlCommand cmd3 = new SqlCommand(query3, con);
            SqlDataReader dbrr = cmd3.ExecuteReader();
            while (dbrr.Read())
            {
                string id = comboBox2.Text;
                if (id == Convert.ToString(dbrr[1]))
                {
                    isExistss = true;
                    MessageBox.Show("Group ID already exixts. Cannot add data again corresponding to that ID.");
                    comboBox2.SelectedItem = null;
                    break;
                }
            }
            con.Close();


            con.Open();
            if (!isExists && !isExistss)
            {

                string query1 = "insert into GroupProject(ProjectId, GroupId, AssignmentDate) values ( '" +comboBox1.Text+ "' , '" + comboBox2.Text + "','" +Convert.ToDateTime(dateTimePicker1.Value) + "') ";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dbr1;
                try
                {
                    dbr1 = cmd1.ExecuteReader();
                    MessageBox.Show("saved");
                    comboBox1.SelectedItem = null;
                    comboBox2.SelectedItem = null;
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
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Main m = new Main();
            m.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            SqlConnection con = new SqlConnection(conStr);
            string query = "Select * from GroupProject";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            int Id1 = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            int Id2 = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);

            if (e.ColumnIndex == 4)
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {


                    string query1 = "Delete from GroupProject where ProjectId = @Id1 AND GroupId= @Id2 ";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    this.dataGridView1.Rows.RemoveAt(e.RowIndex);
                    cmd1.Parameters.Add(new SqlParameter("@Id2", Id2));
                    cmd1.Parameters.Add(new SqlParameter("@Id1", Id1));
                    cmd1.ExecuteNonQuery();

                    comboBox1.SelectedItem = null;
                    comboBox2.SelectedItem = null;
                    dateTimePicker1.Value = DateTimePicker.MinimumDateTime;
                    con.Close();
                }
            }

            if (e.ColumnIndex == 3)
            {
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            string query = "update GroupProject set ProjectId = '" + this.comboBox1.Text + "' , GroupId = '" + this.comboBox2.Text + "', AssignmentDate = '" + (dateTimePicker1.Value) + "' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record is successfully edited.");

            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            dateTimePicker1.Value = DateTimePicker.MinimumDateTime;
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from GroupProject", sqlcon);
                DataTable t = new DataTable();
                sqlDa.Fill(t);
                dataGridView1.DataSource = t;

            }



        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value == DateTimePicker.MinimumDateTime)
            {
                dateTimePicker1.Value = DateTime.Now;
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
