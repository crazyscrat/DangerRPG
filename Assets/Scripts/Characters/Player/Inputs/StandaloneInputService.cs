using UnityEngine;

namespace Characters.Player.Inputs
{
  public class StandaloneInputService : InputService
  {
    public override Vector2 Axis
    {
      get
      {
        Vector2 axis = SimpleInputAxis();
        if (axis == Vector2.zero)
        {
          axis = UnityAxis();
        }

        return axis;
      }
    }

    public override bool IsAttackButtonDown()
    {
      //return Input.GetMouseButtonDown(0);
      return Input.GetKeyDown(KeyCode.Alpha1);
    }

    public override bool IsInteractionButtonDown()
    {
      return Input.GetKeyDown(KeyCode.E);
    }

    public override bool IsAttackAdditionButtonDown()
    {
      //return Input.GetMouseButtonDown(1);
      return Input.GetKeyDown(KeyCode.Alpha2);
    }

    private Vector2 UnityAxis()
    {
      return new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
    }
  }
}