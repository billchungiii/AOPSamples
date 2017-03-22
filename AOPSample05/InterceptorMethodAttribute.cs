using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOPSample05
{
    internal class LogHelper
    {
        internal static void Log()
        {
            Console.WriteLine("Log something......");
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class InterceptorMethodAttribute : Attribute
    {
        public abstract void OnExecuting();
        public abstract void OnExecuted();
    }


    public sealed class MethodLogAttribute : InterceptorMethodAttribute
    {
        public override void OnExecuted()
        { LogHelper.Log(); }

        public override void OnExecuting() { }
    }

    public sealed class MethodPreprocessAttribute : InterceptorMethodAttribute
    {
        public override void OnExecuted() { }

        public override void OnExecuting()
        { Console.WriteLine("Before Executing"); }
    }
}
