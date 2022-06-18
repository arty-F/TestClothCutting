using Assets.Scripts.StateMachine;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class PlayerMover : MonoBehaviour
    {
        #region settings

        private const float _speed = 10f;

        #endregion

        #region private fields

        [Inject]
        private StateMachine<GameState> gameStateMachine;

        private Vector3[] checkpoints;

        private int nextPointIndex;

        private bool isMoving;

        #endregion

        private void Awake()
        {
            gameStateMachine.Subscribe(GameState.LevelConstructed, GameState.ObjectStartMoving, OnStartMoving);
        }

        public void OnStartMoving()
        {
            isMoving = true;
        }

        public void SetMovingPoints(Vector3[] checkpoints)
        {
            this.checkpoints = checkpoints;
            nextPointIndex = 1;
        }

        private void Update()
        {
            if (!isMoving)
            {
                return;
            }

            MoveToNextPoint();

            //TODO Swipes
            //TODO Rotation to point
        }

        private void MoveToNextPoint()
        {
            if (transform.position == checkpoints[nextPointIndex])
            {
                if (nextPointIndex == checkpoints.Length - 1)
                {
                    isMoving = false;
                    gameStateMachine.ChangeStateTo(GameState.ObjectEndMoving);
                    return;
                }

                nextPointIndex++;
            }

            transform.position = Vector3.MoveTowards(transform.position, checkpoints[nextPointIndex], _speed * Time.deltaTime);
        }
    }
}
