using System.Threading.Tasks;
using CameraControl;
using Characters;
using Characters.Enemy;
using Characters.Player;
using Data;
using Logic.Factory;
using Logic.Services;
using UI;
using UnityEngine;
using Utils;
using Zenject;

namespace Logic.States
{
  public class LoadLevelState : IState
  {
    public GameStateMachine StateMachine;
    private SceneLoader _sceneLoader;
    private IGameFactory _gameFactory;
    private IProgressService _progress;

    [Inject]
    public LoadLevelState(SceneLoader sceneLoader, IProgressService progress, IGameFactory gameFactory)
    {
      //_stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _progress = progress;
      _gameFactory = gameFactory;
    }

    public void Enter()
    {
      
    }

    public void Enter(string nextScene)
    {
      _sceneLoader.Load(nextScene, OnLoaded);
    }

    public void Exit()
    {
      
    }

    private async void OnLoaded()
    {
      await InitGameWorld();
      InformProgressWatchers();
    }

    //первичная загрузка мира после загрузки сцены
    private async Task InitGameWorld()
    {
      Vector3Data vector3Data = _progress.Progress.WorldData.PositionOnLevel.Position;
      Vector3 position = vector3Data?.ToVector3() ?? new Vector3(0,0,0);
      GameObject player = await _gameFactory.CreateHero(position);
      CameraFollow(player);

      await InitHud(player);

      SpawnEnemies();

      InitLevelTransfer();
    }

    private void InitLevelTransfer()
    {
      LevelTransferTrigger levelTransfer = Object.FindObjectOfType<LevelTransferTrigger>();
      levelTransfer.Construct(StateMachine);
    }

    private void InformProgressWatchers()
    {
      foreach (ISaveProgress progressReader in _gameFactory.ProgressWatchers)
        progressReader.LoadProgress(_progress.Progress);
      
      _gameFactory.UpdateHud();
    }

    //устанавливаем цель камеры
    private void CameraFollow(GameObject player)
    {
      PlayerFollow follow = Camera.main.GetComponent<PlayerFollow>();
      follow.PlayerTransform = player.transform;
      //follow.SetOffset();
    }

    //загрузка hud
    private async Task InitHud(GameObject player)
    {
      await _gameFactory.CreateHud(player);
    }

    //спавн монстров
    private void SpawnEnemies()
    {
      EnemySpawnPoint[] spawnPoints = Object.FindObjectsOfType<EnemySpawnPoint>();

      foreach (EnemySpawnPoint spawnPoint in spawnPoints)
      {
        _gameFactory.CreateEnemy(spawnPoint);
      }
    }
  }
}