using Assets.Scripts.LevelGeneration;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class AppManager : MonoBehaviour
    {
        #region events



        #endregion

        #region private fields

        [SerializeField]
        private GameObject clothPrefab;

        [SerializeField]
        private GameObject cubePrefab;

        [SerializeField]
        private CuttingPointsGenerationSettings cuttingPointsGenerationSettings;

        [SerializeField]
        private MeshGenerationSettings meshGenerationSettings;

        private Vector3[] cuttingPoints;

        private ICuttingPointsGenerator pointsGenerator;

        private IMeshGenerator meshGenerator;

        private GameObject[] clothParts;

        #endregion

        private void Awake()
        {
            pointsGenerator = new CuttingPointsGenerator(cuttingPointsGenerationSettings, meshGenerationSettings);
            cuttingPoints = pointsGenerator.GenerateCuttingPoints();

            meshGenerator = new CuttedMeshGenerator(meshGenerationSettings, cuttingPoints);
            clothParts = new GameObject[2];

            var mesh = meshGenerator.GenerateMesh(CuttedMeshPart.Left);
            clothParts[0] = Instantiate(clothPrefab, meshGenerationSettings.StartedPoint, Quaternion.identity);
            clothParts[0].GetComponent<MeshFilter>().mesh = mesh;
            clothParts[0].GetComponent<MeshCollider>().sharedMesh = mesh;

            var cloth = clothParts[0].AddComponent(typeof(Cloth)) as Cloth;
            cloth.useGravity = false;
            //cloth.capsuleColliders = new CapsuleCollider[1] { collider };
            cloth.stretchingStiffness = 0.95f;
            cloth.bendingStiffness = 0.5f;
            cloth.friction = 0.5f;
            cloth.clothSolverFrequency = 240f;


            mesh = meshGenerator.GenerateMesh(CuttedMeshPart.Right);
            clothParts[1] = Instantiate(clothPrefab, meshGenerationSettings.StartedPoint, Quaternion.identity);
            clothParts[1].GetComponent<MeshFilter>().mesh = mesh;
            clothParts[1].GetComponent<MeshCollider>().sharedMesh = mesh;

            cloth = clothParts[1].AddComponent(typeof(Cloth)) as Cloth;
            cloth.useGravity = false;
            //cloth.capsuleColliders = new CapsuleCollider[1] { collider };
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
