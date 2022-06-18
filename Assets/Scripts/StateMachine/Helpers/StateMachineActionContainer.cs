using System;
using System.Collections.Generic;

namespace Assets.Scripts.StateMachine
{
    /// <summary>
    /// Represents container for storing actions which will be invoked, when FSM changed one specific state to another.
    /// </summary>
    /// <typeparam name="T">Enum state.</typeparam>
    public class StateMachineActionContainer<T> where T : struct
    {
        /// <summary>
        /// Initial state.
        /// </summary>
        public T From { get; private set; }

        /// <summary>
        /// Destination state.
        /// </summary>
        public T To { get; private set; }

        private List<Action> actions;

        public StateMachineActionContainer(T from, T to)
        {
            From = from;
            To = to;
            actions = new List<Action>();
        }

        /// <summary>
        /// Add action to invokation collection.
        /// </summary>
        public void Subscribe(Action action)
        {
            if (actions.Contains(action))
            {
                return;
            }

            actions.Add(action);
        }

        /// <summary>
        /// Remove action from invokation collection.
        /// </summary>
        public void Unsubscribe(Action action)
        {
            var index = actions.IndexOf(action);

            if (index == -1)
            {
                return;
            }

            actions.RemoveAt(index);
        }

        /// <summary>
        /// Invoke all stored actions.
        /// </summary>
        public void Invoke()
        {
            foreach (var action in actions)
            {
                action.Invoke();
            }
        }
    }
}
