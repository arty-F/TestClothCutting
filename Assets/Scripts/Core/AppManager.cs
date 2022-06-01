using Assets.Scripts.LevelGeneration;
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

        #endregion

        private void Awake()
        {
            var cube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity);
            var cubeColliders = cube.GetComponents<CapsuleCollider>();

            var clothFactory = new ClothFactory(cuttingPointsGenerationSettings, meshGenerationSettings, clothPrefab);
            var cloth = clothFactory.CreateCloth(cubeColliders);
        }
    }
}
