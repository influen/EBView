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
using System.Globalization;

namespace EBView
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        //  public RichTextBox TextView;
      //  public static MainWindow MainW;
        public  Point viewsize;
        
        public Point ViewSize
        {
            get
            {
                Point viewsize = new Point();

                viewsize.X = TextView.ActualWidth;
                viewsize.Y = TextView.ActualHeight;

                return viewsize;
            }
            set { viewsize = value; }
            
        }
       // List<string> textFs = new List<string>();
        
            
        public MainWindow()
        {
            InitializeComponent();
            //MainW = this;




        }

        TextRenders tr = new TextRenders();
        FileOpen fo = new FileOpen();

     
        private void FileOpenbtn_Click(object sender, RoutedEventArgs e)
        {
            FileOpen fo = new FileOpen();
            fo.OpenFile();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

           // DevMonitor dv = new DevMonitor();
           DevMonitor.Dev();
        }

        private void NPage_Click(object sender, RoutedEventArgs e)
        {
            
            
            if (!string.IsNullOrEmpty(FileOpen.Filepath))
            {
                tr.Paging(TextRenders.PNumber + 1);
            }
        }

        private void PPage_Click(object sender, RoutedEventArgs e)
        {
            
            if (!string.IsNullOrEmpty(FileOpen.Filepath))
                {
                tr.Paging(TextRenders.PNumber - 1);
                }

        }

        private void PageNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                if (!string.IsNullOrEmpty(FileOpen.Filepath))
                {
                    int page = int.Parse(PageNumber.Text);
                    tr.Paging(page);
                }

            }
            else if (!(((Key.D0 <= e.Key) && (e.Key <= Key.D9))
                 || ((Key.NumPad0 <= e.Key) && (e.Key <= Key.NumPad9))
                 || e.Key == Key.Back))
            {
                e.Handled = true;
            }
        }
    }


}

