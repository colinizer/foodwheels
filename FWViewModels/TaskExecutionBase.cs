using FWCore.Utils;
using FWViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWViewModels
{
    public abstract class TaskExecutionBase : NotificationBase
    {
        protected TaskExecutionBase()
        {

        }

        protected async Task ExecuteAsync(Func<Task> TaskToRun)
        {
            try
            {
                IsExecuting = true;
                OnCanExecuteChanged();

                await TaskToRun();
            }
            catch (KnownException Exception)
            {
                var KEH = Repository.GetObject<IKnownExceptionHandler>();
                if (KEH == null)
                    throw;
                await KEH.HandleException(Exception);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsExecuting = false;
                OnCanExecuteChanged();
            }
        }

        protected virtual void OnCanExecuteChanged()
        {

        }

        private bool _IsExecuting;
        public bool IsExecuting
        {
            get { return _IsExecuting; }
            private set { NotifySet(ref _IsExecuting, value); }
        }
    }
}
