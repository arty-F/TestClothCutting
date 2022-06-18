using Assets.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Core
{
    /// <summary>
    /// Managing app lifecycle.
    /// </summary>
    public class AppManager : MonoBehaviour
    {
        #region private fields

        [Inject]
        private StateMachine<GameState> gameStateMachine;

        #endregion

        private void Awake()
        {
            gameStateMachine.Subscribe(GameState.ObjectStartMoving, GameState.ObjectEndMoving, OnLevelCompleted);
        }

        private void Start()
        {
            gameStateMachine.ChangeStateTo(GameState.AppLoaded);
        }

        private void OnLevelCompleted()
        {
            SceneManager.LoadScene(0);
        }
    }
}
