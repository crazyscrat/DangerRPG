using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class EnemyHpBar : MonoBehaviour
  {
    [SerializeField] private Image _imageHp;

    public void SetValue(int currentHp, int maxHp) => 
      _imageHp.fillAmount =(float)currentHp/maxHp;
  }
}