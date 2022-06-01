using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    public interface IMeshGenerator
    {
        public Mesh GenerateMesh(CuttedMeshPart meshPart);
    }
}