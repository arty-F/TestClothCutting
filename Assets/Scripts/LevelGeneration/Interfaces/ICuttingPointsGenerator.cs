using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    public interface ICuttingPointsGenerator
    {
        public Vector3[] GenerateCuttingPoints();
    }
}
