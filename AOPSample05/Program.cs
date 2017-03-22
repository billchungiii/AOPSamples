using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOPSample05
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileAccess file = new RealFileAccess("Book.txt");
            ((FileAccess)file).Create();
           // file.Create();

            file.Delete(); 


            Console.ReadLine(); 
        }
    }
}
