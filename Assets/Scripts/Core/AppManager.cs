using Assets.Scripts.LevelGeneration;
using UnityEngine;
using Zenject;

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

        [Inject]
        private ClothFactory clothFactory;

        #endregion

        private void Awake()
        {
            var startPos = new Vector3(meshGenerationSettings.Size * 0.5f + meshGenerationSettings.StartedPoint.x,
                meshGenerationSettings.StartedPoint.y, meshGenerationSettings.StartedPoint.z);
            var cube = Instantiate(cubePrefab, startPos, Quaternion.identity);

            _ = clothFactory.CreateCloth(cube.GetComponents<CapsuleCollider>(), cuttingPointsGenerationSettings, meshGenerationSettings, clothPrefab);
        }
    }
}
