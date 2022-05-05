using System;
using Data;
using UnityEngine;

namespace Characters.Player
{
  public class PlayerHealth : MonoBehaviour, IHealth, ISaveProgress
  {
    [SerializeField] private PlayerAnimator _animator;
    public event Action HealthChangedEvent;

    [SerializeField] private PlayerState _state;
    private IExperience _attacking;

    public int CurrentHp
    {
      get => _state.CurrentHP;
      set
      {
        if (value != _state.CurrentHP)
        {
          _state.CurrentHP = value;
          HealthChangedEvent?.Invoke();
        }
      }
    }

    public int MaxHp
    {
      get => _state.MaxHP;
      set => _state.MaxHP = value;
    }

    public void TakeDamage(int damage, IExperience attacking)
    {
      if(CurrentHp <= 0) return;
      
      CurrentHp -= damage;
      _animator.PlayHit();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      progress.PlayerState.CurrentHP = CurrentHp;
      progress.PlayerState.MaxHP = MaxHp;
    }

    public void LoadProgress(PlayerProgress progress)
    {
      CurrentHp = progress.PlayerState.CurrentHP;
      MaxHp = progress.PlayerState.MaxHP;
    }
  }
}