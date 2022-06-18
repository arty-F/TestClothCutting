using Assets.Scripts.StateMachine;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class PlayerMover : MonoBehaviour
    {
        #region settings

        private const float _moveSpeed = 10f;

        private const float _rotationDamping = 2f;

        #endregion

        #region private fields

        [Inject]
        private StateMachine<GameState> gameStateMachine;

        private Vector3[] checkpoints;

        private int nextPointIndex;

        private bool isMoving;

        private bool isRotatedToNextPoint;

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
            RotateToNextPoint();
        }

        private void MoveToNextPoint()
        {
            if (transform.position == checkpoints[nextPointIndex])
            {
                if (nextPointIndex == checkpoints.Length - 1)
                {
                    FinishPointReached();
                    return;
                }

                nextPointIndex++;
                isRotatedToNextPoint = false;
            }

            transform.position = Vector3.MoveTowards(transform.position, checkpoints[nextPointIndex], _moveSpeed * Time.deltaTime);
        }

        private void RotateToNextPoint()
        {
            if (isRotatedToNextPoint)
            {
                return;
            }

            var targetPoint = checkpoints[nextPointIndex] - transform.position;
            var zAngle = Vector3.Angle(targetPoint, transform.up);

            if (transform.eulerAngles.z == zAngle)
            {
                isRotatedToNextPoint = true;
                return;
            }

            var cross = Vector3.Cross(targetPoint, transform.up);
            if (cross.z > 0) zAngle = -zAngle;
            var desiredRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + zAngle);

            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * _rotationDamping);
        }

        private void FinishPointReached()
        {
            isMoving = false;
            gameStateMachine.ChangeStateTo(GameState.ObjectEndMoving);
        }
    }
}
