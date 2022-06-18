using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    /// <summary>
    /// Creates a cloth with specified parameters.
    /// </summary>
    public interface IClothCreator
    {
        /// <summary>
        /// Return points on which the cloth cut was made.
        /// </summary>
        public Vector3[] GetCuttingPoints();

        /// <summary>
        /// Instantiate cloth GameObject to scene.
        /// </summary>
        public void CreateCloth(CapsuleCollider[] sensitiveColliders, CuttingPointsGenerationSettings cuttingPointsGenerationSettings,
            MeshGenerationSettings meshGenerationSettings, GameObject clothPrefab);
    }
}
