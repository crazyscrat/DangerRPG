using Characters.Player;
using Data;
using Logic.Factory;
using UnityEngine;
using Utils;
using Zenject;

namespace Logic.Services
{
  public class SaveLoadService : ISaveLoadService
  {
    
    private IProgressService _progressService;
    private readonly IGameFactory _gameFactory;


    [Inject]
    public SaveLoadService(IProgressService progressService, IGameFactory gameFactory)
    {
      _progressService = progressService;
      _gameFactory = gameFactory;
    }

    public void SaveProgress()
    {
      foreach (ISaveProgress progress in _gameFactory.ProgressWatchers)
      {
        progress.UpdateProgress(_progressService.Progress);
      }
      
      string json = JsonUtility.ToJson(_progressService.Progress);
      PlayerPrefs.SetString(Constants.KeyProgress, json);
      PlayerPrefs.Save();
    }

    public PlayerProgress LoadProgress()
    {
      PlayerProgress progress = null;
      
      string s = PlayerPrefs.GetString(Constants.KeyProgress);
      if(!string.IsNullOrEmpty(s))
        progress = JsonUtility.FromJson<PlayerProgress>(s);
      
      return progress;
    }
  }
}