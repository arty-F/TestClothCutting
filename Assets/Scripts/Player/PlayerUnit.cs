using Assets.Scripts.StateMachine;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerUnit
    {
        #region settings

        private const int _moveDelay = 1000;

        #endregion

        public GameObject PlayerObj { get; private set; }

        private StateMachine<GameState> gameStateMachine;

        public PlayerUnit(StateMachine<GameState> gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
            gameStateMachine.Subscribe(GameState.AppLoaded, GameState.LevelConstructed, OnLevelConstructed);
        }

        public void SetPlayerObj(GameObject playerObj)
        {
            if (PlayerObj != null)
            {
                return;
            }

            PlayerObj = playerObj;
        }

        private void OnLevelConstructed()
        {
            WaitAndMove();
        }

        private async void WaitAndMove()
        {
            await Task.Delay(_moveDelay);

            gameStateMachine.ChangeStateTo(GameState.ObjectStartMoving);
        }
    }
}
