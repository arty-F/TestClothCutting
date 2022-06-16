using Assets.Scripts.LevelGeneration;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachine;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
    public class LevelConstructor : MonoBehaviour
    {
        #region private fields

        [Inject]
        private LevelObjectsSettings levelObjectsSettings;

        [Inject]
        private CuttingPointsGenerationSettings cuttingPointsGenerationSettings;

        [Inject]
        private MeshGenerationSettings meshGenerationSettings;

        [Inject]
        private IClothCreator clothCreator;

        [Inject]
        private StateMachine<GameState> gameStateMachine;

        /*[Inject]
        private PlayerUnit player;*/

        [Inject]
        private DiContainer diContainer;

        #endregion

        private void Awake()
        {
            gameStateMachine.Subscribe(GameState.Default, GameState.AppLoaded, OnAppLoaded);
        }

        private void OnAppLoaded()
        {
            ConstructLevel();
        }

        private void ConstructLevel()
        {
            var cube = CreateCube();

            clothCreator.CreateCloth(cube.GetComponents<CapsuleCollider>(), cuttingPointsGenerationSettings, meshGenerationSettings, 
                levelObjectsSettings.ClothPrefab);

            SetCubeMovingTrajectory(cube.gameObject, clothCreator.GetCuttingPoints());

            gameStateMachine.ChangeStateTo(GameState.LevelConstructed);
        }

        private GameObject CreateCube()
        {
            var startPos = new Vector3(meshGenerationSettings.Size * 0.5f + meshGenerationSettings.StartedPoint.x,
                meshGenerationSettings.StartedPoint.y, meshGenerationSettings.StartedPoint.z);
            return diContainer.InstantiatePrefab(levelObjectsSettings.CubePrefab, startPos, Quaternion.identity, null);
        }

        private void SetCubeMovingTrajectory(GameObject cube, Vector3[] points)
        {
            if (cube.TryGetComponent<PlayerMover>(out var mover))
            {
                mover.SetMovingPoints(points);
            }
        }

        /*private IEnumerator WaitAndStartMoving(GameObject cube)
        {
            yield return new WaitForSeconds(_gameStartingDelay);

            if (cube.TryGetComponent<PlayerMover>(out var mover))
            {
                mover.StartMove();
            }
            TrackedObjectStartsMoving?.Invoke(cube);
        }*/
    }
}
