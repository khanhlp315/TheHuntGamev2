using System;
using System.Collections.Generic;
using TheHuntGame.Utilities;

namespace TheHuntGame.EventSystem
{
    public class EventSystem: MonoSingleton<EventSystem, EventSystemConfig>
    {
        List<IEventQueue> _eventQueues;

        public override void Initialize()
        {
            _eventQueues = new List<IEventQueue>();
            Runner.Runner.Instance.ScheduleLateUpdate(Dispatch);
            OnInitializeDone?.Invoke();
        }
        
        private EventQueue<T> EQ<T>() where T : struct, IEvent
        {
            int index = EventType<T>.Index;
            if (index == -1)
            {
                TypeManager<IEvent>.RegisterType(typeof(T));
                index = EventType<T>.SyncIndex();
                _eventQueues.Add(new EventQueue<T>());
            }

            return (EventQueue<T>)_eventQueues[index];
        }
        
        public void Bind<T>(Action<T> method, bool receiveImmediately = false) where T : struct, IEvent
        {
            EQ<T>().Bind(method, receiveImmediately);
        }

        public void Bind<T>(Action<T[], int> method, bool receiveImmediately = false) where T : struct, IEvent
        {
            EQ<T>().Bind(method, receiveImmediately);
        }

        public void Unbind<T>(Action<T> method) where T : struct, IEvent
        {
            EQ<T>().Unbind(method);
        }

        public void Unbind<T>(Action<T[], int> method) where T : struct, IEvent
        {
            EQ<T>().Unbind(method);
        }

        public void Emit<T>(T e = default(T)) where T : struct, IEvent
        {
            EQ<T>().Emit(e);
        }

        public void Emit<T>(T[] events) where T : struct, IEvent
        {
            EQ<T>().Emit(events);
        }

        public bool HasEventInQueue<T>() where T : struct, IEvent
        {
            return EQ<T>().HasEventInQueue();
        }

        private void Dispatch()
        {
            foreach (var eventQueue in _eventQueues)
            {
                eventQueue.Dispatch();
            }
        }

        public void ClearAll()
        {
            foreach (var eventQueue in _eventQueues)
            {
                eventQueue.ClearAll();
            }
        }

    }
}