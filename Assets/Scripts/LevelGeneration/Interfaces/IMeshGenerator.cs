using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    public interface IMeshGenerator
    {
        public Mesh GenerateMesh(int xSize, int ySize, Vector3 startedPoint);
    }
}