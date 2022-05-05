using Logic.Services;
using SimpleInputNamespace;
using UnityEngine;

namespace Characters.Player.Inputs
{
  public abstract class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    private const string Attack = "Fire";
    private const string AttackAdd = "FireAdd";
    private const string Interact = "Action";
    
    public virtual Vector2 Axis { get; }
    public virtual bool IsAttackButtonDown()
    {
      return SimpleInput.GetButtonDown(Attack);
    }

    public virtual bool IsAttackAdditionButtonDown()
    {
      return SimpleInput.GetButtonDown(AttackAdd);
    }

    protected static Vector2 SimpleInputAxis()
    {
      return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }

    public virtual bool IsInteractionButtonDown()
    {
      return SimpleInput.GetButtonDown(Interact);
    }

    public virtual bool IsHandle()
    {
      return Joystick.joystickHeld;
    }
  }
}