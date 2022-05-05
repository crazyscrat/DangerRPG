using System;
using Characters.Player;
using UnityEngine;

namespace Characters.Enemy
{
  [RequireComponent(typeof(EnemyAnimator))]
  public class Attack : MonoBehaviour
  {
    [SerializeField] private EnemyAnimator _animator;
    [Header("Attack settings")]
    [SerializeField] private float _attackCooldown = 3f;
    [SerializeField] private int _damage = 10;
    
    private bool _attackIsActive;
    private float _remainCooldown;
    private bool _isAttacking;
    private Transform _target;

    public void Construct(Transform target) => _target = target;

    private void Update()
    {
      UpdateCooldown();
      
      if (CanAttack() && _target)
      {
        StartAttack();
      }
    }

    private void StartAttack()
    {
      _isAttacking = true;
      transform.LookAt(_target);
      _animator.PlayAttack();
    }

    private void UpdateCooldown()
    {
      if (!CooldownIsUp()) _remainCooldown -= Time.deltaTime;
    }

    private bool CooldownIsUp() => _remainCooldown <= 0f;

    private bool CanAttack()
    {
      return _attackIsActive && !_isAttacking && CooldownIsUp() && !_animator.IsDead;
    }

    public void EnableAttack() => _attackIsActive = true;

    public void DisableAttack() => _attackIsActive = false;
    
    private void OnAttack()
    {
      // Debug.Log($"{name} - OnAttack");
      //if (Hit(out Collider hit))
      {
        //hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
        IExperience attacking = null;
        TryGetComponent<IExperience>(out attacking); 
        
        _target.GetComponent<IHealth>().TakeDamage(_damage, attacking);
      }
    }
    
    // private bool Hit(out Collider hit)
    // {
    //   var hitAmount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);
    //
    //   hit = _hits.FirstOrDefault();
    //   
    //   return hitAmount > 0;
    // }
    
    private void OnAttackEnded()
    {
      // Debug.Log($"{name} - OnAttackEnded");
      _remainCooldown = _attackCooldown;
      _isAttacking = false;
    }
  }
}