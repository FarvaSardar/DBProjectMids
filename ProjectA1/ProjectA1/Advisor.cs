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
    public partial class Advisor : Form
    {

        SqlConnection con = new SqlConnection("Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;


        public Advisor()
        {
            InitializeComponent();
        }
        string conStr = "Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True";
        private void Advisor_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projectADataSet8.Advisor' table. You can move, or remove it, as needed.
            this.advisorTableAdapter1.Fill(this.projectADataSet8.Advisor);
            // TODO: This line of code loads data into the 'projectADataSet3.Advisor' table. You can move, or remove it, as needed.
            //this.advisorTableAdapter.Fill(this.projectADataSet3.Advisor);


            comboBox1.Items.Remove(comboBox1.SelectedItem);
            //dataGridView1.Hide();
            SqlConnection con = new SqlConnection(conStr);
            string query = "select Id from Lookup  where Category = 'DESIGNATION' or Category = 'ADVISOR_ROLE' ";
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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            string s = comboBox1.Text;
            if (con.State == ConnectionState.Open)
            {
                string query1 = "insert into Advisor(Id, Designation, Salary) values ( '" + comboBox1.Text + "' ,(select Id from Lookup where value ='" + comboBox2.Text + "') , '" + Convert.ToDecimal(textBox2.Text) + "')";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dbr1;
                try
                {
                    dbr1 = cmd1.ExecuteReader();
                    MessageBox.Show("saved");
                    comboBox1.SelectedValue = "";
                    comboBox2.SelectedValue = "";
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
            string query = "Select * from Advisor";
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
            
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox2.Text = "";
            ID = 0;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //SqlConnection con = new SqlConnection(conStr);
            //con.Open();
            //int Id1 = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            //if (e.ColumnIndex == 3)
            //{
            //    if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {

            //        this.dataGridView1.Rows.RemoveAt(e.RowIndex);
            //        string query1 = "Delete from Advisor where Id = @Id1";
            //        SqlCommand cmd1 = new SqlCommand(query1, con);
            //        cmd1.Parameters.Add(new SqlParameter("@Id1", Id1));
            //        cmd1.ExecuteNonQuery();
            //        con.Close();
            //    }
            //}

            //if (e.ColumnIndex == 2)
            //{
            //    //comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            //    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            //    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();

            //}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            //int number = int.Parse(comboBox2.Items[comboBox2.SelectedIndex].ToString());
            

            //int id;
            //if (comboBox2.Items[comboBox2.SelectedIndex].ToString() == "Professor")
            //{
            //    id = 6;
            //}
            //if (comboBox2.Items[comboBox2.SelectedIndex].ToString() == "Associate Professor")
            //{
            //    id = 7;
            //}
            //if (comboBox2.Items[comboBox2.SelectedIndex].ToString() == "Assisstant Professor")
            //{
            //    id = 8;
            //}
            //if (comboBox2.Items[comboBox2.SelectedIndex].ToString() == "Lecturer")
            //{
            //    id = 9;
            //}
            //if (comboBox2.Items[comboBox2.SelectedIndex].ToString() == "Industry Professional")
            //{
            //    id = 10;
            //}

            //int value;
            //if (!Int32.TryParse(this.comboBox2.Text, out value))
            //{
            //    comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            //}

            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && comboBox1.Text != "" && comboBox2.Text != "")
            {

                SqlDataAdapter adapter = new SqlDataAdapter();

                cmd = new SqlCommand("update Advisor set Designation=@desig, Salary=@salry where ID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@salry", textBox2.Text);
                cmd.Parameters.AddWithValue("@desig", comboBox2.Text);

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
            if (ID != 0)
            {
                cmd = new SqlCommand("delete Advisor where ID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.MaxLength = 6;
        }

        private void Number_Only(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Main m = new Main();
            m.Show();
            this.Hide();
        }
    }
}
