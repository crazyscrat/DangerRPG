using System;
using Data;
using UnityEngine;

namespace Characters.Player
{
  public class PlayerExperience : MonoBehaviour, ISaveProgress, IExperience
  {
    public event Action ExpChachgedEvent;
    public event Action LevelChachgedEvent;
    [SerializeField] private PlayerState _state;

    public int CurrentExp
    {
      get => _state.CurrentExp;
      set
      {
        if (value != _state.CurrentExp)
        {
          _state.CurrentExp = value;
          ExpChachgedEvent?.Invoke();
        }
      }
    }
    
    public int Level
    {
      get => _state.Level;
      set
      {
        if (value != _state.Level)
        {
          _state.Level = value;
          LevelChachgedEvent?.Invoke();
        }
      }
    }
    
    public int NextLevelExp
    {
      get => _state.NextLevelExp;
      set => _state.NextLevelExp = value;
    }
    
    public int FreePoints { get => _state.FreePoints; set => _state.FreePoints = value; }
    
    public void TakeExp(int exp)
    {
      if ((CurrentExp + exp) >= NextLevelExp)
      {
        int diff = CurrentExp + exp - NextLevelExp;
        NextLevelExp *= 3;

        CurrentExp = diff;
        Level++;
        FreePoints += 3;

        return;
      }
      
      CurrentExp += exp;
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      progress.PlayerState.CurrentExp = CurrentExp;
      progress.PlayerState.NextLevelExp = NextLevelExp;
      progress.PlayerState.Level = Level;
    }

    public void LoadProgress(PlayerProgress progress)
    {
      CurrentExp = progress.PlayerState.CurrentExp;
      NextLevelExp = progress.PlayerState.NextLevelExp;
      Level = progress.PlayerState.Level;
    }
  }
}