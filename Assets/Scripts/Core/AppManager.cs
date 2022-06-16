using Assets.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Core
{
    public class AppManager : MonoBehaviour
    {
        [Inject]
        private StateMachine<GameState> gameStateMachine;

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
