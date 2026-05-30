using System;
using System.IO;
using System.Windows.Forms;

namespace FileCopyApp
{
    public partial class Form1 : Form
    {
        private TextBox txtFrom;
        private TextBox txtTo;
        private Button btnFrom;
        private Button btnTo;
        private Button btnCopy;

        public Form1()
        {
            this.Width = 350;
            this.Height = 150;
            this.Text = "Copy File";

            txtFrom = new TextBox();
            txtFrom.Location = new System.Drawing.Point(10, 10);
            txtFrom.Width = 200;

            btnFrom = new Button();
            btnFrom.Location = new System.Drawing.Point(220, 8);
            btnFrom.Text = "From";
            btnFrom.Click += BtnFrom_Click;

            txtTo = new TextBox();
            txtTo.Location = new System.Drawing.Point(10, 40);
            txtTo.Width = 200;

            btnTo = new Button();
            btnTo.Location = new System.Drawing.Point(220, 38);
            btnTo.Text = "To";
            btnTo.Click += BtnTo_Click;

            btnCopy = new Button();
            btnCopy.Location = new System.Drawing.Point(10, 70);
            btnCopy.Text = "Copy";
            btnCopy.Click += BtnCopy_Click;

            this.Controls.Add(txtFrom);
            this.Controls.Add(btnFrom);
            this.Controls.Add(txtTo);
            this.Controls.Add(btnTo);
            this.Controls.Add(btnCopy);

            this.AcceptButton = btnCopy;
        }

        private void BtnFrom_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFrom.Text = ofd.FileName;
            }
        }

        private void BtnTo_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtTo.Text = fbd.SelectedPath;
            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = Path.GetFileName(txtFrom.Text);
                string destPath = Path.Combine(txtTo.Text, fileName);
                File.Copy(txtFrom.Text, destPath, true);
                MessageBox.Show("Успішно скопійовано");
            }
            catch
            {
                MessageBox.Show("Помилка при копіюванні");
            }
        }
    }
}
