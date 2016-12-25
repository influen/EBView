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
        //private Point TextView;
        private List<string> textfiles=new List<string>();
        private int endpagenumber=1;
        private int pnumber=1;
        

        public List<string> TextFiles { get{ return this.textfiles; } set{ } }
        public int EndPageNumber { get { return this.endpagenumber; } set { } }
        public int PNumber { get { return this.pnumber; } set { } }

        public TextRenders() { }

        //public Point TextViewSize() //리치박스 가로, 세로 사이즈 리턴
        //{
        //    Point textviewsize = new Point();

        //    textviewsize.X = mw.TextView.ActualWidth;
        //    textviewsize.Y = mw.TextView.ActualHeight;

        //    return TextView;
        //}
        public void TextRender(Point ViewSize) //텍스트 스플리트
        {
            MainWindow mw = (MainWindow)System.Windows.Application.Current.MainWindow;
            Point viewsize = ViewSize;
            
            float viewsizewidth = (float)viewsize.X;
            float viewsizeheight = (float)viewsize.Y;
            //string OneLine = string.Empty;
            //string 
            
            textfiles.Clear();
            FileOpen fo = (FileOpen)new FileOpen();
            StreamReader srr = fo.TextCopy();
            StringBuilder OneLine = new StringBuilder(1024);
            StringBuilder PageLine = new StringBuilder(1024);

           // List<string> textF = new List<string>();
            
            double pageheight = 0;


            while (!srr.EndOfStream)
            {
                bool lineend = true;

                while (lineend)//한줄커팅
                {
                    OneLine.Append(srr.Read());
                    FormattedText formattedText = new FormattedText(OneLine.ToString(), CultureInfo.GetCultureInfo("ko-kr"),
                                    FlowDirection.LeftToRight, new Typeface("Arial"), 14, Brushes.Black);
                    //if (ViewSize[0] < formattedText.Width)
                    int switchExpression = 0;
                    //문자길이가 가로사이즈보다 크거나 줄바꿈이면 라인 커팅
                    if ((formattedText.Width >= viewsizewidth) || (srr.Peek().Equals("\r"))) 
                    {
                        switchExpression = 1;
                    }
                    else if (pageheight >= viewsizeheight)//세로사이즈보다 크다면 페이지커팅
                    {
                        switchExpression = 2;
                    }

                    switch (switchExpression)
                    {
                        case 1:
                            PageLine.Append(OneLine);
                            OneLine.Clear();
                            pageheight = pageheight + (formattedText.Baseline + formattedText.Height);
                            break;
                
                        case 2:
                            textfiles.Add(PageLine.ToString());
                            PageLine.Clear();
                            lineend = false;
                            break;
                        default:
                            PageLine.Append(OneLine);
                            break;
                    }

                }
            }
            EndPageNumber = textfiles.Count;
            //PageNumber.Text = $"1/{EndPageNumber}";
            mw.PageNumber.Text = PNumber.ToString();

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
            Paragraph paragraph = new Paragraph();
            mw.PageNumber.Text = Page.ToString();
            if (page >= EndPageNumber)
            {
                Page = EndPageNumber;
            }
            else if (page <= 1)
            {
                Page = 1;
                PNumber = 1;
            }

            PNumber = page;
            mw.PageNumber.Text = Page.ToString();
            paragraph.Inlines.Add(textfiles[Page - 1]);

            FlowDocument document = new FlowDocument(paragraph);
            mw.TextView.Document = document;

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
    }
}
