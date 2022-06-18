namespace Assets.Scripts.StateMachine
{
    public interface IStateMachineInitializer<T> where T : struct
    {
        /// <summary>
        /// Creates and return initialized FSM of type <see cref="StateMachine{T}"/>.
        /// </summary>
        public StateMachine<T> Create();
    }
}
