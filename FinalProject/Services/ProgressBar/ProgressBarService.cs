using System;
using System.Collections.Generic;

namespace FinalProject.Services.ProgressBar
{
    public class ProgressBarService
    {
        public event Action OnProgressBarStart;
        public event Action OnProgressBarStop;

        private readonly object _tasksLock = new object();
        private readonly List<Guid> _tasks = new List<Guid>();

        public void Start(Guid id)
        {
            lock (_tasksLock)
            {
                if (_tasks.Count == 0)
                    OnProgressBarStart?.Invoke();
                _tasks.Add(id);
            }
        }

        public void Stop(Guid id)
        {
            lock (_tasksLock)
            {
                _tasks.Remove(id);
                if (_tasks.Count == 0)
                    OnProgressBarStop?.Invoke();
            }
        }
    }
}
