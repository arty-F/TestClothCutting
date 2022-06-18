using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.StateMachine
{
    /// <summary>
    /// Generic FSM implementation.
    /// </summary>
    /// <typeparam name="T">Enum state.</typeparam>
    public class StateMachine<T> where T : struct
    {
        private List<StateMachineActionContainer<T>> actionContainers;

        private StateNode<T> currentState;

        public StateMachine(StateNode<T> currentState)
        {
            this.currentState = currentState;
            actionContainers = new List<StateMachineActionContainer<T>>();
        }

        /// <summary>
        /// Changed current FSM state to passed value.
        /// </summary>
        /// <param name="next">Next state.</param>
        public void ChangeStateTo(T next)
        {
            if (!currentState.CanMakeTransition(next))
            {
                return;
            }

            var current = currentState.Current;
            currentState = currentState.Next.First(n => next.Equals(n.Current));

            var container = GetContainerWithTransition(current, next);
            if (container != null)
            {
                container.Invoke();
            }
        }

        /// <summary>
        /// Subscribe to action invoking, when FSM changed it state.
        /// </summary>
        /// <param name="from">From state.</param>
        /// <param name="to">To state.</param>
        /// <param name="action">Action to invoke, when state changing.</param>
        public void Subscribe(T from, T to, Action action)
        {
            var container = GetContainerWithTransition(from, to);

            if (container == null)
            {
                container = new StateMachineActionContainer<T>(from, to);
                actionContainers.Add(container);
            }

            container.Subscribe(action);
        }

        /// <summary>
        /// Unsubscribe to action invoking, when FSM changed it state.
        /// </summary>
        /// <param name="from">From state.</param>
        /// <param name="to">To state.</param>
        /// <param name="action">Unsubscribed action.</param>
        public void Unsubscribe(T from, T to, Action action)
        {
            var container = GetContainerWithTransition(from, to);

            if (container == null)
            {
                return;
            }

            container.Unsubscribe(action);
        }

        private StateMachineActionContainer<T> GetContainerWithTransition(T from, T to)
        {
            return actionContainers.FirstOrDefault(a => from.Equals(a.From) && to.Equals(a.To));
        }
    }
}
