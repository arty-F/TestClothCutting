using Assets.Scripts.Core;
using Assets.Scripts.LevelGeneration;
using Assets.Scripts.StateMachine;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private LevelManager levelManager;

    private StateMachine<GameState> gameStateMachine;

    public override void InstallBindings()
    {
        gameStateMachine = new GameStateMachineInitializer().Create();
        Container.Bind<StateMachine<GameState>>().FromInstance(gameStateMachine).AsSingle();

        Container.Bind<IClothCreator>().To<ClothCreator>().AsSingle();
        Container.Bind<ICuttingPointsGenerator>().To<CuttingPointsGenerator>().AsSingle();
        Container.Bind<ICuttedMeshGenerator>().To<CuttedMeshGenerator>().AsSingle();
        Container.Bind<LevelManager>().FromInstance(levelManager).AsSingle();
    }
}