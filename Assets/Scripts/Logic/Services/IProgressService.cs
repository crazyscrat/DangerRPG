using Data;

namespace Logic.Services
{
  public interface IProgressService : IService
  {
    PlayerProgress Progress { get; set; }
  }
}