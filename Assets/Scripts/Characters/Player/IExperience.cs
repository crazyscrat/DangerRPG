using System;

namespace Characters.Player
{
  public interface IExperience
  {
    int CurrentExp { get; set; }
    int Level { get; set; }
    int NextLevelExp { get; set; }
    int FreePoints { get; set; }
    
    event Action ExpChachgedEvent;
    event Action LevelChachgedEvent;
    void TakeExp(int exp);
  }
}