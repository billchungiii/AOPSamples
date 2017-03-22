using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOPSample03
{
    public class FileAccessProxy : IFileAccess
    {
        private IFileAccess _fileAccess;
        public FileAccessProxy (IFileAccess fileAccess)
        {
            _fileAccess = fileAccess;
        }

        public void Create()
        {
            _fileAccess.Create();
            ExecuteLog();
        }

        public void Delete()
        {
            _fileAccess.Delete();
            ExecuteLog();
        }

        public void Read()
        {
            _fileAccess.Read();
            ExecuteLog();
        }

        public void Write()
        {
            _fileAccess.Write();
            ExecuteLog();
        }

        private void ExecuteLog()
        {
            LogHelper.Log();
        }
    }


    public class LogHelper
    {
        public static void Log()
        {
            Console.WriteLine("Log something......");
        }
    }
}
