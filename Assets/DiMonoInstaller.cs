using UnityEngine;
using Zenject;

public class DiMonoInstaller : MonoInstaller<DiMonoInstaller>
{
   public override void InstallBindings()
   {
      DiInstaller.Install(Container);
   }
}