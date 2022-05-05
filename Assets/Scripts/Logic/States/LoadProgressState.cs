using Data;
using Logic.Services;
using UnityEngine;
using Utils;
using Zenject;

namespace Logic.States
{
  public class LoadProgressState : IState
  {
    public GameStateMachine StateMachine;
    private IProgressService _progressService;
    private ISaveLoadService _saveLoadProgress;

    [Inject]
    public LoadProgressState(IProgressService progressService, ISaveLoadService saveLoadProgress)
    {
      _progressService = progressService;
      _saveLoadProgress = saveLoadProgress;
      Debug.Log("LoadProgressState");
    }

    public void Enter()
    {
      LoadOrNewProgress();
      
      StateMachine.Enter<LoadLevelState>(_progressService.Progress.WorldData.PositionOnLevel.LevelName);
    }

    public void Enter(string nextScene) { }

    public void Exit() { }

    private void LoadOrNewProgress()
    {
      _progressService.Progress = _saveLoadProgress.LoadProgress() ?? new PlayerProgress(AssetPath.SceneLevel1);
    }
  }
}