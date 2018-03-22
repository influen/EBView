using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;


namespace EBView
{
    public class FileOpen
    {

        //필드
        private static string filepath;
        private static string filename;
        struct textfileinfo
        {
            public long filesize;
            public int stringcount;
            public string filename;
            public textfileinfo(long filesize, int stringcount, string filename)
            {
                this.filesize = filesize;
                this.filename = filename;
                this.stringcount = stringcount;
            }
        }

        //프로퍼티
        public static string Filepath { get { return filepath; } set { filepath = value; } }
        public static string Filename { get { return filename; } set { filename = value; } }

        //클래스 생성자 필드초기화
        public FileOpen()
        {

        }

        //메서드
        public static void OpenFile() //파일오픈
        {
            Microsoft.Win32.OpenFileDialog ofdd = new Microsoft.Win32.OpenFileDialog();
            ofdd.DefaultExt = "*.txt";
            ofdd.Filter = "Text files| *.txt|All Files|*.*";
            ofdd.Title = "select Text file";
            MainWindow mw = (MainWindow)System.Windows.Application.Current.MainWindow;


            filepath = mw.FilePath.Text.Trim();
            TextRenders tr = (TextRenders)new TextRenders();
            if (filepath.Any())
            {

                if (FilePathCheck(filepath) == true)
                {
                    if (filepath != mw.FilePath.Text)
                    {
                        filepath = mw.FilePath.Text;
                        tr.TextRender(mw.ViewSize);
                        tr.Paging(1);
                    }
                    else
                    {
                        if (ofdd.ShowDialog() == true)
                        {
                            filepath = ofdd.FileName;
                            mw.FilePath.Text = ofdd.FileName;
                            tr.TextRender(mw.ViewSize);
                            tr.Paging(1);
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show(mw.FilePath.Text + " 파일을 찾을수없습니다.");
                    if (ofdd.ShowDialog() == true)
                    {
                        filepath = ofdd.FileName;
                        mw.FilePath.Text = ofdd.FileName;
                        tr.TextRender(mw.ViewSize);
                        tr.Paging(1);
                    }
                }
            }
            else if (ofdd.ShowDialog() == true)
            {
                mw.FilePath.Text = ofdd.FileName;
                filepath = ofdd.FileName;
                tr.TextRender(mw.ViewSize);
                tr.Paging(1);
            }
            
        }

        public static bool FilePathCheck(string filepath) //파일 체크
        {
            string path = filepath;
            bool check = false;

            FileInfo fi = new FileInfo(path);
            if (fi.Exists == true)
            {
                check = true;
            }
            
            return check;
        }

        public string TextCopy() //파일 메모리저장
        {
           
            using (StreamReader sr = new StreamReader(filepath, true))
            {


                Encoding enc = sr.CurrentEncoding ;
               
                ObjectCache cache = MemoryCache.Default;
                string filenames = cache["filenames"] as string;
                filenames = File.ReadAllText(filepath);
                
                //if (filenames == null)
                // {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(300.0);
                List<string> filepaths = new List<string>();
                filepaths.Add(filepath);
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(filepaths));
                filenames = File.ReadAllText(filepath, enc);
                cache.Set("filenames", filenames, policy);


                //            }
                //string memString = mem.ReadToEnd();

                // var bytes = Encoding.UTF8.GetBytes(memString);
                //  byte[] buffer = Encoding.UTF8.GetBytes(filenames);
                // var bytes = File.ReadAllBytes(path);
                //  var bytes = Encoding.UTF8.GetBytes(path);
                //  MemoryStream memcopy = new MemoryStream(buffer);

                //StreamReader sr = new StreamReader(memcopy,Encoding.UTF8);
                // using (var sr = new BinaryReader(memcopy, Encoding.UTF8))
                //    var sss=Encoding.UTF8.GetString(memcopy.ToArray());
                // StreamReader sr = new StreamReader(memcopy);
                return filenames;
            }
        }
    }
}