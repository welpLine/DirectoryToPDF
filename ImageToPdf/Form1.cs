using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfFileWriter;

namespace ImageToPdf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderDiag.ShowDialog();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(folderDiag.SelectedPath != null)
            {
                Image[] pages = System.IO.Directory.GetFiles(folderDiag.SelectedPath)
                        .Select(file => System.Drawing.Image.FromFile(file))
                        .ToArray();
                PdfDocument book = new PdfDocument("test.pdf");
                foreach (Image i in pages)
                {
                    //PdfPage page = new PdfPage(book);
                    //PdfContents contents = new PdfContents(page);
                    PdfImage Image = new PdfImage(book, i);
                }
                book.CreateFile();
                textBox1.Text = "Book created";
            } else
            {
                textBox1.Text = "No directory selected";
            }
        }
    }
}
