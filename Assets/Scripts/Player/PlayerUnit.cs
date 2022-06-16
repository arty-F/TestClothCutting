using Assets.Scripts.StateMachine;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class PlayerUnit : MonoBehaviour
    {
        #region settings

        private const float _moveDelay = 1f;

        #endregion

        [Inject]
        private StateMachine<GameState> gameStateMachine;

        private void Awake()
        {
            gameStateMachine.Subscribe(GameState.AppLoaded, GameState.LevelConstructed, OnLevelConstructed);
        }

        private void OnLevelConstructed()
        {
            StartCoroutine(WaitAndMove());
        }

        private IEnumerator WaitAndMove()
        {
            yield return new WaitForSeconds(_moveDelay);

            gameStateMachine.ChangeStateTo(GameState.ObjectStartMoving);
        }
    }
}
