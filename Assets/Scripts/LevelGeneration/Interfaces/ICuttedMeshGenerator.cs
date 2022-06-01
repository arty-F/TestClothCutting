using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    public interface ICuttedMeshGenerator
    {
        public Mesh GenerateMesh(MeshGenerationSettings settings, Vector3[] cuttingPoints, CuttedMeshPart meshPart);
    }
}