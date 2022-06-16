using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    public interface IClothCreator
    {
        public Vector3[] GetCuttingPoints();

        public void CreateCloth(CapsuleCollider[] sensitiveColliders, CuttingPointsGenerationSettings cuttingPointsGenerationSettings,
            MeshGenerationSettings meshGenerationSettings, GameObject clothPrefab);
    }
}
