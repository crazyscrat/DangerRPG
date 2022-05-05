using Characters.Player.Inputs;
using Logic;
using Logic.Factory;
using Logic.Services;
using Logic.States;
using UnityEngine;
using Utils;
using Zenject;

namespace DI
{
    public class MainInstaller : MonoInstaller
    {
        public CoroutineRunner CoroutineRunnerPrefab;

        public override void InstallBindings()
        {
            //var coroutineRunner = Container.InstantiatePrefab(CoroutineRunnerPrefab);
            Container.Bind<CoroutineRunner>().FromComponentInNewPrefab(CoroutineRunnerPrefab).AsSingle().NonLazy();
            Container.Bind<SceneLoader>().FromNew().AsSingle().NonLazy();

            Container.Bind<IProgressService>().To<ProgressService>().AsSingle();
            Container.Bind<IInputService>().FromMethod(InputService).AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();

            Container.Bind<GameStateMachine>().FromNew().AsSingle();
            Container.Bind<BootStrapState>().AsCached();
            Container.Bind<LoadProgressState>().AsCached();
            Container.Bind<LoadLevelState>().AsCached();
        }

        private ICoroutineRunner GetICoroutineRunner()
        {
            return Container.Resolve<GameBootstrapper>().GetComponent<ICoroutineRunner>();
        }

        //регистрируем сервис управления персонажем
        private IInputService InputService() => 
            Application.isEditor ? (IInputService) new StandaloneInputService() : new MobileInputService();
    }
}