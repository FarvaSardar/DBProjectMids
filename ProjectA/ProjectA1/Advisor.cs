﻿using System;
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
        


        public Advisor()
        {
            InitializeComponent();
        }
        string conStr = "Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True";
        private void Advisor_Load(object sender, EventArgs e)
        {
            this.advisorTableAdapter1.Fill(this.projectADataSet8.Advisor);


            SqlConnection con = new SqlConnection(conStr);
            string query = "select Id from Lookup  where Category = 'DESIGNATION' ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dbr;

            try
            {
                con.Open();
                dbr = cmd.ExecuteReader();
                while (dbr.Read())
                {
                
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
            

            bool isExists = false;
            con.Open();
            string query = "Select * from Advisor";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dbr = cmd.ExecuteReader();
            while (dbr.Read())
            {
                string id = textBox1.Text;
                if (id == Convert.ToString(dbr[0]))
                {
                    isExists = true;
                    MessageBox.Show("Advisor ID already exixts. Cannot add data again corresponding to that ID.");
                    textBox1.Text="";

                    break;
                }
            }
            con.Close();


            con.Open();
            if (!isExists)
            {
                string query1 = "insert into Advisor(Id, Designation, Salary) values ( '" +Convert.ToInt32(textBox1.Text) + "' ,(select Id from Lookup where value ='" + comboBox2.Text + "') , '" + Convert.ToDecimal(textBox2.Text) + "')";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dbr1;
                try
                {
                    dbr1 = cmd1.ExecuteReader();
                    MessageBox.Show("Advisor added successfully.");

                    textBox1.Text = "";
                    comboBox2.SelectedItem = null;
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


            con.Open();
            dataGridView1.Show();
            string query3 = "Select * from Advisor";
            SqlCommand cmd3 = new SqlCommand(query3, con);

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd3;
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
            con.Close();

        }



        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            SqlConnection con = new SqlConnection(conStr);
            string query3 = "Select * from Advisor";
            SqlCommand cmd3 = new SqlCommand(query3, con);

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd3;
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
            con.Close();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            int Id1 = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            if (e.ColumnIndex == 3)
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string query = "Delete from ProjectAdvisor where AdvisorId = @Id1";
                    string query1 = "Delete from Advisor where Id = @Id1";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    this.dataGridView1.Rows.RemoveAt(e.RowIndex);
                    cmd.Parameters.Add(new SqlParameter("@Id1", Id1));
                    cmd.ExecuteNonQuery();
                    cmd1.Parameters.Add(new SqlParameter("@Id1", Id1));
                    cmd1.ExecuteNonQuery();

                    textBox1.Text = "";
                    comboBox2.SelectedItem = null;
                    textBox2.Text = "";
                    con.Close();
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            var a = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            SqlCommand cmd1 = new SqlCommand ("select Value FROM Lookup where Category = 'DESIGNATION'' ", con);
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

        }        

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox2.Text != "" )
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("update Advisor set Salary=@salry where id = @Id ", con);

                cmd1.Parameters.AddWithValue("@id", Convert.ToInt32(textBox1.Text));
                cmd1.Parameters.AddWithValue("@salry", textBox2.Text);
                cmd1.ExecuteNonQuery();

                MessageBox.Show("Record Edited Successfully");

                //textBox1.Text = "";
                textBox2.Text = "";

                con.Close();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
