using System;
using System.Collections;
using System.Collections.Generic;
using TheHuntGame.Utilities;
using UnityEngine;


namespace TheHuntGame.Runner
{
    public class Runner:MonoSingleton<Runner, RunnerConfig>
    {
        private List<Action> _mainThreadActionQueue = new List<Action>();
        private List<Action> _updateActionQueue = new List<Action>();
        private List<Action> _lateUpdateActionQueue = new List<Action>();
        private object _lockCall = new object();

        public override void Initialize()
        {
            OnInitializeDone?.Invoke();
        }
        
        private void Update()
        {
            foreach (var action in _updateActionQueue)
            {
                action.Invoke();
            }
        }
        
        private void LateUpdate()
        {
            foreach (var action in _lateUpdateActionQueue)
            {
                action.Invoke();
            }
        }
        
        private IEnumerator MainThreadUpdater()
        {
            while (true)
            {
                lock (_lockCall)
                {
                    if (_mainThreadActionQueue.Count > 0)
                    {
                        foreach (var action in _mainThreadActionQueue)
                        {
                            action.Invoke();
                        }

                        _mainThreadActionQueue.Clear();
                    }
                }

                yield return null;
            }
        }
        
        public void ScheduleUpdate(Action action)
        {
            _updateActionQueue.Add(action);
        }

        public void UnscheduleUpdate(Action action)
        {
            _updateActionQueue.Remove(action);
        }

        public void ScheduleLateUpdate(Action action)
        {
            _lateUpdateActionQueue.Add(action);
        }

        public void UnscheduleLateUpdate(Action action)
        {
            _lateUpdateActionQueue.Remove(action);
        }

        public void CallOnMainThread(Action func)
        {
            if (func == null)
            {
                throw new System.Exception("Function can not be null");
            }

            lock (_lockCall)
            {
                _mainThreadActionQueue.Add(func);
            }
        }
    }
}