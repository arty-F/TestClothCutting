using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    /// <summary>
    /// Settings for generation mesh cutting points.
    /// </summary>
    [CreateAssetMenu(fileName = "CuttingPointsGenerationSettings", menuName = "ScriptableObjects/CuttingPointsGenerationSettings")]
    public class CuttingPointsGenerationSettings : ScriptableObject
    {
        [Tooltip("Minimum distance of one cut by Y coord.")]
        public float YCuttingDistanceMin;

        [Tooltip("Maximum distance of one cut by Y coord.")]
        public float YCuttingDistanceMax;

        [Tooltip("Minimum distance of one cut by X coord.")]
        public float XCuttingDistanceMin;

        [Tooltip("Maximum distance of one cut by X coord.")]
        public float XCuttingDistanceMax;
    }
}
