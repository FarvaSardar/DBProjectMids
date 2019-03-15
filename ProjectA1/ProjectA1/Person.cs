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
    public partial class Person : Form
    {

        SqlConnection con = new SqlConnection("Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True; MultipleActiveResultSets=true");
        SqlDataAdapter adapt;
        int ID = 0;

        public Person()
        {
            InitializeComponent();
        }

        string conStr = "Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True; MultipleActiveResultSets=true";

        string oldText = string.Empty;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.All(chr => char.IsLetter(chr)))
            {
                oldText = textBox2.Text;
                textBox2.Text = oldText;

                textBox2.BackColor = System.Drawing.Color.White;
                textBox2.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                textBox2.Text = oldText;
                textBox2.BackColor = System.Drawing.Color.Red;
                textBox2.ForeColor = System.Drawing.Color.White;
            }
            textBox2.SelectionStart = textBox2.Text.Length;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                string query1 = "insert into Person(FirstName,LastName,Contact,Email,DateOfBirth,Gender) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "' , '" + (dateTimePicker1.Value) + "',(select Id from Lookup where value ='"+comboBox1.Text+ "'))";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dbr1;
                try
                {
                    dbr1 = cmd1.ExecuteReader();
                    MessageBox.Show("saved");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    dateTimePicker1.Text = "";
                    comboBox1.SelectedValue = "";
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

        private void Number_Only(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
            if (textBox3.Text.Length > 11)
            {
                MessageBox.Show("Contact length cannot be greater than 11");
            }


        }

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Person", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Text = "";
            comboBox1.Text = "";
            ID = 0;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.MaxLength = 12;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(textBox4.Text, pattern))
            {
                errorProvider1.Clear();
            }
            else
            {
                errorProvider1.SetError(this.textBox4, "Please Enter a valid Email");
                return;
            }
        }

        private void Person_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projectADataSet6.Person' table. You can move, or remove it, as needed.
            //this.personTableAdapter1.Fill(this.projectADataSet6.Person);
            // TODO: This line of code loads data into the 'projectADataSet.Person' table. You can move, or remove it, as needed.
            // this.personTableAdapter.Fill(this.projectADataSet.Person);
            // dataGridView1.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            SqlConnection con = new SqlConnection(conStr);
            string query = "Select * from Person";
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
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

        }


        private void button3_Click(object sender, EventArgs e)
        {
            Main m = new Main();
            m.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Student m = new Student();
            m.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //SqlConnection con = new SqlConnection(conStr);
            //con.Open();
            //int Id2 = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            //if (e.ColumnIndex == 7)
            //{
            //    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            //    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            //    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            //    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();

            //    var datestring = dateTimePicker1.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //    //            dateTimePicker1.Value = DateTime.ParseExact(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(),
            //    //"dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

            //    dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].FormattedValue.ToString());
            //    comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
            //}

            //if (e.ColumnIndex == 8)
            //{
            //    if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {

            //        this.dataGridView1.Rows.RemoveAt(e.RowIndex);
            //        string query1 = "Delete from Student where Id = @Id2";
            //        SqlCommand cmd1 = new SqlCommand(query1, con);
            //        cmd1.Parameters.Add(new SqlParameter("@Id2", Id2));
            //        cmd1.ExecuteReader();



            //        this.dataGridView1.Rows.RemoveAt(e.RowIndex);
            //        string query3 = "Delete from Person where Id = @Id2";
            //        SqlCommand cmd3 = new SqlCommand(query3, con);
            //        cmd3.Parameters.Add(new SqlParameter("@Id2", Id2));
            //        cmd3.ExecuteReader();
                    
            //        con.Close();

            //    }
            //}
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && comboBox1.Text != "")
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("update Person set FirstName = @fn, LastName=@ln, Contact = @cnt, Email = @eml,  Gender= (select Id from Lookup where value ='" + comboBox1.Text + "') where ID=@id", con);
                                              

                cmd1.Parameters.AddWithValue("@id", ID);
                cmd1.Parameters.AddWithValue("@fn", textBox1.Text);
                cmd1.Parameters.AddWithValue("@ln", textBox2.Text);
                cmd1.Parameters.AddWithValue("@cnt", textBox3.Text);
                cmd1.Parameters.AddWithValue("@eml", textBox4.Text);
                cmd1.Parameters.AddWithValue("@gndr", comboBox1.Text);             
                cmd1.ExecuteNonQuery();


                //SqlCommand cmd = new SqlCommand("update Student where ID=@id", con);
                //cmd.Parameters.AddWithValue("@id", ID);
                ////cmd.Parameters.AddWithValue("@regno", textBox1.Text);
                //cmd.ExecuteNonQuery();

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
            if (ID != 0)
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("delete Student where ID=@id", con);
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand("delete Person where ID=@id", con);
                cmd1.Parameters.AddWithValue("@id", ID);
                cmd1.ExecuteNonQuery();


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

        private void Red(object sender, PaintEventArgs e)
        {

        }
    }
}