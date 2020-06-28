using UnityEngine;
using Zenject;

public class DiInstaller : Installer<DiInstaller>
{
   public override void InstallBindings()
   {
      Container.Bind<SceneHandlerService>().To<SceneHandlerServiceImpl>().AsSingle().NonLazy();
      Container.Bind<GameSessionFirst>().AsSingle().NonLazy();
      Container.Bind<RealTimeClient>().AsSingle().NonLazy();
   }
}