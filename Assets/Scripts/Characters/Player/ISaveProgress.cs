using Data;

namespace Characters.Player
{
  public interface ISaveProgress
  {
    void UpdateProgress(PlayerProgress progress);
    void LoadProgress(PlayerProgress progress);
  }
}