using Logic;
using UnityEngine;

namespace Characters.Enemy
{
  public class EnemyAnimator : MonoBehaviour
  {
    [SerializeField] private Animator _animator;
    public AnimatorState State { get; private set; }
    
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DieHash = Animator.StringToHash("Die");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int MovingHash = Animator.StringToHash("Moving");
    
    public bool IsDead;
    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    public void PlayHit()
    {
      if(IsDead) return;
      if(State==AnimatorState.Hit || State==AnimatorState.Died) return;
      
      SetState(AnimatorState.Hit);
      _animator.SetTrigger(HitHash);
    }
    
    public void PlayDeath()
    {
      IsDead = true;
      SetState(AnimatorState.Died);
      _animator.SetBool(DieHash, true);
    }
    
    public void Move(float speed)
    {
      if(IsDead) return;
      SetState(AnimatorState.Walking);
      _animator.SetBool(MovingHash, true);
      _animator.SetFloat(SpeedHash, speed);
    }

    public void StopMoving()
    {
      SetState(AnimatorState.Idle);
      _animator.SetBool(MovingHash, false);
    }

    public void PlayAttack()
    {
      if(IsDead) return;
      SetState(AnimatorState.Attack);
      _animator.SetTrigger(AttackHash);
    }

    public void SetState(AnimatorState newState) => 
      State = newState;

    private void OnHitEnded()
    {
      Debug.Log("OnHitEnded");
      SetState(AnimatorState.Idle);
    }
  }
}