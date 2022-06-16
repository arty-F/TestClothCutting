using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    [CreateAssetMenu(fileName = "MeshGenerationSettings", menuName = "ScriptableObjects/MeshGenerationSettings")]
    public class MeshGenerationSettings : ScriptableObject
    {
        public Vector3 StartedPoint;

        public int Size;

        public float XOffsetBetweenParts;

        private Vector3 bottomCenter;

        private Vector3 middleCenter;

        private void OnEnable()
        {
            var halfSize = Size * 0.5f;
            bottomCenter = new Vector3(halfSize + StartedPoint.x, StartedPoint.y, StartedPoint.z);
            middleCenter = new Vector3(halfSize + StartedPoint.x, halfSize + StartedPoint.y, StartedPoint.z);
        }

        public Vector3 BottomCenter()
        {
            return bottomCenter;
        }

        public Vector3 MiddleCenter()
        {
            return middleCenter;
        }
    }
}
