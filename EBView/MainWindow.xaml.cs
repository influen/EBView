using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace EBView
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        string Fpath;

        public MainWindow()
        {
            InitializeComponent();
            TextViewSize();
        }
        public void OpenFile()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = ofd.ShowDialog();
            ofd.DefaultExt = "*.txt";
            ofd.Filter = "Text files| *.txt|All Files|*.*";
            ofd.Title = "select Text file";
            FilePath.Text = FilePath.Text.Trim();
            if (FilePath.Text.Any())
            {

                if (FilePathCheck(FilePath.Text) == true)
                {
                    if (ofd.FileName != FilePath.Text)
                    {
                        //FDocR(FilePath.Text);
                        TextRender();
                    }
                    else
                    {
                        if (result == true)
                        {
                            Fpath = ofd.FileName;
                            FilePath.Text = ofd.FileName;
                            TextRender();
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show(FilePath.Text + " 파일을 찾을수없습니다.");
                    if (result == true)
                    {
                        Fpath = ofd.FileName;
                        FilePath.Text = ofd.FileName;
                        TextRender();
                    }
                }
            }
            else if (result == true)
            {
                FilePath.Text = ofd.FileName;
                Fpath = ofd.FileName;
                TextRender();
            }
        }
        public void TextViewSize()
        {
            var TextViewSizefontsize = TextView.FontSize;
            var TextViewSizewidth = TextView.ActualWidth;
            var TextViewSizeheight = TextView.ActualHeight;
            var WLength = TextViewSizewidth / TextViewSizefontsize;
            var TextViewLines = TextViewSizeheight / TextViewSizefontsize;
        }
        public StreamReader TextCopy(String Fpath)
        {
            string path = Fpath;
            StreamReader mem = new StreamReader(path);
            string memString = mem.ReadToEnd();
            byte[] buffer = Encoding.UTF8.GetBytes(memString);
            MemoryStream ms = new MemoryStream(buffer);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            return sr;
        }
        public void TextRender()
        {

            List<string> textFiles = new List<string>();
            string sLine = string.Empty;
            StreamReader srr = TextCopy(Fpath);
            Paragraph paragraph = new Paragraph();
            int index = 0;
            int count = 500;
            while (srr.EndOfStream != true)
            {
                char[] buffer = new char[count - index];
                srr.ReadBlock(buffer, 0, buffer.Length);
                sLine = new String(buffer);
                textFiles.Add(sLine);
                index = count;
                count += 500;
            }
            short EndPageNumber = (short)textFiles.Count;
            PageNumber.Text = $"1/{EndPageNumber}";
            paragraph.Inlines.Add(textFiles[2]);
            
            FlowDocument document = new FlowDocument(paragraph);
            TextView.Document = document;

            document.Background = Brushes.Beige;
            paragraph.BorderBrush = Brushes.Blue;
            ThicknessConverter tc = new ThicknessConverter();
            paragraph.BorderThickness = (Thickness)tc.ConvertFromString("2");

            //   Textview.Document.Blocks.Add(new Paragraph(new Run(TextSplit.textFiles[2])));
            //  Textview.Document.Blocks.Add(new Paragraph(new Run(TextSplit.textFiles[2])));
            //  document.MaxPageWidth = 800;
            //   document.PageWidth = 500;
            // document.ColumnWidth = 800;
            // this.Title = TextSplit.astitle;

        }
        //public void FDocR(string path)
        //{
        //}

        public bool FilePathCheck(string Fpath)
        {
            string path = Fpath;
            bool check = false;

            FileInfo fi = new FileInfo(path);
            if (fi.Exists == true)
            {
                check = true;
            }
            return check;
        }

        private void FileOpenbtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

    }
}

