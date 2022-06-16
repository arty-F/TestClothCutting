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

        [Inject]
        private MainInstaller diInstaller;

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
            return diInstaller.CreatePlayer();
        }

        private void SetCubeMovingTrajectory(GameObject cube, Vector3[] points)
        {
            if (cube.TryGetComponent<PlayerMover>(out var mover))
            {
                mover.SetMovingPoints(points);
            }
        }
    }
}
