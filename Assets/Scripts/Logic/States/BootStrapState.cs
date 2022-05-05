using Characters.Player.Inputs;
using Logic.Factory;
using Logic.Services;
using UnityEngine;
using Utils;
using Zenject;

namespace Logic.States
{
  public class BootStrapState : IState
  {
    public GameStateMachine StateMachine;
    private readonly SceneLoader _sceneLoader;
    
    // public BootStrapState(GameStateMachine gameStateMachine)
    // {
    //   _stateMachine = gameStateMachine;
    // }

    [Inject]
    public BootStrapState(SceneLoader sceneLoader)
    {
      _sceneLoader = sceneLoader;
      //_stateMachine = gameStateMachine;
      Debug.Log("BootStrapState");
      //_stateMachine = gameStateMachine;
      //_services = services;
    
      //RegisterServices();
    }

    private void RegisterServices()
    {
      // _services.RegisterService(InputService());
      // _services.RegisterService(_stateMachine);
      // _services.RegisterService(new ProgressService());
      // _services.RegisterService(new SaveLoadService(_services.GetService<IProgressService>()));
      // _services.RegisterService(new GameFactory());
    }

    //регистрируем сервис управления персонажем
    private IInputService InputService() => 
      Application.isEditor ? (IInputService) new StandaloneInputService() : new MobileInputService();

    public void Enter()
    {
      Debug.Log("BootStrapState Enter");
      _sceneLoader.Load(AssetPath.SceneLevel1, EnterLoadLevel);
    }

    public void Enter(string nextScene)
    {
      
    }

    public void Exit()
    {
      
    }

    private void EnterLoadLevel()
    {
      StateMachine.Enter<LoadProgressState>();
    }
  }
}