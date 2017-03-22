using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOPSample03
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "my.txt";
            IFileAccess file = new FileAccessProxy(new FileAccess(filename ));
            file.Create();

            Console.ReadLine(); 
        }
    }
}
