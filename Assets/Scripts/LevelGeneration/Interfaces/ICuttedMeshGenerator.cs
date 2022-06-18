using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    /// <summary>
    /// Generates two parts of cutted mesh.
    /// </summary>
    public interface ICuttedMeshGenerator
    {
        /// <summary>
        /// Returns one of two cutted mesh part.
        /// </summary>
        public Mesh GenerateMesh(MeshGenerationSettings settings, Vector3[] cuttingPoints, CuttedMeshPart meshPart);
    }
}