using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace AOPSample05
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface , AllowMultiple = false)]
    public sealed class InterceptorAttribute : ContextAttribute, IContributeObjectSink
    {
        public InterceptorAttribute() : base("Interceptor")
        {

        }

        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            // 決定整個訊息鏈         
            return new InterceptorHandler(nextSink);
        }
    }


    public sealed class InterceptorHandler : IMessageSink
    {

        /// <summary>
        /// 建構式, 要傳入呼叫者的訊息鏈
        /// </summary>
        /// <param name="nextSink">The next sink.</param>
        public InterceptorHandler(IMessageSink nextSink)
        {
            NextSink = nextSink;
        }

        public IMessageSink NextSink
        {
            get; private set;
        }


        public IMessage SyncProcessMessage(IMessage msg)
        {
            IMessage result;
            IMethodCallMessage call = msg as IMethodCallMessage;
            if (call == null)
            {
                // 如果不是 Method Call
                result = NextSink.SyncProcessMessage(msg);
                Console.WriteLine(" null call");
            }
            else
            {

                var methodAttributes =
                    Attribute.GetCustomAttributes(call.MethodBase, 
                    typeof(InterceptorMethodAttribute));
                if (methodAttributes == null)
                {
                    result = NextSink.SyncProcessMessage(msg);
                    Console.WriteLine("null Attribute");
                }
                else
                {
                    foreach (var attribute in methodAttributes)
                    {
                        ((InterceptorMethodAttribute)attribute).OnExecuting();
                    }

                    result = NextSink.SyncProcessMessage(msg);

                    foreach (var attribute in methodAttributes)
                    {
                        ((InterceptorMethodAttribute)attribute).OnExecuted();
                    }
                }
               
            }
            return result;
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            return null;
        }

    }
}
