using System;
using Logic.States;
using UnityEngine;

namespace Logic
{
  public class LevelTransferTrigger : MonoBehaviour
  {
    [SerializeField] private string TransferTo;
    
    private IGameStateMachine _stateMachine;
    private bool _triggered = false;
    
    private void OnTriggerEnter(Collider other)
    {
      if(_triggered) return;
      
      if(other.CompareTag("Player"))
      {
        _triggered = true;
        _stateMachine.Enter<LoadLevelState>(TransferTo);
      }
    }

    public void Construct(GameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }
  }
}