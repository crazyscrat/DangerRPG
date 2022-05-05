using System;
using Characters.Enemy;
using UnityEngine;

namespace Characters.Player
{
  public class PlayerDetection : MonoBehaviour
  {
    [SerializeField] private TriggerObserver _agroZone;
    [SerializeField] private SphereCollider _colliderDetection;

    private void Start()
    {
      _agroZone.TriggerEnter += TriggerEnter;
      _agroZone.TriggerExit += TriggerExit;
    }
    
    private void OnDestroy()
    {
      _agroZone.TriggerEnter -= TriggerEnter;
      _agroZone.TriggerExit -= TriggerExit;
    }

    public void SetRadiusDetection(float radius)
    {
      _colliderDetection.radius = radius;
    }
    
    private void TriggerEnter(Collider collider)
    {
      if (collider.TryGetComponent(out AgentMoveToPlayer component))
      {
        component.Construct(transform);
      }
    }

    private void TriggerExit(Collider collider)
    {
      if (collider.TryGetComponent(out AgentMoveToPlayer component))
      {
        component.Construct(null);
      }
    }
  }
}