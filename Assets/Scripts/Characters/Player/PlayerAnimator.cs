using Logic;
using UnityEngine;

namespace Characters.Player
{
  public class PlayerAnimator : MonoBehaviour
  {
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;

    private static readonly int WalkHash = Animator.StringToHash("Walk");
    private static readonly int Attack1Hash = Animator.StringToHash("Attack_1");
    private static readonly int Attack2Hash = Animator.StringToHash("Attack_2");
    private static readonly int HitHash = Animator.StringToHash("Hit");

    private AnimatorState State { get; set; }
    public bool IsAttacking => State == AnimatorState.Attack;

    public void SetMove(float speed)
    {
      SetState(speed < 0.1f ? AnimatorState.Idle : AnimatorState.Walking);

      //TODO радиус обнаружения перенести в отдельный компонент
      float radius = speed > 0.5 ? 8f : 2f;
      GetComponent<PlayerDetection>().SetRadiusDetection(radius);
      
      _animator.SetFloat(WalkHash, speed);
    }

    public void PlayAttack()
    {
      SetState(AnimatorState.Attack);
      _animator.SetTrigger(Attack1Hash);
    }

    public void PlayAttackAddition()
    {
      SetState(AnimatorState.Attack);
      _animator.SetTrigger(Attack2Hash);
    }

    public void PlayHit()
    {
      SetState(AnimatorState.Hit);
      _animator.SetTrigger(HitHash);
    }

    public void SetState(AnimatorState newState) => 
      State = newState;

    public void StopAttack()
    {
      SetState(AnimatorState.Idle);
    }
  }
}