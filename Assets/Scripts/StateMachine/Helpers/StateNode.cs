using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.StateMachine
{
    public class StateNode<T> where T : struct
    {
        public T Current { get; private set; }

        public List<StateNode<T>> Next { get; private set; }

        public StateNode(T current)
        {
            Current = current;
        }

        public StateNode(T current, List<StateNode<T>> next) : this(current)
        {
            Next = next;
        }

        public void SetNextTransitions(List<StateNode<T>> next)
        {
            if (Next != null)
            {
                return;
            }

            Next = next;
        }

        public bool CanMakeTransition(T nextState)
        {
            return Next != null && Next.Any(n => nextState.Equals(n.Current));
        }
    }
}
