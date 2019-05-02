using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectA1
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Person p = new Person();
            p.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Student s = new Student();
            s.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Project p = new Project();
            p.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Advisor a = new Advisor();
            a.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Evaluation es = new Evaluation();
            es.Show();
            this.Hide();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ProjectAdvisor p = new ProjectAdvisor();
            p.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Group g = new Group();
            g.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GroupStudent gs = new GroupStudent();
            gs.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GroupProject gp = new GroupProject();
            gp.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GroupEvaluation ge = new GroupEvaluation();
            ge.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ReportProject r = new ReportProject();
            r.Show();
            this.Hide();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            ReportEvaluation re = new ReportEvaluation();
            re.Show();
            this.Hide();
        }
    }
}
