using Assets.Scripts.LevelGeneration;
using Assets.Scripts.Player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Core
{
    public class LevelManager : MonoBehaviour
    {
        #region settings

        private const float _gameStartingDelay = 1f;

        #endregion

        #region events

        public event Action<GameObject> TrackedObjectStartsMoving;

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
        private IClothFactory clothFactory;

        #endregion

        private void Start()
        {
            StartLevel();
        }

        private void StartLevel()
        {
            var cube = CreateCube();

            _ = clothFactory.CreateCloth(cube.GetComponents<CapsuleCollider>(), cuttingPointsGenerationSettings, meshGenerationSettings, clothPrefab);

            SetCubeMovingTrajectory(cube, clothFactory.GetCuttingPoints());

            StartCoroutine(WaitAndStartMoving(cube));
        }

        private GameObject CreateCube()
        {
            var startPos = new Vector3(meshGenerationSettings.Size * 0.5f + meshGenerationSettings.StartedPoint.x,
                meshGenerationSettings.StartedPoint.y, meshGenerationSettings.StartedPoint.z);
            return Instantiate(cubePrefab, startPos, Quaternion.identity);
        }

        private void SetCubeMovingTrajectory(GameObject cube, Vector3[] points)
        {
            if (cube.TryGetComponent<PlayerMover>(out var mover))
            {
                mover.SetMovingPoints(points);
                mover.FinishReached += OnFinishPointReached;
            }
        }

        private IEnumerator WaitAndStartMoving(GameObject cube)
        {
            yield return new WaitForSeconds(_gameStartingDelay);

            if (cube.TryGetComponent<PlayerMover>(out var mover))
            {
                mover.StartMove();
            }
            TrackedObjectStartsMoving?.Invoke(cube);
        }

        private void OnFinishPointReached()
        {
            SceneManager.LoadScene(0);
        }
    }
}
