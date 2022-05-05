using System;
using Characters.Player;
using UnityEngine;

namespace Characters.Enemy
{
  public class EnemyHealth : MonoBehaviour, IHealth
  {
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private int _currentHP;
    [SerializeField] private int _maxHP;

    public event Action HealthChangedEvent;
    public IExperience Attacking { get; set; }


    public int CurrentHp
    {
      get => _currentHP;
      set => _currentHP = value;
    }

    public int MaxHp
    {
      get => _maxHP;
      set => _maxHP = value;
    }

    public void TakeDamage(int damage, IExperience attacking)
    {
      if(_animator.IsDead) return;

      Attacking = attacking;
      CurrentHp -= damage;

      if (CurrentHp < 0)
        CurrentHp = 0;

      //анимация урона
      _animator.PlayHit();

      HealthChangedEvent?.Invoke();
    }
  }
}