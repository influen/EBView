using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace EBView
{
    public static class DevMonitor
    {

        public static void Dev()
        {
            MainWindow mw = (MainWindow)System.Windows.Application.Current.MainWindow;
            Window DevWin = new Window();
            DevWin.Owner = (MainWindow)System.Windows.Application.Current.MainWindow;
            DevWin.Width = 200;
            DevWin.Height = 200;
            DevWin.Background = Brushes.Gray;
            DevWin.Title = "Devwin";
            DevWin.ShowActivated = false;
            DevWin.ShowInTaskbar = false;
            DevWin.WindowStyle = WindowStyle.None;
            DevWin.Top = mw.Top;
            DevWin.Left = mw.Left + mw.ActualHeight;
            DevWin.Show();

        }
        public static Stopwatch sw = new Stopwatch();
        public static string LoadTime()
        {
            TimeSpan ts = sw.Elapsed;
            string elstime = FileOpen.Filepath + string.Format(":{0:00}:{1:00}.{2:00}초", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            Paragraph pr = new Paragraph();
            pr.Inlines.Add(elstime);
            FlowDocument document = new FlowDocument(pr);
            document.Background = Brushes.Beige;
            pr.BorderBrush = Brushes.Blue;
            ThicknessConverter tc = new ThicknessConverter();
            pr.BorderThickness = (Thickness)tc.ConvertFromString("2");


            return elstime;
        }

    }

}

