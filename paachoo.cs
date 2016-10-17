using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions; //正则表达式的命名空间

namespace test
{
    public delegate void testWeiTuo(string name);
    public delegate void GetWebData();
    class Program
    {
        #region 变量
        /// <summary>
        /// 所有页面读取的内容
        /// </summary>
        public static List<string> pageData = new List<string>();
        /// <summary>
        /// 初始化WebClient实例
        /// </summary>
        public static WebClient WebContent = new WebClient();
        /// <summary>
        /// Loading标志位
        /// </summary>
        static bool _loading = true;
        public static event Action CallBack;
        #endregion

        static void Main(string[] args)
        {
            #region List数组
            List<int> arr = new List<int>();
            for(var i = 0;i < 10; i++)
            {
                arr.Add(i);
                //Console.WriteLine();
            }
            #endregion
            #region 委托
            testWeiTuo wt = new testWeiTuo(intrr);
            wt += intss;  
            wt("AA");
            openInt("Aa", intrr);
            #endregion
            #region Event CallBack
            AsyncFunction o = new AsyncFunction();
            //o.Fun();
            #endregion
            #region Timer
            Timer Timmer;
            //Timmer = new Timer();

            #endregion

            #region 网页抓取
            GetWebData getWebData = getPageCon;
            getWebData.BeginInvoke(null ,null); //异步执行网页读取
            LoadingAnimate();
            if (pageData.Count > 0)
            {
                //Console.WriteLine(pageData[0]);
            }
            else
            {
                Console.WriteLine("没有读取到数据");
            }
            WebContent.Credentials = CredentialCache.DefaultCredentials;
            
            //Byte[] pageData = WebContent.DownloadData("https://movie.douban.com/top250"); //从指定网站下载数据
            //string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句            
            //string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
            //Console.WriteLine(pageHtml);//在控制台输入获取的内容
            //string[] getLink = GetLinks(pageHtml);//使用正则表达式获取页面链接

            //foreach (string Link in getLink)
            //{
            //    Console.WriteLine(Link);
            //}
            #endregion
            Console.ReadKey();
        }
        #region 方法
        /// <summary>
        /// 获取所有页面链接,读取所有页面内容存放到pageData中
        /// </summary>
        private static void getPageCon()
        {
            List<string> allMovieLink = new List<string>();
            for (int i = 0; i < 250; i += 25)
            {
                string linkTemp = "https://movie.douban.com/top250?start=" + i;
                allMovieLink.Add(linkTemp);
            }
            foreach (string getlink in allMovieLink)
            {
                Byte[] pageDatas = WebContent.DownloadData(getlink);
                string pageHtml = Encoding.UTF8.GetString(pageDatas);
                pageData.Add(pageHtml);
            }
            _loading = false;
        }
        /// <summary>
        /// 获取网页中的链接
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static string[] GetLinks(string html)
        {
            const string pattern = @"https://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase); //新建正则模式
            MatchCollection m = r.Matches(html); //获得匹配结果
            string[] links = new string[m.Count];

            for (int i = 0; i < m.Count; i++)
            {
                links[i] = m[i].ToString(); //提取出结果
            }
            return links;
        }
        /// <summary>
        /// Loading等待动画
        /// </summary>
        static void LoadingAnimate()
        {
            while (true)
            {
                // -/|\
                string[] lo = new string[] { "-", "\\", "|", "/" };
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine("Loading...{0}", lo[i]);
                    Thread.Sleep(500);
                    Console.Clear();
                }
                if (!_loading)
                {
                    Console.WriteLine("网页读取完毕!");
                    break;
                }
            }
        }
        #endregion
        

        private static void openInt(string name,testWeiTuo makeOpen)
        {
            makeOpen(name);
        }
        private static void intrr(string id)
        {
           // Console.WriteLine("Rr" + id);
        }
        private static void intss(string id)
        {
            //Console.WriteLine("Ss" + id);
        }

        public static void CheckOpen()
        {
            Console.WriteLine("Sleep Start...");
            Thread.Sleep(2000);
            Console.WriteLine("Sleep End....");
        }
       
    }
    class AsyncFunction
    {
        public event Action CallBack;
        private void Function_1()
        {
            for (var i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine("Funciton_1:{0}", i);
            }
        }
        private void Function_2()
        {
            for (var i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine("Function_2:{0}", i);
            }
        }
        public void Fun()
        {
            CallBack += AsyncFunction_CallBack;
            if (CallBack != null)
            {
                CallBack();
            }
        }

        void AsyncFunction_CallBack()
        {
            
            Function_1();
            Function_2();
        }
    }
    class demoNum
    {
        private int demoNumP = 1;
        public int Num
        {
            set 
            {
                demoNumP = value + demoNumP;
            }
            get
            {
                return demoNumP;
            }
        }
    }
}
