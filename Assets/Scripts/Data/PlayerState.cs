using System;

namespace Data
{
  [Serializable]
  public class PlayerState
  {
    public int MaxHP;
    public int CurrentHP;

    public int Level;
    public int NextLevelExp;
    public int CurrentExp;
    public int FreePoints;

    public int Power;
    public int PowerBonus;
    public int Dexterity;
    public int DexterityBonus;
    public int Stamina;
    public int StaminaBonus;
    public int Intellect;
    public int IntellectBonus;

    public PlayerState()
    {
      Reset();
    }

    public void Reset()
    {
      MaxHP = 10;
      CurrentHP = MaxHP;

      Level = 1;
      NextLevelExp = 10;
      CurrentExp = 0;
      Power = 5;
      PowerBonus = 0;
      Dexterity = 5;
      DexterityBonus = 0;
      Stamina = 5;
      StaminaBonus = 0;
      Intellect = 5;
      IntellectBonus = 0;
    }
  }
}