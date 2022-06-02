using Assets.Scripts.Core;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class PlayerMover : MonoBehaviour
    {
        #region settings

        private const float _speed = 10f;

        #endregion

        #region events

        public event Action FinishReached;

        #endregion

        #region private fields

        private Vector3[] checkpoints;

        private int nextPointIndex;

        private bool isMoving;

        #endregion

        public void StartMove()
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
                    FinishReached?.Invoke();
                    return;
                }

                nextPointIndex++;
            }

            transform.position = Vector3.MoveTowards(transform.position, checkpoints[nextPointIndex], _speed * Time.deltaTime);
        }
    }
}
