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
using System.Text.RegularExpressions;

namespace ProjectA1
{
    public partial class Evaluation : Form
    {
        SqlConnection con = new SqlConnection("Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True; MultipleActiveResultSets=true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;
        public Evaluation()
        {
            InitializeComponent();
        }
        string conStr = "Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True; MultipleActiveResultSets=true";
        private void Evaluation_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projectADataSet4.Evaluation' table. You can move, or remove it, as needed.
            this.evaluationTableAdapter.Fill(this.projectADataSet4.Evaluation);

        }

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Evaluation", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        
        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            ID = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                string query1 = "insert into Evaluation(Name, TotalMarks, TotalWeightage) values ('" + textBox1.Text.ToString() + "','" +Convert.ToInt32( textBox2.Text) + "','" + Convert.ToInt32(textBox3.Text) + "')";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dbr1;
                try
                {
                    dbr1 = cmd1.ExecuteReader();
                    MessageBox.Show("saved");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
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
            string query = "Select * from Evaluation";
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
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //SqlConnection con = new SqlConnection(conStr);
            //con.Open();
            //int Id1 = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            //if (e.ColumnIndex == 5)
            //{
            //    if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {

            //        this.dataGridView1.Rows.RemoveAt(e.RowIndex);
            //        string query1 = "Delete from Evaluation where Id = @Id1";
            //        SqlCommand cmd1 = new SqlCommand(query1, con);
            //        cmd1.Parameters.Add(new SqlParameter("@Id1", Id1));
            //        cmd1.ExecuteNonQuery();
            //        con.Close();
            //    }
            //}

            //if (e.ColumnIndex == 4)
            //{
            //    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            //    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            //    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
       

            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                cmd = new SqlCommand("update Evaluation set Name=@name,TotalMarks= @totalmarks, TotalWeightage=@totalweightage where ID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@totalmarks", textBox2.Text);
                cmd.Parameters.AddWithValue("@totalweightage", textBox3.Text);
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
            //EditEvaluation ee = new EditEvaluation();
            //ee.Show();
            //this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                cmd = new SqlCommand("delete Evaluation where ID=@id", con);
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


        private void Number_Only(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar ))
            {
                e.Handled = true;
            }
            
            //if (textBox3.Text.Length >3 || textBox2.Text.Length >3)
            //{
            //    MessageBox.Show("Contact length cannot be greater than 3");
            //}


        }
        string oldText = string.Empty;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.All(chr => char.IsLetter(chr)))
            {
                oldText = textBox1.Text;
                textBox1.Text = oldText;

                textBox1.BackColor = System.Drawing.Color.White;
                textBox1.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                textBox1.Text = oldText;
                textBox1.BackColor = System.Drawing.Color.Red;
                textBox1.ForeColor = System.Drawing.Color.White;
            }
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Main m = new Main();
            m.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //textBox2.MaxLength = 3;
        }


        private void Validate_Text(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                int i;
                if (int.TryParse(tb.Text, out i))
                {
                    if (i >= 0 && i <= 99)
                        return;
                }
            }
            MessageBox.Show("invalid input");
            e.Cancel = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //textBox3.MaxLength = 3;
        }
    }
}
