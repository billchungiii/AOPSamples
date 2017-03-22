using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOPSample04
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "your.txt";
            //IFileAccess file = (new FileLogProxyFactory(filename)).GetInstane();
            //file.Create();

            IFileAccess file = new LogProxyFactory<IFileAccess>(typeof(FileAccess), new object[] { filename }).GetInstance();
            file.Delete();

            Console.ReadLine();
        }
    }
}
