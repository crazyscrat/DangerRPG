using System;
using Characters.Player;

namespace Characters
{
  public interface IHealth
  {
    event Action HealthChangedEvent;
    int CurrentHp { get; set; }
    int MaxHp { get; set; }
    void TakeDamage(int damage, IExperience attacking);
  }
}