using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    [CreateAssetMenu(fileName = "CuttingPointsGenerationSettings", menuName = "ScriptableObjects/CuttingPointsGenerationSettings")]
    public class CuttingPointsGenerationSettings : ScriptableObject
    {
        public float YCuttingDistanceMin;

        public float YCuttingDistanceMax;

        public float XCuttingDistanceMin;

        public float XCuttingDistanceMax;
    }
}
