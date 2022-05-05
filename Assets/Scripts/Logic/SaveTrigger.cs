using Logic.Services;
using UnityEngine;
using Zenject;

namespace Logic
{
  public class SaveTrigger : InteractableItemBase
  {
    private ISaveLoadService _saveLoadService;

    [Inject]
    void Init(ISaveLoadService saveLoadService)
    {
      _saveLoadService = saveLoadService;
    }

    public override void OnInteract()
    {
      _saveLoadService.SaveProgress();
      Debug.Log("Progress saved!");
    }
  }
}