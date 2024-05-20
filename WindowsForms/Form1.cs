using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //List<DirectoryInfo> lstDicrectory = new List<DirectoryInfo>();
       
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please select a directory first.");
                return;
            }
            // Delete Directory
            // Directory.Delete(textDirectory.Text, true);

            // Delete Files From Directory 

            DateTime now = DateTime.Now;

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please select a day first.");
                return;
            }
            int days = Convert.ToInt32(textBox2.Text);

            int cnt = 0, c = 0;
            foreach (string path in checkedListBox1.Items)
            {
                
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    foreach (string file in files)
                    {
                        Console.WriteLine(file);

                        DateTime creationTime = File.GetLastWriteTime(file);

                        TimeSpan timeDifference = now - creationTime;

                        if (timeDifference.TotalDays >= days)
                        {
                            try
                            {
                               
                                if (File.Exists(file))
                                {
                                    
                                    ++cnt;
                                    File.Delete(file);

                                    Console.WriteLine($"File '{file}' deleted successfully.");
                                }
                                else
                                {
                                    MessageBox.Show("There is no file here.");
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error deleting file '{file}': {ex.Message}");
                            }

                        }

                    }
                }
                else
                {
                    MessageBox.Show("No path here.");
                }
            }
            
            if(cnt > 0)
            {
                MessageBox.Show($"{cnt} Files Are Deleted From Directory Successfully.");
            }
            else
            {
                MessageBox.Show("File is already Empty.");
            }

            textBox3.Clear();
            textBox2.Clear();
            checkedListBox1.Items.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please select a folder first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            if (startDate.Date > endDate.Date)
            {
                MessageBox.Show("The start date must be earlier than the end date.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int cnt = 0;
                
                foreach(string path in checkedListBox1.Items)
                {
                    if(Directory.Exists(path))
                    {
                        string[] files = Directory.GetFiles(path);
                        foreach(string file in files)
                        {
                            Console.WriteLine(file);

                            DateTime fileDate = File.GetLastWriteTime(file);
                            if (fileDate.Date >= startDate.Date && fileDate.Date <= endDate.Date)
                            {
                                cnt++;
                                File.Delete(file);
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("No path here.");
                    }
                }
                if (cnt > 0)
                {
                    MessageBox.Show($"{cnt} Files between the specified dates have been deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("File is already Empty.");
                }

               

                textBox3.Clear();
                textBox2.Clear();
                checkedListBox1.Items.Clear();

                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult result = folder.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox3.Text = folder.SelectedPath;
            }
            if (!checkedListBox1.Items.Contains(textBox3.Text))
            {
                checkedListBox1.Items.Add(textBox3.Text);
            }
            else
            {
                MessageBox.Show("Path is already added.");
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void undo_Click(object sender, EventArgs e)
        {
            List<object> lstobj = new List<object>(); 

            foreach (object obj in checkedListBox1.CheckedItems) 
            { 
                lstobj.Add(obj); 
            }
            foreach (object obj in lstobj)
            {
                checkedListBox1.Items.Remove(obj);
            }
        }
    }
}
