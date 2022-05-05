using UnityEngine;

namespace Data
{
  [SerializeField]
  public class PlayerProgress
  {
    public WorldData WorldData;
    public PlayerState PlayerState;
    public PlayerStats PlayerStats;

    public PlayerProgress(string initLevel)
    {
      PlayerState = new PlayerState();
      PlayerStats = new PlayerStats();
      WorldData = new WorldData(initLevel);
    }
  }
}