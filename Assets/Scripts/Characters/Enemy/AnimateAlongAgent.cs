using Logic;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
  [RequireComponent(typeof(NavMeshAgent))]
  [RequireComponent(typeof(EnemyAnimator))]
  public class AnimateAlongAgent : MonoBehaviour
  {
    private const float MinimalVelocity = 0.1f;
    
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private EnemyAnimator Animator;

    private void Update()
    {
      if(ShouldMove() && !Animator.IsDead)
        Animator.Move(Agent.velocity.magnitude);
      else if(Animator.State == AnimatorState.Walking)
        Animator.StopMoving();
    }

    private bool ShouldMove() => 
      Agent.velocity.magnitude > MinimalVelocity && Agent.remainingDistance > Agent.radius;
  }
  
}