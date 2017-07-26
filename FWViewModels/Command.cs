using FWCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FWViewModels
{
    public class Command : TaskExecutionBase, ICommand
    {
        public Command(Action Exec, Func<bool> CanExec = null)
        {
            _Exec = Exec;
            _CanExec = CanExec;
        }

        public Command(Action<object> ExecObj, Func<bool> CanExec)
        {
            _ExecObj = ExecObj;
            _CanExec = CanExec;
        }

        public Command(Func<Task> ExecTask, Func<bool> CanExec = null)
        {
            _ExecTask = ExecTask;
            _CanExec = CanExec;
        }

        public Command(Func<object, Task> ExecObjTask, Func<bool> CanExec = null)
        {
            _ExecObjTask = ExecObjTask;
            _CanExec = CanExec;
        }

        protected override void OnCanExecuteChanged()
        {
            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if(_CanExec == null)
                return !IsExecuting;
            return !IsExecuting && _CanExec();
        }

        public void Execute()
        {
            Execute(null);
        }
        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }
        public async Task ExecuteAsync()
        {
            await ExecuteAsync(null);
        }
        public async Task ExecuteAsync(object parameter)
        {
            if (_ExecObjTask != null)
                await base.ExecuteAsync(() => _ExecObjTask(parameter));
            else if (_ExecTask != null)
                await base.ExecuteAsync(() =>_ExecTask());
            else if (_ExecObj != null)
                _ExecObj(parameter);
            else
                _Exec();

            //try
            //{
            //    IsExecuting = true;
            //    RaiseCanExecuteChanged();

            //    if (_ExecObjTask != null)
            //        await _ExecObjTask(parameter);
            //    else if (_ExecTask != null)
            //        await _ExecTask();
            //    else if (_ExecObj != null)
            //        _ExecObj(parameter);
            //    else
            //        _Exec();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    IsExecuting = false;
            //    RaiseCanExecuteChanged();
            //}
        }

        //private bool _IsExecuting;

        //public bool IsExecuting
        //{
        //    get { return _IsExecuting; }
        //    private set { NotifySet(ref _IsExecuting, value); }
        //}

        Func<object, Task> _ExecObjTask;
        Func<Task> _ExecTask;
        Action _Exec;
        Action<object> _ExecObj;
        Func<bool> _CanExec;
    }
}
