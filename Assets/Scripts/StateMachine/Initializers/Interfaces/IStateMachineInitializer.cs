namespace Assets.Scripts.StateMachine
{
    public interface IStateMachineInitializer<T> where T : struct
    {
        public StateMachine<T> Create();
    }
}
