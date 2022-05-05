using Characters.Player.Inputs;
using Logic.Services;
using UnityEngine;
using Utils;
using Zenject;

namespace Characters.Player
{
  public class PlayerAttack : MonoBehaviour
  {
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _layerMask;

    private IInputService _inputService;
    private Collider[] _hits = new Collider[3];

    [Header("Test")]
    [SerializeField] private float DamageRadius;
    [SerializeField] private int Damage;
    private Transform _target;

    [Inject]
    void Setup(IInputService inputService)
    {
      _inputService = inputService;
    }
    
    private void Awake()
    {
      //_inputService = AllServices.Container.GetService<IInputService>();
    }

    private void Update()
    {
      if (_inputService.IsAttackButtonDown() && !_animator.IsAttacking)
      {
        //обычная атака
        RotateToTarget();
        _animator.PlayAttack();
      }
      
      if (_inputService.IsAttackAdditionButtonDown() && !_animator.IsAttacking)
      {
        //особая атака
        _animator.PlayAttackAddition();
      }
    }

    private void RotateToTarget()
    {
      if(_target != null) transform.LookAt(_target);
    }

    //событие атаки в анимации
    private void OnAttack()
    {
      PhysicsDebug.DrawDebug(_attackPoint.position, DamageRadius, 1.0f);
      for (int i = 0; i < Hit(); ++i)
      {
        _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(Damage, GetComponent<IExperience>());
      }
    }

    private void OnPlayerAttackEnded()
    {
      _animator.StopAttack();
    }
    
    private int Hit() => 
      Physics.OverlapSphereNonAlloc(_attackPoint.position, DamageRadius, _hits, _layerMask);

    public void SetTarget(Transform target)
    {
      _target = target;
    }
  }
}