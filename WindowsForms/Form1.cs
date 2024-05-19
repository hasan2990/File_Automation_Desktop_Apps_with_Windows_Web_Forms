using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult result = folder.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folder.SelectedPath;
            }

        }
        private void deleteFile(string filePath)
        {
            try
            {
                int cnt = 0;
                if (File.Exists(filePath))
                {
                    ++cnt;
                    File.Delete(filePath);
                    Console.WriteLine($"{cnt} Files '{filePath}' deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file '{filePath}': {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please select a directory first.");
                return;
            }
            // Delete Directory
            // Directory.Delete(textDirectory.Text, true);

            // Delete Files From Directory 

            DateTime now = DateTime.Now;

            int days = Convert.ToInt32(textBox2.Text);


            foreach (string file in Directory.GetFiles(textBox1.Text))
            {
                Console.WriteLine(file);

                DateTime creationTime = File.GetLastWriteTime(file);

                TimeSpan timeDifference = now - creationTime;

                if (timeDifference.TotalDays >= days)
                {
                    deleteFile(file);
                }

            }

            MessageBox.Show($"Files Are Deleted From Directory Successfully.");

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please select a folder first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            if (startDate > endDate)
            {
                MessageBox.Show("The start date must be earlier than the end date.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var files = Directory.GetFiles(textBox1.Text);
                int cnt = 0;
                foreach (var file in files)
                {
                    DateTime fileDate = File.GetLastWriteTime(file);
                    if (fileDate >= startDate && fileDate <= endDate)
                    {
                        cnt++;
                        File.Delete(file);
                    }
                }

                MessageBox.Show($"{cnt} Files between the specified dates have been deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox1.Clear();
                textBox2.Clear();
                textBox1.Text = string.Empty;
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
    }
}
