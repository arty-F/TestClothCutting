using System;
using System.Collections.Generic;

namespace Assets.Scripts.StateMachine
{
    public class StateMachineActionContainer<T> where T : struct
    {
        public T From { get; private set; }

        public T To { get; private set; }

        private List<Action> actions;

        public StateMachineActionContainer(T from, T to)
        {
            From = from;
            To = to;
            actions = new List<Action>();
        }

        public void Subscribe(Action action)
        {
            if (actions.Contains(action))
            {
                return;
            }

            actions.Add(action);
        }

        public void Unsubscribe(Action action)
        {
            var index = actions.IndexOf(action);

            if (index == -1)
            {
                return;
            }

            actions.RemoveAt(index);
        }

        public void Invoke()
        {
            foreach (var action in actions)
            {
                action.Invoke();
            }
        }
    }
}
