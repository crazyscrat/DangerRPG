using Logic.Services;
using Logic.States;
using UnityEngine;
using Zenject;

namespace Logic
{
  public class Game
  {
    public GameStateMachine StateMachine;

    [Inject]
    public Game(GameStateMachine stateMachine)
    {
      StateMachine = stateMachine;
      StateMachine.Enter<BootStrapState>();
      Debug.Log("Game");
    }
  }
}