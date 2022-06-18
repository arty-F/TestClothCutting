using Assets.Scripts.LevelGeneration;
using Assets.Scripts.Player;
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

    private PlayerUnit player;

    public override void InstallBindings()
    {
        gameStateMachine = new GameStateMachineInitializer().Create();
        Container.Bind<StateMachine<GameState>>().FromInstance(gameStateMachine).AsSingle();

        Container.Bind<LevelObjectsSettings>().FromInstance(levelObjectsSettings).AsSingle();
        Container.Bind<CuttingPointsGenerationSettings>().FromInstance(cuttingPointsGenerationSettings).AsSingle();
        Container.Bind<MeshGenerationSettings>().FromInstance(meshGenerationSettings).AsSingle();

        Container.Bind<IClothCreator>().To<ClothCreator>().AsSingle();
        Container.Bind<ICuttingPointsGenerator>().To<ZigzagCuttingPointsGenerator>().AsSingle();
        Container.Bind<ICuttedMeshGenerator>().To<CuttedMeshGenerator>().AsSingle();

        Container.Bind<MainInstaller>().FromInstance(this).AsSingle();

        player = new PlayerUnit(gameStateMachine);
        Container.Bind<PlayerUnit>().FromInstance(player).AsSingle();
    }

    /// <summary>
    /// Instantiated player character GameObject and storing reference in <see cref="PlayerUnit"/>.
    /// </summary>
    public GameObject CreatePlayer()
    {
        var playerObj = Container.InstantiatePrefab(levelObjectsSettings.CubePrefab, 
            meshGenerationSettings.BottomCenter(), Quaternion.identity, null);

        player.SetPlayerObj(playerObj);

        return playerObj;
    }
}