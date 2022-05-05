using UnityEngine;

namespace Characters.Player.Inputs
{
  public class MobileInputService : InputService
  {
    public override Vector2 Axis => SimpleInputAxis();
  }
}