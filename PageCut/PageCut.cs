using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;

namespace PageCut
{
    public class PageCuts
    {
        private static List<string> cutbook = new List<string>();
        private static int endpagenumber;
        private static int pnumber;
        private static int aaa;

      
        public static int EndPageNumber { get { return endpagenumber; } set { } }
        public static int PNumber { get { return pnumber; } set { } }
        

        public static List<string> PageCuting(string text,double width,double height,string fontFamily,double emsize )
        {
            double viewsizewidth = width;
            double viewsizeheight = height;
            return cutbook;
        }
       
  
    


        


    }
}
