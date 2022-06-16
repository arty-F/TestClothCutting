using System.Collections.Generic;

namespace Assets.Scripts.StateMachine
{
    public class GameStateMachineInitializer : IStateMachineInitializer<GameState>
    {
        public StateMachine<GameState> Create()
        {
            var defaultState = new StateNode<GameState>(GameState.Default);
            var appLoadedState = new StateNode<GameState>(GameState.AppLoaded);
            var levelGaneratedState = new StateNode<GameState>(GameState.LevelGanerated);
            var objectStartMovingState = new StateNode<GameState>(GameState.ObjectStartMoving);
            var objectEndMovingState = new StateNode<GameState>(GameState.ObjectEndMoving);

            defaultState.SetNextTransitions(new List<StateNode<GameState>>() { appLoadedState });
            appLoadedState.SetNextTransitions(new List<StateNode<GameState>>() { levelGaneratedState });
            levelGaneratedState.SetNextTransitions(new List<StateNode<GameState>>() { objectStartMovingState });
            objectEndMovingState.SetNextTransitions(new List<StateNode<GameState>>() { objectEndMovingState });

            return new StateMachine<GameState>(defaultState);
        }
    }
}
