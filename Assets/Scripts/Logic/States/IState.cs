namespace Logic.States
{
  public interface IState
  {
    void Enter();
    void Enter(string nextScene);
    void Exit();
  }
}