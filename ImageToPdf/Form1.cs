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
            if (folderDiag.SelectedPath != null && folderDiag.SelectedPath != "")
            {
                textBox1.Text = "Adding pages";
                Image[] pages = System.IO.Directory.GetFiles(folderDiag.SelectedPath)
                        .Select(file => System.Drawing.Image.FromFile(file))
                        .ToArray();
                string title = titleBox.Text == "Title" ? "untitled" : titleBox.Text;
                PdfDocument book = new PdfDocument(title + ".pdf");
                PdfPage page;
                PdfContents contents;
                PdfImage pic;

                for (int i = 0; i < pages.Length; i++)
                {
                    Image source = pages[i];
                    double height = source.Height;
                    double width = source.Width;

                    page = new PdfPage(book, width, height);
                    contents = new PdfContents(page);
                    contents.SaveGraphicsState();
                    pic = new PdfImage(book, source);
                    contents.DrawImage(pic, 0, 0, width, height);
                    contents.RestoreGraphicsState();
                    contents.CommitToPdfFile(true);
                }
                book.CreateFile();
                textBox1.Text = "Book created";
                pages = null;
                book = null;
                page = null;
                contents = null;
                pic = null;
            }
            else
            {
                textBox1.Text = "No directory selected";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
