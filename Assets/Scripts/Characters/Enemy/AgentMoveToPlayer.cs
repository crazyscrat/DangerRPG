using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
  public class AgentMoveToPlayer : MonoBehaviour
  {
    [SerializeField] private NavMeshAgent _agent;

    private const float MinimalDistance = 1;
    
    private Transform _targetTransform;

    public void Construct(Transform heroTransform) => _targetTransform = heroTransform;

    private void Update()
    {
      if(_targetTransform && IsHeroNotReached() && _agent.enabled)
        _agent.destination = _targetTransform.position;
    }
    
    private bool IsHeroNotReached() => 
      Vector3.Distance(_agent.transform.position, _targetTransform.position) >= MinimalDistance;
  }
}