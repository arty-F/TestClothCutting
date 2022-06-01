using Assets.Scripts.Core;
using Assets.Scripts.LevelGeneration;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ClothFactory>().AsSingle().NonLazy();
        Container.Bind<ICuttingPointsGenerator>().To<CuttingPointsGenerator>().AsSingle().NonLazy();
        Container.Bind<ICuttedMeshGenerator>().To<CuttedMeshGenerator>().AsSingle().NonLazy();
    }
}