﻿using UnityEngine;

namespace Logic.Services
{
  public interface IInputService : IService
  {
    Vector2 Axis { get; }
    bool IsAttackButtonDown();
    bool IsAttackAdditionButtonDown();
    bool IsInteractionButtonDown();
    bool IsHandle();
  }
}