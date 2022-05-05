using System;

namespace Data
{
  [Serializable]
  public class PlayerStats
  {
    public int Level;
    public int CurrentExp;
    public int NextLevelExp;

    public void ResetStats()
    {
      CurrentExp = 0;
      NextLevelExp = 100;
      Level = 0;
    }
  }
}