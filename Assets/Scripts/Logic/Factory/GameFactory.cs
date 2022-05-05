using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Characters;
using Characters.Enemy;
using Characters.Player;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utils;
using Zenject;

namespace Logic.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly DiContainer _diContainer;
    private PlayerHud _playerHud;

    public List<ISaveProgress> ProgressWatchers { get; } = new List<ISaveProgress>();

    public GameFactory(DiContainer diContainer)
    {
      _diContainer = diContainer;
    }

    public async Task<GameObject> CreateHero(Vector3 pos)
    {
      GameObject go = await CreateObjectAtPosition(AssetPath.PlayerAddressable, pos);
      RegisterProgressWatchers(go);
      return go;
    }

    private GameObject CreateObject(string path)
    {
      GameObject go = _diContainer.InstantiatePrefabResource(path);
      RegisterProgressWatchers(go);
      return go;
    }

    private async Task<GameObject> CreateObjectAtPosition(string path, Vector3 pos)
    {
      GameObject prefab = await Addressables.LoadAssetAsync<GameObject>(path).Task;

      GameObject go = _diContainer.InstantiatePrefab(prefab, pos, Quaternion.identity, null);
      RegisterProgressWatchers(go);

      return go;
    }

    public async Task CreateHud(GameObject player)
    {
      GameObject prefab = await Addressables.LoadAssetAsync<GameObject>(AssetPath.HUDAddressable).Task;

      GameObject hud = _diContainer.InstantiatePrefab(prefab);
      RegisterProgressWatchers(hud);

      _playerHud = hud.GetComponent<PlayerHud>();
      _playerHud.Construct(player.GetComponent<IHealth>(), player.GetComponent<IExperience>());
      player.GetComponent<PlayerInteractable>().PlayerHud = _playerHud;
    }

    public void UpdateHud()
    {
      _playerHud.UpdateHud();
    }

    public async void CreateEnemy(EnemySpawnPoint spawnPoint)
    {
      EnemyType enemyType = spawnPoint.EnemyType;
      Vector3 position = spawnPoint.transform.position;
      string path = "";

      switch (enemyType)
      {
        case EnemyType.Wizard:
          path = AssetPath.WizardAddressablePrefab;
          break;
        case EnemyType.Grunt:
          path = AssetPath.GruntAddressablePrefab;
          break;
        case EnemyType.DogKnight:
          path = AssetPath.DogKnightAddressablePrefab;
          break;
      }

      GameObject prefab = await Addressables.LoadAssetAsync<GameObject>(path).Task;

      GameObject enemy =
        _diContainer.InstantiatePrefab(prefab, position, Quaternion.identity, spawnPoint.transform);
      RegisterProgressWatchers(enemy);
    }

    private void RegisterProgressWatchers(GameObject go)
    {
      foreach (ISaveProgress progress in go.GetComponentsInChildren<ISaveProgress>())
      {
        Register(progress);
      }
    }

    public void Register(ISaveProgress progressReader)
    {
      if (progressReader is ISaveProgress progressWriter)
        ProgressWatchers.Add(progressWriter);
    }
  }
}