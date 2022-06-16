namespace Assets.Scripts.StateMachine
{
    public enum GameState : byte
    {
        Default,
        AppLoaded,
        LevelConstructed,
        ObjectStartMoving,
        ObjectEndMoving
    }
}
