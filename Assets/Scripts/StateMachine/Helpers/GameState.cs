namespace Assets.Scripts.StateMachine
{
    /// <summary>
    /// Represents state of a game.
    /// </summary>
    public enum GameState : byte
    {
        Default,

        /// <summary>
        /// All game classes are ready to use.
        /// </summary>
        AppLoaded,

        /// <summary>
        /// Game level is successfully generated and instantiated.
        /// </summary>
        LevelConstructed,

        /// <summary>
        /// Player character starts moving.
        /// </summary>
        ObjectStartMoving,

        /// <summary>
        /// Player character is reached end point.
        /// </summary>
        ObjectEndMoving
    }
}
