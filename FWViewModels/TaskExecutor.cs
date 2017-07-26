using FWCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWViewModels
{
    public class TaskExecutor : TaskExecutor<bool>
    {
        public TaskExecutor(Func<Task> TaskToRun) : base((o) => TaskToRun())
        {            
        }

        public Task ExecuteAsync()
        {
            return base.ExecuteAsync(null);
        }

        public async void Execute()
        {
            await ExecuteAsync();
        }
    }

    public class TaskExecutor<T> : TaskExecutionBase
    {
        public TaskExecutor(Func<T,Task> TaskToRun)
        {
            _TaskToRun = TaskToRun;
        }

        Func<T,Task> _TaskToRun;

        public Task ExecuteAsync(T param)
        {
            return base.ExecuteAsync(() =>_TaskToRun(param));
        }

        public async void Execute(T param)
        {
            await ExecuteAsync(param);
        }
    }
}
