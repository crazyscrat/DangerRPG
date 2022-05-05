using Data;

namespace Logic.Services
{
  public interface ISaveLoadService : IService
  {
    void SaveProgress();
    PlayerProgress LoadProgress();
  }
}