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
    public partial class Group : Form
    {
        SqlConnection con = new SqlConnection("Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True;");
        public Group()
        {
            InitializeComponent();
        }

        string conStr = "Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True";
        private void Group_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'groupDataSet.Group' table. You can move, or remove it, as needed.
            //this.groupTableAdapter.Fill(this.groupDataSet.Group);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                string query1 = "insert into [Group] ( Created_On) values( '" + (dateTimePicker1.Value) + "') ";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dbr1;
                try
                {
                    dbr1 = cmd1.ExecuteReader();
                    MessageBox.Show("saved");
                    dateTimePicker1.Text = " ";
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
            string query = "Select * from [Group]";
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

            if (e.ColumnIndex == 3)
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string query1 = "Delete from [Group] where Id = @Id1 ";
                    string query2 = "Delete from [GroupStudent] where GroupId = @Id1 ";
                    string query3 = "Delete from [GroupProject] where GroupId = @Id1 ";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    SqlCommand cmd3 = new SqlCommand(query3, con);
                    this.dataGridView1.Rows.RemoveAt(e.RowIndex);
                    cmd2.Parameters.Add(new SqlParameter("@Id1", Id1));
                    cmd2.ExecuteNonQuery();
                    cmd3.Parameters.Add(new SqlParameter("@Id1", Id1));
                    cmd3.ExecuteNonQuery();
                    cmd1.Parameters.Add(new SqlParameter("@Id1", Id1));
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
            }


            if (e.ColumnIndex == 2)
            {
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            string query = "update [Group] set Created_On= '" + this.dateTimePicker1.Value + "'  ";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record is successfully edited.");
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from [Group]", sqlcon);
                DataTable t = new DataTable();
                sqlDa.Fill(t);
                dataGridView1.DataSource = t;

            }
        }
    }
}