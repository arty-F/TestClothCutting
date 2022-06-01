using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        #region private

        private bool isFollowing;

        private Transform target;

        private Vector3 offset;

        #endregion

        private void Awake()
        {
            offset = transform.position;
        }

        public void StartFollow(Transform target)
        {
            isFollowing = true;
            this.target = target;
        }

        public void StopFollow()
        {
            isFollowing = false;
            target = null;
        }

        private void Update()
        {
            if (!isFollowing)
            {
                return;
            }

            transform.position = target.position + offset;
        }

    }
}
