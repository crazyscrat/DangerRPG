using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.States
{
  public class GameStateMachine : IGameStateMachine
  {
    private IState _activeState;
    private Dictionary<Type, IState> _states;

    public GameStateMachine(BootStrapState bootStrapState, LoadProgressState loadProgressState, LoadLevelState loadLevelState)
    {
      Debug.Log("GameStateMachine");
      _states = new Dictionary<Type, IState>
      {
        [typeof(BootStrapState)] = bootStrapState,
        
        [typeof(LoadLevelState)] = loadLevelState,
        
        [typeof(LoadProgressState)] = loadProgressState,
        
        [typeof(GameLoopState)] = new GameLoopState(this)
      };

      bootStrapState.StateMachine = this;
      loadProgressState.StateMachine = this;
      loadLevelState.StateMachine = this;
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Exit(){}

    private TState ChangeState<TState>() where TState : class, IState
    {
      _activeState?.Exit();

      TState state = GetState<TState>();
      _activeState = state;
      return state;
    }

    private TState GetState<TState>() where TState : class, IState => 
      _states[typeof(TState)] as TState;

    public void Enter<TState>(string nextScene) where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter(nextScene);
    }
  }
}