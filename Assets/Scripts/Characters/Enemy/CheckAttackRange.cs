using System;
using Characters.Player;
using UnityEngine;

namespace Characters.Enemy
{
  public class CheckAttackRange : MonoBehaviour
  {
    [SerializeField] private TriggerObserver _attackTrigger;
    [SerializeField] private Attack _attack;

    private void Start()
    {
      _attackTrigger.TriggerEnter += TriggerEnter;
      _attackTrigger.TriggerExit += TriggerExit;
    }
    
    private void OnDestroy()
    {
      _attackTrigger.TriggerEnter += TriggerEnter;
      _attackTrigger.TriggerExit += TriggerExit;
    }

    private void TriggerEnter(Collider collider)
    {
      SetTargetSelf(collider, transform);
      _attack.Construct(collider.transform);
      _attack.EnableAttack();
    }

    private void TriggerExit(Collider collider)
    {
      SetTargetSelf(collider, null);
      _attack.DisableAttack();
      _attack.Construct(null);
    }

    private void SetTargetSelf(Collider collider, Transform t)
    {
      if(collider.TryGetComponent(out PlayerAttack player))
      {
        player.SetTarget(t);
      }
    }
  }
}