using System;
using Logic.States;
using UnityEngine;
using Zenject;

namespace Logic
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    //[Inject] private Game _game;

    public GameStateMachine StateMachine;

    [Inject]
    void Initialize(GameStateMachine stateMachine)
    {
      StateMachine = stateMachine;
      Debug.Log("GameBootstrapper");
    }
    
    private void Awake()
    {
      //_game = new Game();
      //_game.StateMachine.Enter<BootStrapState>();
      StateMachine.Enter<BootStrapState>();

      //DontDestroyOnLoad(this);
    }
  }
}