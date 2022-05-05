using Logic.Services;

namespace Logic.States
{
  public interface IGameStateMachine : IService
  {
    void Enter<TState>() where TState: class, IState;
    void Enter<TState>(string text) where TState: class, IState;
    void Exit();
  }
}