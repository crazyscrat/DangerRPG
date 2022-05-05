using System;
using Characters.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class PanelCharacter : MonoBehaviour
  {
    [SerializeField] Button _buttonClose;

    [Header("Level Name")]
    [SerializeField] private TMP_Text _textNickName;
    [SerializeField] private TMP_Text _textLevel;

    [Header("Panel Stats")] 
    [SerializeField] private TMP_Text _textPoints;

    public IExperience Experience { get; set; }

    private void Start()
    {
      _buttonClose.onClick.AddListener(ClosePanel);
    }

    private void OnEnable()
    {
      _textLevel.text = $"Level: {Experience.Level}";
      _textPoints.text = $"Points: {Experience.FreePoints}";
    }

    public void ClosePanel()
    {
      Time.timeScale = 1;
      gameObject.SetActive(false);
    }
  }
}