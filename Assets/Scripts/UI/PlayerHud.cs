using System;
using Characters;
using Characters.Player;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class PlayerHud : MonoBehaviour
  {
    [Header("Health")]
    [SerializeField] private Image _imageHp;
    [SerializeField] private TMP_Text _textHP;
    [Header("Experience/Level")]
    [SerializeField] private Image _imageProgressExp;
    [SerializeField] private TMP_Text _textExpirience;
    [SerializeField] private TMP_Text _textLevel;

    [Header("Message panel")] 
    [SerializeField] private TMP_Text _textMessage;

    [Header("Action button")] 
    [SerializeField] private ToggleSprite _toggleActionButton;

    [Header("Panel Character")] 
    [SerializeField] private GameObject _panelCharacter;
    
    private IHealth _health;
    private IExperience _expirience;
    private PlayerState _playerState;

    public bool PanelCharacterActive => _panelCharacter.activeInHierarchy; 
    
    public void Construct(IHealth health, IExperience experience)
    {
      _health = health;
      _health.HealthChangedEvent += OnHealthChanged;

      _expirience = experience;
      _expirience.ExpChachgedEvent += OnExperienceChanged;
      _expirience.LevelChachgedEvent += SetTextLevel;

      _panelCharacter.GetComponent<PanelCharacter>().Experience = experience;
      
      ShowMessageText(false);
      
      UpdateHud();
    }

    public void UpdateHud()
    {
      OnHealthChanged();
      OnExperienceChanged();
      SetTextLevel();
    }

    private void Start()
    {
      SetInteractableAction(false);
    }

    private void OnHealthChanged()
    {
      SetImageFillHp();
      SetTextHp();
    }
    
    private void OnExperienceChanged()
    {
      SetImageFillExp();
      SetTextExp();
    }

    private void SetImageFillHp()
    {
      _imageHp.fillAmount = (float) _health.CurrentHp / _health.MaxHp;
    }

    private void SetTextHp()
    {
      _textHP.text = $"{_health.CurrentHp}/{_health.MaxHp}";
    }
    
    private void SetImageFillExp()
    {
      _imageProgressExp.fillAmount = (float) _expirience.CurrentExp / _expirience.NextLevelExp;
    }
    
    private void SetTextLevel()
    {
      _textLevel.text = $"{_expirience.Level}";
    }
    
    private void SetTextExp()
    {
      _textExpirience.text = $"{_expirience.CurrentExp}/{_expirience.NextLevelExp}";
    }

    public void SetInteractableAction(bool isInteractable, string textMessage = "")
    {
      
      _toggleActionButton.ToggleSpriteToOne(isInteractable);

      SetMessageText(textMessage);
    }

    private void SetMessageText(string text)
    {
      _textMessage.text = text;

      ShowMessageText(!string.IsNullOrEmpty(text));
    }

    private void ShowMessageText(bool show)
    {
      _textMessage.enabled = show;
    }

    public void ShowPanelCharacter(bool show)
    {
      _panelCharacter.SetActive(show);
    }
  }
}