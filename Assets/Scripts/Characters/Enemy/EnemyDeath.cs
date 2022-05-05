using System;
using System.Collections;
using Characters.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
  [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
  public class EnemyDeath : MonoBehaviour
  {
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private float _destroyTime;

    public event Action Happened;
    
    private void Start()
    {
      _health.HealthChangedEvent += OnHealthChanged;
    }

    private void OnDestroy()
    {
      _health.HealthChangedEvent -= OnHealthChanged;
    }

    private void OnHealthChanged()
    {
      if (_health.CurrentHp <= 0)
        Die();
      
    }

    private void Die()
    {
      if (_health.Attacking != null)
      {
        //опыт убившему
        _health.Attacking.TakeExp(_health.MaxHp);
      }
      
      _health.HealthChangedEvent -= OnHealthChanged;
      _animator.PlayDeath();
      
      //Destroy(gameObject, _destroyTime);
      //StartCoroutine(DestroyAnimationScale());
      StartCoroutine(DestroyAnimationUnderLand());
      
      Happened?.Invoke();
    }

    private IEnumerator DestroyAnimationScale()
    {
      yield return new WaitForSeconds(_destroyTime);

      int steps = 20;
      float timeScale = .3f / steps;
      
      var scale = transform.localScale;
      var scaleStep = scale / steps;
      
      for (int i = 0; i < steps; i++)
      {
        yield return new WaitForSeconds(timeScale);
        scale -= scaleStep;
        transform.localScale = scale;
      }
      
      Destroy(gameObject);
    }
    
    private IEnumerator DestroyAnimationUnderLand()
    {
      GetComponent<NavMeshAgent>().enabled = false;
      yield return new WaitForSeconds(_destroyTime);

      int steps = 20;
      float timeScale = 1f / steps;
      
      var position = transform.localPosition;
      var posStep = Vector3.up / 30f;

      for (int i = 0; i < steps; i++)
      {
        yield return new WaitForSeconds(timeScale);
        position -= posStep;
        transform.localPosition = position;
      }
      
      Destroy(gameObject);
    }
  }
}