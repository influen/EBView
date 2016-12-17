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
            TextRender();
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
                        FDocR(FilePath.Text);
                    }
                    else
                    {
                        if (result == true)
                        {
                            Fpath = ofd.FileName;
                            FDocR(Fpath);
                            FilePath.Text = ofd.FileName;
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show(FilePath.Text + " 파일을 찾을수없습니다.");
                    if (result == true)
                    {
                        Fpath = ofd.FileName;
                        FDocR(Fpath);
                        FilePath.Text = ofd.FileName;
                    }
                }
            }
            else if (result == true)
            {
                FilePath.Text = ofd.FileName;
                Fpath = ofd.FileName;
                FDocR(Fpath);
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
            
           // using (StreamReader sr = new StreamReader(Fpath))
           // {
               
                // TextSplit ts = new TextSplit();
                //TextSplit.textFiles = new List<string>();
             //   string sLine;
              //  while ((sLine = sr.ReadLine()) != null)
               // {
                    //  TextSplit.textFiles.Add(sLine);
              //  }
                // int iOnePageLines;

                //   Textview.Document.Blocks.Add(new Paragraph(new Run(TextSplit.textFiles[2])));
                //  Textview.Document.Blocks.Add(new Paragraph(new Run(TextSplit.textFiles[2])));
                TextViewSize();
                // this.Title = TextSplit.astitle;
            


        }
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


        public void FDocR(string path)
        {
     
            StreamReader srr =  TextCopy(Fpath);
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(srr.ReadToEnd());
          
            FlowDocument document = new FlowDocument(paragraph);
            TextView.Document = document;
            document.Background = Brushes.Beige;
            document.ColumnWidth = 800;
            //  document.MaxPageWidth = 800;
            paragraph.BorderBrush = Brushes.Blue;
            ThicknessConverter tc = new ThicknessConverter();
            paragraph.BorderThickness = (Thickness)tc.ConvertFromString("2");
            //   document.PageWidth = 500;
        }
        private void FileOpenbtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

    }
}

