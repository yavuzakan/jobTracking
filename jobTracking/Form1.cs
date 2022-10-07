using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jobTracking
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox5.Text = "0";
            textBox6.Text = "";
            textBox5.Visible=false;
            textBox6.Visible=false;
           

            database.Create_db();
            if (textBox5.Text=="0")
            {
                button2.Text="Save";
                textBox4.Enabled = false;
                button3.Enabled = false;
                button5.Enabled = false;
            }
            else
            {
                button2.Text="Update";
                textBox4.Enabled = true;
                button3.Enabled = true;
                button5.Enabled = true;
            }

            ara();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text=="0")
            {
                button2.Text="Save";
                textBox4.Enabled = false;
                button3.Enabled = false;
                button5.Enabled = false;
            }
            else 
            {
                button2.Text="Update";
                textBox4.Enabled = true;
                button3.Enabled = true;
                button5.Enabled = true;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string control = button2.Text;


            string data1 = textBox2.Text;
            string data2 = textBox3.Text;
            string data3 = folder.RandomString(12);
            string id = textBox5.Text;

            if (data1 != ""  ||  data2 !="")
            {
                if(control=="Save")
                {
                    database.add(data1, data2, data3);
                    textBox2.Text="";
                    textBox3.Text="";
                    textBox4.Text="";
                    ara();
                }

                if (control=="Update")
                {
                    database.update(data1, data2, data3 , id);
                    textBox2.Text="";
                    textBox3.Text="";
                    textBox4.Text="";
                    ara();
                }


            }
            else 
            {
                MessageBox.Show("Error");
            }
        }
        public void ara()
        {
            string ara = textBox1.Text;
            dataGridView1.Rows.Clear();
            string path = "deneme.db";
            string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";
            var con = new SQLiteConnection(cs);
            SQLiteDataReader dr;
            con.Open();

            //string stm = "select * FROM data ORDER BY id ASC  ";
            //SELECT * FROM (SELECT * FROM graphs WHERE sid=2 ORDER BY id DESC LIMIT 10) g ORDER BY g.id
            string stm = "select * FROM data where data1 LIKE '%"+ ara +"%' or data2 LIKE '%"+ ara +"%' ORDER BY id ";
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dataGridView1.Rows.Insert(0, dr.GetValue(0).ToString(), dr.GetValue(1).ToString(), dr.GetValue(2).ToString(), dr.GetValue(3).ToString(), dr.GetValue(4).ToString(), dr.GetValue(5).ToString());

            }

            con.Close();

            this.dataGridView1.AllowUserToAddRows = false;


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;

            // dataGridView1.Columns[0].Visible = false;






        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];


                textBox5.Text = dataGridViewRow.Cells["Column1"].Value.ToString();
                textBox2.Text = dataGridViewRow.Cells["Column2"].Value.ToString();
                textBox3.Text = dataGridViewRow.Cells["Column3"].Value.ToString();
                textBox6.Text = dataGridViewRow.Cells["Column4"].Value.ToString();


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ara();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            textBox5.Text = "0";
            textBox6.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            button2.Text="Save";

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            textBox4.Text = dlg.FileName;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //System.IO.Directory.CreateDirectory(@"/asd");
            try {
                
                string target = textBox4.Text;
                string fold = textBox6.Text;

                string filename = Path.GetFileName(target);
                string rnd = folder.RandomString(3);
                if (target != "")
                {
                    //  MessageBox.Show(filename);
                    String result = fold +"//"+rnd+"-"+filename;
                    File.Copy(@target, @result);
                    textBox4.Text = "";
                    MessageBox.Show("Successful");
                }
                else 
                {
                    MessageBox.Show("Error . Select File");
                }

            }
            catch { 
            
            
            }




        }

        private void button5_Click(object sender, EventArgs e)
        {
            string fold = textBox6.Text;
            Process.Start("explorer.exe", @fold);
        }
    }
}
