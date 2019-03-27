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
    public partial class GroupEvaluation : Form
    {
        SqlConnection con = new SqlConnection("Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True;");

        public GroupEvaluation()
        {
            InitializeComponent();
        }
        string conStr = "Data Source=FARVASARDAR-PC\\FARVASQL;Initial Catalog=ProjectA;Integrated Security=True";

        private void GroupEvaluation_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'groupEvaluationDataSet.GroupEvaluation' table. You can move, or remove it, as needed.
            this.groupEvaluationTableAdapter.Fill(this.groupEvaluationDataSet.GroupEvaluation);

            SqlConnection con = new SqlConnection(conStr);
            string query = "select Id from [Group]";
            string query1 = "select Id from Evaluation";
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

                string query1 = "insert into GroupEvaluation( GroupId,EvaluationId, ObtainedMarks, EvaluationDate) values ( '" + comboBox1.Text + "' , '" + comboBox2.Text + "','" + textBox1.Text.ToString() + "','" + Convert.ToDateTime(dateTimePicker1.Value) + "') ";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader dbr1;
                try
                {
                    dbr1 = cmd1.ExecuteReader();
                    MessageBox.Show("saved");
                    comboBox1.SelectedText = "";
                    comboBox2.SelectedValue = "";
                    dateTimePicker1.Text = "";
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
            string query = "Select * from GroupEvaluation";
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


                    string query1 = "Delete from GroupEvaluation where GroupId = @Id1 AND EvaluationId= @Id2 ";
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
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            string query = "update GroupEvaluation set GroupId = '" + this.comboBox1.Text + "' , EvaluationId = '" + this.comboBox2.Text + "', ObtainedMarks = '" + this.textBox1.Text + "', EvaluationDate = '" + (dateTimePicker1.Value) + "' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record is successfully edited.");
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from GroupEvaluation", sqlcon);
                DataTable t = new DataTable();
                sqlDa.Fill(t);
                dataGridView1.DataSource = t;
            }
        }
    }
}
