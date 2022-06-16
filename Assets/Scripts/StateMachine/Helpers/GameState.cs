namespace Assets.Scripts.StateMachine
{
    public enum GameState : byte
    {
        Default,
        AppLoaded,
        LevelGanerated,
        ObjectStartMoving,
        ObjectEndMoving
    }
}
