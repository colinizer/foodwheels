using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCore.Utils
{
    public class Repository
    {
        private static Dictionary<Type, object> _Lookup = new Dictionary<Type, object>();

        public static void StoreFactory<T>(Func<T> Factory)
        {
            _Lookup[typeof(T)] = Factory;
        }

        public static T GetObject<T>()
        {
            object o = null;
            if (_Lookup.TryGetValue(typeof(T), out o))
            {
                var factory = (Func<T>)o;
                return (T)factory();
            }
            return default(T);
        }
    }
}
