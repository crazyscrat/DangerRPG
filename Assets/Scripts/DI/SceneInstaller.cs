using Logic;
using Zenject;

namespace DI
{
    public class SceneInstaller : MonoInstaller
    {
        public GameBootstrapper GameBootStrapPrefab;
    
        public override void InstallBindings()
        {
            
            //Container.Bind<Game>().AsCached();
            Container.InstantiatePrefabForComponent<GameBootstrapper>(GameBootStrapPrefab);
        }
    }
}