using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOPSample05
{

   
    public interface IFileAccess
    {
     
        void Create();
        void Delete();
        void Write();
        void Read();
    }


    public abstract class FileAccess : ContextBoundObject, IFileAccess
    {
        public abstract void Create();
        public abstract void Delete();
        public abstract void Read();
        public abstract void Write();
    }


    [Interceptor]
    public class RealFileAccess : FileAccess
    {
        private string _file;
        public RealFileAccess(string filename)
        { _file = filename; }


        [MethodPreprocess]
        [MethodLog]
        public override void Create()
        { Console.WriteLine(string.Format("Create a file : {0}", _file)); }

        [MethodPreprocess]
        public override void Delete()
        { Console.WriteLine(string.Format("Delete a file :{0}", _file)); }

        [MethodLog]
        public override void Read()
        { Console.WriteLine(string.Format("Read from a file :{0}", _file)); }

        public override void Write()
        { Console.WriteLine(string.Format("Write to a file :{0}", _file)); }
    }
}
