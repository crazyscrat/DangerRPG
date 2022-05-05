using Data;
using UnityEngine;

namespace Utils
{
  public static class Extention
  {
    public static Vector3 AddY(this Vector3 vector, float y)
    {
      vector.y = y;
      return vector;
    }

    public static Vector3 ToVector3(this Vector3Data data)
    {
      return new Vector3(data.X, data.Y, data.Z);
    }
  }
}