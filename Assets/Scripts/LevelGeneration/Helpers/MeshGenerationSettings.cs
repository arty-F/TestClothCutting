using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    /// <summary>
    /// Settings for mesh generation.
    /// </summary>
    [CreateAssetMenu(fileName = "MeshGenerationSettings", menuName = "ScriptableObjects/MeshGenerationSettings")]
    public class MeshGenerationSettings : ScriptableObject
    {
        [Tooltip("Mesh generation started point. Mesh will be сщтыегсе to the right and up from that point.")]
        public Vector3 StartedPoint;

        [Tooltip("Length and width of a generated mesh.")]
        public int Size;

        [Tooltip("Distance between cutted mesh parts.")]
        public float XOffsetBetweenParts;

        private Vector3 bottomCenter;

        private Vector3 middleCenter;

        private void OnEnable()
        {
            var halfSize = Size * 0.5f;
            bottomCenter = new Vector3(halfSize + StartedPoint.x, StartedPoint.y, StartedPoint.z);
            middleCenter = new Vector3(halfSize + StartedPoint.x, halfSize + StartedPoint.y, StartedPoint.z);
        }

        /// <summary>
        /// Bottom-center point of a current mesh.
        /// </summary>
        public Vector3 BottomCenter()
        {
            return bottomCenter;
        }

        /// <summary>
        /// Middle-center point of a current mesh.
        /// </summary>
        public Vector3 MiddleCenter()
        {
            return middleCenter;
        }
    }
}
