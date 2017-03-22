using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;

namespace AOPSample04
{
    public class LogProxy<T> : RealProxy where T : class
    {

        private T _instance;
        public LogProxy(T instance) : base(typeof(T))
        {
            _instance = instance;
        }
        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            object result = methodInfo.Invoke(_instance, methodCall.InArgs);
            LogHelper.Log();
            return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
        }
    }

   
    public class LogProxyFactory<T> where T : class
    {
        LogProxy<T> _proxy;
        public LogProxyFactory(Type realType, object[] args)
        {
            T instance = (T)Activator.CreateInstance(realType, args);
            _proxy = new LogProxy<T>(instance);
        }

        public T GetInstance()
        {
            var remoteObj = _proxy.GetTransparentProxy();
            return (T)remoteObj;
        }

    }

   

    internal class LogHelper
    {
        internal static void Log()
        {
            Console.WriteLine("Log something......");
        }
    }
}
