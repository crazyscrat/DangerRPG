using System.Collections.Generic;
using System.Threading.Tasks;
using Characters.Enemy;
using Characters.Player;
using Logic.Services;
using UnityEngine;

namespace Logic.Factory
{
  public interface IGameFactory : IService
  {
    Task<GameObject> CreateHero(Vector3 pos);
    Task CreateHud(GameObject player);

    void UpdateHud();
    
    void CreateEnemy(EnemySpawnPoint spawnPoint);
    List<ISaveProgress> ProgressWatchers { get; }
  }
}