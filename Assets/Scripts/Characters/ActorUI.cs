using System;
using Characters.Player;
using UI;
using UnityEngine;

namespace Characters
{
  public class ActorUI : MonoBehaviour
  {
    [SerializeField] private EnemyHpBar _hpBar;
    private IHealth _health;

    void Construct(IHealth health)
    {
      _health = health;
      _health.HealthChangedEvent += OnHealthChanged;
      UpdateHpBar();
    }

    private void Start()
    {
      IHealth health = GetComponent<IHealth>();
      if(health != null) Construct(health);
    }

    private void OnDestroy() => _health.HealthChangedEvent -= OnHealthChanged;

    private void OnHealthChanged() => UpdateHpBar();

    private void UpdateHpBar()
    {
      _hpBar.SetValue(_health.CurrentHp, _health.MaxHp);
      if(_health.CurrentHp <=0)
      {
        OnDestroy();
        Destroy(_hpBar.gameObject);
      }
    }
  }
}