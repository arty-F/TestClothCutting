using Assets.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        #region settings

        private const float _zOffset = -60f;

        #endregion

        private void Start()
        {
            //levelManager.TrackedObjectStartsMoving += OnObjectStartsMoving;
            //TODO Calculating started position and zoom
        }

        private void OnObjectStartsMoving(GameObject obj)
        {
            //TODO Smooth following and cube size calculating
            transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z + _zOffset);
            transform.parent = obj.transform;
            
        }

        private void OnDestroy()
        {
            //levelManager.TrackedObjectStartsMoving -= OnObjectStartsMoving;
        }
    }
}
