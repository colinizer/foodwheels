using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FWCore.Utils
{
    public class NotificationBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        protected bool NotifySet<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }

        protected void NotifyPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var memberExpr = propertyExpression.Body as MemberExpression;
            if (memberExpr == null)
                throw new ArgumentException("propertyExpression should represent access to a member");

            string memberName = memberExpr.Member.Name;
            NotifyPropertyChanged(memberName);
        }
    }
}
