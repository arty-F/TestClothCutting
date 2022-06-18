using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.StateMachine
{
    /// <summary>
    /// Node of a FSM.
    /// </summary>
    /// <typeparam name="T">Enum state.</typeparam>
    public class StateNode<T> where T : struct
    {
        /// <summary>
        /// State of a current node.
        /// </summary>
        public T Current { get; private set; }

        /// <summary>
        /// Possible transitions to next states.
        /// </summary>
        public List<StateNode<T>> Next { get; private set; }

        public StateNode(T current)
        {
            Current = current;
        }

        public StateNode(T current, List<StateNode<T>> next) : this(current)
        {
            Next = next;
        }

        /// <summary>
        /// Set naxt states collention to recieved vaue.
        /// </summary>
        /// <param name="next">Next states collection.</param>
        public void SetNextTransitions(List<StateNode<T>> next)
        {
            if (Next != null)
            {
                return;
            }

            Next = next;
        }

        /// <summary>
        /// Returns true when it is possible to move to the next state.
        /// </summary>
        /// <param name="nextState">Next state.</param>
        public bool CanMakeTransition(T nextState)
        {
            return Next != null && Next.Any(n => nextState.Equals(n.Current));
        }
    }
}
