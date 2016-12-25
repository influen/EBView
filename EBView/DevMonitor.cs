using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EBView
{
    public  static class DevMonitor
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


    }
}
