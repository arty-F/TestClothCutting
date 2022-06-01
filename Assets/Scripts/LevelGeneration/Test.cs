using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    public class Test : MonoBehaviour
    {
        public CapsuleCollider collider;

        private void Awake()
        {
            var meshSettings = new MeshGenerationSettings();
            meshSettings.StartedPoint = Vector3.zero;
            meshSettings.XSize = 20;
            meshSettings.YSize = 20;

            var cuttingPointsSettings = new CuttingPointsGenerationSettings();
            cuttingPointsSettings.YCuttingDistanceMin = 4f;
            cuttingPointsSettings.YCuttingDistanceMax = 10f;
            cuttingPointsSettings.XCuttingDistanceMin = 2f;
            cuttingPointsSettings.XCuttingDistanceMax = 5f;

            var cuttingPointsGenerator = new CuttingPointsGenerator(cuttingPointsSettings, meshSettings);
            var cuttingPoints = cuttingPointsGenerator.GenerateCuttingPoints();

            var meshGenerator = new CuttedMeshGenerator(meshSettings, cuttingPoints, CuttedMeshPart.Left);
            var mesh = meshGenerator.GenerateMesh();

            GetComponent<MeshFilter>().mesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;

            var cloth = gameObject.AddComponent(typeof(Cloth)) as Cloth;
            cloth.useGravity = false;
            cloth.capsuleColliders = new CapsuleCollider[1] { collider };
            cloth.stretchingStiffness = 0.95f;
            cloth.bendingStiffness = 0.5f;
            cloth.friction = 0.5f;
            cloth.clothSolverFrequency = 240f;
        }

        private void Start()
        {

        }
    }
}
