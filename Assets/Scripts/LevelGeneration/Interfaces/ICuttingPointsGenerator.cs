using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    /// <summary>
    /// Generation poins collction for mesh cutting.
    /// </summary>
    public interface ICuttingPointsGenerator
    {
        /// <summary>
        /// Returns point collection, on these points will be mesh cutted.
        /// </summary>
        public Vector3[] GenerateCuttingPoints(CuttingPointsGenerationSettings pointSettings, MeshGenerationSettings meshSettings);
    }
}
