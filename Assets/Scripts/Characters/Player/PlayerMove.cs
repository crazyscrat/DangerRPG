using System;
using Data;
using Logic.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Zenject;

namespace Characters.Player
{
  public class PlayerMove : MonoBehaviour, ISaveProgress
  {
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speedWalk;
    [SerializeField] private float _speedRun;
    [SerializeField] private PlayerAnimator _animator;

    private IInputService _inputService;
    private Transform _transform;
    private float _movementSpeed;

    [Inject]
    void Setup(IInputService inputService)
    {
      _inputService = inputService;
    }
    
    private void Awake()
    {
      _transform = transform;
      //_inputService = AllServices.Container.GetService<IInputService>();
    }

    private void Start()
    {
      _characterController.Move(Physics.gravity);
    }

    private void Update()
    {
      if(_animator.IsAttacking) return;
      
      Vector3 moveVector = Vector3.zero;

      float magnitude = _inputService.Axis.magnitude;

      if (magnitude > Constants.Epsilon)
      {
        moveVector = _inputService.Axis;
        moveVector.z = moveVector.y;
        moveVector.y = 0;
        if(_inputService.IsHandle())
        {
          _transform.forward = moveVector;
        }
        moveVector.Normalize();
        _movementSpeed = magnitude < 0.5 ? _speedWalk : _speedRun;
      }
      
      _animator.SetMove(magnitude);

      moveVector += Physics.gravity;
      _characterController.Move(_movementSpeed * moveVector * Time.deltaTime);
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      Vector3 position = transform.position;
      
      progress.WorldData.PositionOnLevel = new PositionOnLevel(
        SceneManager.GetActiveScene().name,
        new Vector3Data(position.x, position.y, position.z));
    }

    public void LoadProgress(PlayerProgress progress)
    {
      Vector3Data positionData = progress.WorldData.PositionOnLevel.Position;
      if (positionData != null)
      {
        SetPosition(positionData);
      }
    }

    private void SetPosition(Vector3Data positionData)
    {
      Vector3 position = new Vector3(positionData.X, positionData.Y, positionData.Z);
      _characterController.enabled = false;
      _transform.position = position.AddY(_characterController.height);
      _characterController.enabled = true;
    }
  }
}