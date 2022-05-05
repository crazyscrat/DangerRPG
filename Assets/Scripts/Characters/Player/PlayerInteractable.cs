using Logic.Services;
using UI;
using UnityEngine;
using Zenject;

namespace Characters.Player
{
  public class PlayerInteractable : MonoBehaviour
  {
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private TriggerObserver _trigger;
    private InteractableItemBase _interactItem;
    private IInputService _inputService;

    public PlayerHud PlayerHud { get; set; }

    [Inject]
    void Setup(IInputService inputService)
    {
      _inputService = inputService;
    }

    private void Start()
    {
      _trigger.TriggerEnter += OnTriggerEnter;
      _trigger.TriggerExit += OnTriggerExit;
    }

    private void OnDestroy()
    {
      _trigger.TriggerEnter -= OnTriggerEnter;
      _trigger.TriggerExit -= OnTriggerExit;
    }

    private void Update()
    {
      if (_inputService.IsInteractionButtonDown())
      {
        InteractWithItem();
      }
    }

    void OnTriggerEnter(Collider other)
    {
      InteractableItemBase item = other.GetComponent<InteractableItemBase>();

      if (item != null)
      {
        if (item.CanInteract(other))
        {
          _interactItem = item;

          //Hud.OpenMessagePanel(mInteractItem);
          PlayerHud.SetInteractableAction(true, _interactItem.InteractText);
        }
      }
    }

    void OnTriggerExit(Collider other)
    {
      InteractableItemBase item = other.GetComponent<InteractableItemBase>();

      if (item != null)
      {
        if (item.CanInteract(other))
        {
          _interactItem = null;
          PlayerHud.SetInteractableAction(false);
        }
      }
    }

    public void InteractWithItem()
    {
      if (_interactItem != null)
      {
        Debug.Log(_interactItem.Name);
        _interactItem.OnInteract();

        if (_interactItem is InventoryItemBase inventoryItem)
        {
          // Inventory.AddItem(inventoryItem);
          inventoryItem.OnPickup();

          // if (inventoryItem.UseItemAfterPickup)
          // {
          //     Inventory.UseItem(inventoryItem);
          // }
          PlayerHud.SetInteractableAction(false);
          _interactItem = null;
        }
        //else
        //{
        //    if (mInteractItem.ContinueInteract())
        //    {
        //        Hud.OpenMessagePanel(mInteractItem);
        //    }
        //    else
        //    {
        //        Hud.CloseMessagePanel();
        //        mInteractItem = null;
        //    }
        //}
      }
      else
      {
        PlayerHud.ShowPanelCharacter(!PlayerHud.PanelCharacterActive);
        Time.timeScale = PlayerHud.PanelCharacterActive ? 0 : 1;
      }
    }
  }
}