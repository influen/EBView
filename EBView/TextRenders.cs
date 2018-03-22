using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace EBView
{
    public class TextRenders
    {
     
        private static List<string> textfiles = new List<string>();
        private static int endpagenumber;
        private static int pnumber;

        public static List<string> TextFiles { get { return textfiles; } set { } }
        public static int EndPageNumber { get { return endpagenumber; } set { } }
        public static int PNumber { get { return pnumber; } set { } }

        public TextRenders() { }

        public void TextRender(Point ViewSize) //텍스트 스플리트
        {
            DevMonitor.sw.Start();
            MainWindow mw = (MainWindow)System.Windows.Application.Current.MainWindow;
            Point viewsize = ViewSize;

            double viewsizewidth = viewsize.X;
            double viewsizeheight = viewsize.Y;

            textfiles.Clear();
            FileOpen fo = (FileOpen)new FileOpen();
            // StreamReader srr = fo.TextCopy();
            string fdd = fo.TextCopy();
            StringBuilder OneLine = new StringBuilder(1024);
            StringBuilder PageLine = new StringBuilder(1024);

            double emSize = 14.0 ;
            double pageheight = 0;

            int i = 0;
            while (i < (fdd.Length-1))
            {
                bool lineend = true;
                int j = 0;
                while (lineend)//한줄커팅
                {


                    OneLine.Append(fdd.ElementAt(i));
                    // OneLine.Append(srr.ReadBlock(, 1, 1));
                    //char[] sss=srr.read
                    FormattedText formattedText = new FormattedText(OneLine.ToString(), CultureInfo.GetCultureInfo("ko-kr"),
                                    FlowDirection.LeftToRight, new Typeface("굴림"), emSize, Brushes.Black,1);
                    //if (ViewSize[0] < formattedText.Width)
                    formattedText.Trimming = TextTrimming.CharacterEllipsis;
                    
                    int switchExpression = 0;
                    //문자길이가 가로사이즈보다 크거나 줄바꿈이면 라인 커팅
                    if ((formattedText.Width >= viewsizewidth))
                    {
                        switchExpression = 1;
                    }
                    else if ((fdd.ElementAt(i) == '\r'))
                    {
                        switchExpression = 3;
                    }
                    else if (pageheight >= viewsizeheight)//세로사이즈보다 크다면 페이지커팅
                    {
                        switchExpression = 2;
                    }

                    switch (switchExpression)
                    {
                        case 1:
                            OneLine.Remove(OneLine.Length-1, 1);
                            PageLine.Append(OneLine);
                            OneLine.Clear();
                            pageheight = pageheight + formattedText.Height;
                            //pageheight = pageheight + (formattedText.Baseline + formattedText.Height);
                            
                            j = j + 1;
                            break;

                        case 2:
                            textfiles.Add(PageLine.ToString());
                            PageLine.Clear();
                            PageLine.Append(OneLine);
                            OneLine.Clear();
                            
                            lineend = false;
                            i = i + 1;
                            pageheight = 0;
                            pageheight = pageheight + formattedText.Height;
                           
                            break;

                        case 3:
                            i = i + 1;
                            OneLine.Append(fdd.ElementAt(i));
                            PageLine.Append(OneLine);
                            OneLine.Clear();
                            pageheight = pageheight + formattedText.Height;
                            i = i + 1;
                            j = j + 1;
                            break;

                        default:
                            //PageLine.Append(OneLine);
                            i = i + 1;
                            break;
                    }
                    if (i > (fdd.Length - 1))
                    {
                        PageLine.Append(OneLine);
                        textfiles.Add(PageLine.ToString());
                        lineend = false;
                       
                    }
                }
            }
            endpagenumber = textfiles.Count;
            //PageNumber.Text = $"1/{EndPageNumber}";
            mw.PageNumber.Text = pnumber.ToString();
            DevMonitor.sw.Stop();
            DevMonitor.LoadTime();
            // return textF;
        }


        #region 느림
        //while (textF.Count > i + 1)
        //{
        //    string HLine = string.Empty;
        //    double vv = 0;

        //    while (true)
        //    {
        //        int index = 0;
        //        if (textF.Count < i + 1)
        //        {
        //            textFiles.Add(HLine);
        //            break;
        //        }
        //        else if (ViewSize[1] > vv)
        //        {
        //            sLine = string.Empty;
        //            string dfd = textF[i];

        //            var buffer = dfd.ToCharArray(index, textF[i].Length);

        //            while (true)
        //            {
        //                sLine = sLine + buffer[count];
        //                FormattedText formattedText = new FormattedText(sLine, CultureInfo.GetCultureInfo("ko-kr"),
        //                    FlowDirection.LeftToRight, new Typeface("Arial"), FontSize = 14, Brushes.Black);

        //                if (ViewSize[0] < formattedText.Width)
        //                {
        //                    HLine += sLine;
        //                    sLine = string.Empty;
        //                    vv = vv + (formattedText.Baseline + formattedText.Height);

        //                }
        //                else if (buffer.Length == count + 1)
        //                {
        //                    HLine += sLine;
        //                    vv = vv + (formattedText.Baseline + formattedText.Height);
        //                    count = 0;

        //                    break;
        //                }
        //                index = count;
        //                count++;
        //            }
        //            i++;
        //        }


        //        else if (ViewSize[1] < vv)
        //        {
        //            textFiles.Add(HLine);
        //            break;

        //        } 


        //    }
        //    //  textFiles.Add(HLine);
        //} 
        #endregion

        //while (srr.EndOfStream != true) {
        //    while (TextViewLines < textviewlines)
        //    {

        //        int wlength = 0;
        //        for (count=0; Fontwidth =< WLength; count++)
        //        {

        //            if (gets[i]=="/n")
        //            {

        //                break;
        //            }
        //            wlength = Fontwidth + wlength;
        //            sLine = sLine + gets[i];

        //        }
        //        textviewlines++;
        //    }
        //    textviewlines = 0;
        //}

        //while (srr.EndOfStream != true)
        //{
        //    char[] buffer = new char[count - index];
        //    srr.ReadBlock(buffer, 0, buffer.Length);
        //    sLine = new String(buffer);
        //    textFiles.Add(sLine);
        //    index = count;
        //    count += 500;
        //}

        public void Paging(int page) //페이지 처리
        {
            // var mw = new MainWindow();
            MainWindow mw = (MainWindow)System.Windows.Application.Current.MainWindow;
            int Page = page;
            mw.PageNumber.Text = Page.ToString();
            if (page > endpagenumber)
            {
                Page = endpagenumber;
                PNumber = endpagenumber;
            }
            else if (page <= 1)
            {
                Page = 1;
                pnumber = 1;
            }
            mw.Maxpage.Content= "/"+ endpagenumber;
            Paragraph paragraph = new Paragraph();
            pnumber = Page;
            mw.PageNumber.Text = Page.ToString();
            paragraph.Inlines.Add(textfiles[Page - 1]);
            
            FlowDocument document = new FlowDocument(paragraph);
            mw.TextView.Document = document;

            document.Background = Brushes.Beige;
            paragraph.BorderBrush = Brushes.Blue;
            paragraph.FontFamily = new FontFamily("굴림");
            paragraph.FontSize = 14.0;
            paragraph.LineHeight = 1;
            paragraph.FontWeight = FontWeights.Normal;
            ThicknessConverter tc = new ThicknessConverter();
            paragraph.BorderThickness = (Thickness)tc.ConvertFromString("1");


        }
    }
}
