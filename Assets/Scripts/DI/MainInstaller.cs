using Assets.Scripts.Core;
using Assets.Scripts.LevelGeneration;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private LevelManager levelManager;

    public override void InstallBindings()
    {
        Container.Bind<IClothFactory>().To<ClothFactory>().AsSingle().NonLazy();
        Container.Bind<ICuttingPointsGenerator>().To<CuttingPointsGenerator>().AsSingle().NonLazy();
        Container.Bind<ICuttedMeshGenerator>().To<CuttedMeshGenerator>().AsSingle().NonLazy();
        Container.Bind<LevelManager>().FromInstance(levelManager).AsSingle().NonLazy();
    }
}