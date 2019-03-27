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
    public partial class GroupStudent : Form
    {
        SqlConnection con = new SqlConnection("Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True;");

        public GroupStudent()
        {
            InitializeComponent();
        }
        string conStr = "Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True";

        private void GroupStudent_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'groupStudentDataSet.GroupStudent' table. You can move, or remove it, as needed.
            this.groupStudentTableAdapter.Fill(this.groupStudentDataSet.GroupStudent);

            SqlConnection con = new SqlConnection(conStr);
            string query = "select Id from [Group]";
            string query1 = "select Id from Student";
            // string query2 = "select Value from Lookup where Category= 'ADVISOR_ROLE' ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            //SqlCommand cmd2 = new SqlCommand(query2, con);
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
                while (dbr1.Read())
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
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                string query1 = "insert into GroupStudent(GroupId, StudentId, Status, AssignmentDate) values ( '" + comboBox1.Text + "' , '" + comboBox2.Text + "',(select Id from Lookup where Value= '" + comboBox3.Text + "' ),  '" + (dateTimePicker1.Value) + "') ";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dbr1;
                try
                {
                    dbr1 = cmd1.ExecuteReader();
                    MessageBox.Show("saved");
                    comboBox2.SelectedValue = "";
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

        private void label4_Click(object sender, EventArgs e)
        {

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
            string query = "Select * from GroupStudent";
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

            if (e.ColumnIndex == 5)
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {


                    string query1 = "Delete from GroupStudent where GroupId = @Id1 AND StudentId= @Id2 ";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    this.dataGridView1.Rows.RemoveAt(e.RowIndex);
                    cmd1.Parameters.Add(new SqlParameter("@Id2", Id2));
                    cmd1.Parameters.Add(new SqlParameter("@Id1", Id1));
                    cmd1.ExecuteNonQuery();
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

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            string query = "update GroupStudent set GroupId = '" + this.comboBox1.Text + "' , StudentId = '" + this.comboBox2.Text + "', Status = (select Id from Lookup where value = '" + comboBox3.Text + "'),  AssignmentDate = '"+(dateTimePicker1.Value)+"' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record is successfully edited.");
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from GroupStudent", sqlcon);
                DataTable t = new DataTable();
                sqlDa.Fill(t);
                dataGridView1.DataSource = t;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bool isExists = false;
            //SqlConnection con = new SqlConnection(conStr);
            //con.Open();
            //string query = "Select * from GroupStudent";
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bool isExists = false;
            //SqlConnection con = new SqlConnection(conStr);
            //con.Open();
            //string query = "Select * from GroupStudent";
            //SqlCommand cmd = new SqlCommand(query, con);
            //SqlDataReader dbr = cmd.ExecuteReader();
            //while (dbr.Read())
            //{
            //    string id = comboBox2.Text;
            //    if (id == Convert.ToString(dbr[0]))
            //    {
            //        isExists = true;
            //        MessageBox.Show("ID already exixts. Cannot add data again corresponding to that ID.");
            //        //comboBox1.Items[comboBox1.SelectedIndex] = string.Empty;
            //        //comboBox1.SelectedIndex = -1;
            //        break;
            //    }
            //}
            //con.Close();
        }
    }
}
