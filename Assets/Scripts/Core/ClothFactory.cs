using Assets.Scripts.LevelGeneration;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class ClothFactory
    {
        #region settings

        private const string _clothRootName = "ClothRoot";

        private const float _stretchingStiffness = 0.99f;

        private const float _bendingStiffness = 0.25f;

        private const float _friction = 0.5f;

        private const float _clothSolverFrequency = 120f;

        #endregion

        private readonly GameObject clothPrefab;

        private readonly ICuttingPointsGenerator pointsGenerator;

        private readonly ICuttedMeshGenerator meshGenerator;

        private readonly Vector3[] cuttingPoints;

        private readonly Vector3 startedPoint;

        public ClothFactory(CuttingPointsGenerationSettings cuttingPointsGenerationSettings, MeshGenerationSettings meshGenerationSettings,
            GameObject clothPrefab)
        {
            pointsGenerator = new CuttingPointsGenerator(cuttingPointsGenerationSettings, meshGenerationSettings);
            cuttingPoints = pointsGenerator.GenerateCuttingPoints();
            meshGenerator = new CuttedMeshGenerator(meshGenerationSettings, cuttingPoints);
            this.clothPrefab = clothPrefab;
            this.startedPoint = meshGenerationSettings.StartedPoint;
        }

        public Vector3[] GetCuttingPoints()
        {
            return cuttingPoints;
        }

        public GameObject CreateCloth(CapsuleCollider[] sensitiveColliders)
        {
            var root = new GameObject(_clothRootName);

            CreateClothPart(root, sensitiveColliders, CuttedMeshPart.Left);
            CreateClothPart(root, sensitiveColliders, CuttedMeshPart.Right);

            return root;
        }

        private void CreateClothPart(GameObject root, CapsuleCollider[] sensitiveColliders, CuttedMeshPart part)
        {
            var mesh = meshGenerator.GenerateMesh(part);
            var clothObj = GameObject.Instantiate(clothPrefab, startedPoint, Quaternion.identity, root.transform);
            clothObj.GetComponent<MeshFilter>().mesh = mesh;
            clothObj.GetComponent<MeshCollider>().sharedMesh = mesh;

            var clothComponent = clothObj.AddComponent(typeof(Cloth)) as Cloth;
            clothComponent.capsuleColliders = sensitiveColliders;
            clothComponent.stretchingStiffness = _stretchingStiffness;
            clothComponent.bendingStiffness = _bendingStiffness;
            clothComponent.friction = _friction;
            clothComponent.clothSolverFrequency = _clothSolverFrequency;

            ClothSkinningCoefficient[] newConstraints;
            newConstraints = clothComponent.coefficients;

            var centerPointInFirstRow = (int)(cuttingPoints[0].x * 0.5f);
            newConstraints[centerPointInFirstRow].maxDistance = 0;
            newConstraints[centerPointInFirstRow - 2].maxDistance = 0;

            var centerPointInLastRow = (int)((cuttingPoints[cuttingPoints.Length - 1].x + 1) * 0.5f);
            newConstraints[newConstraints.Length - centerPointInLastRow].maxDistance = 0;
            newConstraints[newConstraints.Length - centerPointInLastRow - 2].maxDistance = 0;

            clothComponent.coefficients = newConstraints;
            clothComponent.selfCollisionDistance = 1f;
        }
    }
}
