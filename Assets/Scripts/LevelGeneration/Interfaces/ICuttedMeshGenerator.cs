using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    public interface ICuttedMeshGenerator
    {
        public Mesh GenerateMesh(CuttedMeshPart meshPart);
    }
}