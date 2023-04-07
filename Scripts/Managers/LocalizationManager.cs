namespace CivilizationsRoad.Scripts.Managers;

public static class LocalizationManager
{
    public static string GetTranslatedText(string key) => Godot.TranslationServer.Translate(key);
    
}
