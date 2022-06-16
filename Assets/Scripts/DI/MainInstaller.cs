using Assets.Scripts.Core;
using Assets.Scripts.LevelGeneration;
using Assets.Scripts.StateMachine;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    private StateMachine<GameState> gameStateMachine;

    [SerializeField]
    private LevelObjectsSettings levelObjectsSettings;

    [SerializeField]
    private CuttingPointsGenerationSettings cuttingPointsGenerationSettings;

    [SerializeField]
    private MeshGenerationSettings meshGenerationSettings;

    public override void InstallBindings()
    {
        gameStateMachine = new GameStateMachineInitializer().Create();
        Container.Bind<StateMachine<GameState>>().FromInstance(gameStateMachine).AsSingle();

        Container.Bind<LevelObjectsSettings>().FromInstance(levelObjectsSettings).AsSingle();
        Container.Bind<CuttingPointsGenerationSettings>().FromInstance(cuttingPointsGenerationSettings).AsSingle();
        Container.Bind<MeshGenerationSettings>().FromInstance(meshGenerationSettings).AsSingle();

        Container.Bind<IClothCreator>().To<ClothCreator>().AsSingle();
        Container.Bind<ICuttingPointsGenerator>().To<CuttingPointsGenerator>().AsSingle();
        Container.Bind<ICuttedMeshGenerator>().To<CuttedMeshGenerator>().AsSingle();
    }
}