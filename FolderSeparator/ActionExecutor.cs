using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderSeparator
{
    internal class ActionExecutor
    {
        const int maxTaskCount = 5;
        readonly Queue<Action> actionPool = new Queue<Action>();
        readonly Task[] taskArray = new Task[maxTaskCount];

        private void EnableListWindow(int index)
        {
            if (taskArray[index] == null)
            {
                taskArray[index] = Task.Run(() =>
                {
                    while (actionPool.Count > 0)
                    {
                        var action = actionPool.Dequeue();

                        action();
                    }

                    taskArray[index] = null;
                });
            }
        }

        private void ExecuteActions()
        {
            for (int i = 0; i < maxTaskCount; i++)
            {
                EnableListWindow(i);
            }
        }

        internal void AddActionToQueue(Action action)
        {
            actionPool.Enqueue(action);
            if (actionPool.Count == 1)
            {
                ExecuteActions();
            }
        }

        internal Task WaitForFinish()
        {
            return Task.Run(async () =>
            {
                foreach (var task in taskArray)
                {
                    if (task != null)
                        await task;
                }
            });
        }
    }
}
