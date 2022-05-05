﻿using System;

namespace Data
{
  [Serializable]
  public class WorldData
  {
    public PositionOnLevel PositionOnLevel;

    public WorldData(string initLevel)
    {
      PositionOnLevel = new PositionOnLevel(initLevel);
    }
  }
}