using Assets.Scripts.LevelGeneration;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public interface IClothFactory
    {
        public Vector3[] GetCuttingPoints();

        public GameObject CreateCloth(CapsuleCollider[] sensitiveColliders, CuttingPointsGenerationSettings cuttingPointsGenerationSettings,
            MeshGenerationSettings meshGenerationSettings, GameObject clothPrefab);
    }
}
