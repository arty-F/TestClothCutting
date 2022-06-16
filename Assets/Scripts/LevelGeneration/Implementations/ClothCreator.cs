using UnityEngine;
using Zenject;

namespace Assets.Scripts.LevelGeneration
{
    public class ClothCreator : IClothCreator
    {
        #region settings

        private const string _clothRootName = "ClothRoot";

        private const float _stretchingStiffness = 1f;

        private const float _bendingStiffness = 0.75f;

        private const float _friction = 0.5f;

        private const float _clothSolverFrequency = 120f;

        #endregion

        [Inject]
        private ICuttingPointsGenerator pointsGenerator;

        [Inject] 
        private ICuttedMeshGenerator meshGenerator;

        private Vector3[] cuttingPoints;

        public Vector3[] GetCuttingPoints()
        {
            return cuttingPoints;
        }

        public void CreateCloth(CapsuleCollider[] sensitiveColliders, CuttingPointsGenerationSettings cuttingPointsGenerationSettings,
            MeshGenerationSettings meshGenerationSettings, GameObject clothPrefab)
        {
            var root = new GameObject(_clothRootName);

            var left = CreateClothPart(sensitiveColliders, CuttedMeshPart.Left, cuttingPointsGenerationSettings, meshGenerationSettings, clothPrefab);
            left.transform.parent = root.transform;
            
            var right = CreateClothPart(sensitiveColliders, CuttedMeshPart.Right, cuttingPointsGenerationSettings, meshGenerationSettings, clothPrefab);
            right.transform.parent = root.transform;
        }

        private GameObject CreateClothPart(CapsuleCollider[] sensitiveColliders, CuttedMeshPart part,
            CuttingPointsGenerationSettings cuttingPointsGenerationSettings, MeshGenerationSettings meshGenerationSettings, GameObject clothPrefab)
        {
            cuttingPoints = pointsGenerator.GenerateCuttingPoints(cuttingPointsGenerationSettings, meshGenerationSettings);

            var mesh = meshGenerator.GenerateMesh(meshGenerationSettings, cuttingPoints, part);
            var clothObj = GameObject.Instantiate(clothPrefab, meshGenerationSettings.StartedPoint, Quaternion.identity);
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

            //TODO Refactoring and improve edges calculation
            var centerPointInFirstRow = (int)(cuttingPoints[0].x * 0.5f);
            newConstraints[centerPointInFirstRow].maxDistance = 0;
            newConstraints[centerPointInFirstRow - (meshGenerationSettings.Size / 10)].maxDistance = 0;
            newConstraints[centerPointInFirstRow - (meshGenerationSettings.Size / 5)].maxDistance = 0;

            var centerPointInLastRow = (int)((cuttingPoints[cuttingPoints.Length - 1].x + 1) * 0.5f);
            newConstraints[newConstraints.Length - centerPointInLastRow].maxDistance = 0;
            newConstraints[newConstraints.Length - centerPointInLastRow - (meshGenerationSettings.Size / 10)].maxDistance = 0;
            newConstraints[newConstraints.Length - centerPointInLastRow - (meshGenerationSettings.Size / 5)].maxDistance = 0;

            clothComponent.coefficients = newConstraints;
            clothComponent.selfCollisionDistance = 1f;

            return clothObj;
        }
    }
}
