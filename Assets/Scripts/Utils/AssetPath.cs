using JetBrains.Annotations;

namespace Utils
{
  public static class AssetPath
  {
    //Scenes
    public const string InitScene = "InitScene";
    public const string TestScene = "TestScene";
    [NotNull] public const string SceneLevel1 = "Level_1";
    
    //Characters
    public const string Player = "Player/Player";
    public const string PlayerAddressable = "Player";
    public const string HUD = "UI/HUD";
    public const string HUDAddressable = "HUD";
    
    //Enemies
    public static string WizardPrefab = "Enemies/PolyArtWizardMaskTintMat";
    public static string WizardAddressablePrefab = "Wizard";
    public static string DogKnightAddressablePrefab = "DogKnight";
    public static string GruntAddressablePrefab = "Grunt";
  }
}