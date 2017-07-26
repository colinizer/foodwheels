using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCore.Utils
{
    public class KnownException : Exception
    {
        public KnownException(string Message) : base(Message) { }

        public KnownException(string Message, Exception InnerException) : base(Message, InnerException) { }

        public static void Wrap(Action Action, string Message)
        {
            try
            {
                Action();
            }
            catch (Exception Exception)
            {
                throw new KnownException(Message, Exception);
            }
        }
        public async static Task Wrap(Func<Task> Func, string Message)
        {
            try
            {
                await Func();
            }
            catch (Exception Exception)
            {
                throw new KnownException(Message, Exception);
            }
        }

        public async static Task<T> Wrap<T>(Func<Task<T>> Func, string Message)
        {
            try
            {
                return await Func();
            }
            catch (Exception Exception)
            {
                throw new KnownException(Message, Exception);
            }
        }
    }
}
