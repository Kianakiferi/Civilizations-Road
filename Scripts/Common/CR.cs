using Godot;

namespace CivilizationsRoad.Scripts.Common;

public static class CR
{
    public static string FormatMessage(string text1, string text2) => $"{text1} => {text2}"; 
               
     public static string FormatMessage(string text1, string text2, string text3) => $"{text1}: {text2} => {text3}"; 
                
    public static void PrintLocalized(string key)
    {
        var message = Godot.TranslationServer.Translate(key).ToString();
        GD.Print(message); 
    }

    public static void PrintLocalized(params string[] texts)
    {
        var message = Godot.TranslationServer.Translate(texts[0]);
        switch (texts.Length)
        {
            case 1:
            {
                GD.Print(message);
                break;
            }
            case 2:
            {
                GD.Print($"{message} => {texts[1]}"); 
                break;
            }
            case 3:
            {
                GD.Print($"{message}: {texts[1]} => {texts[2]}"); 
                break;
            }
            default:
            {
                GD.Print(texts); 
                break;
            }
        }
    }

    public static string PathToKey(string fullPath)
    {
        var path = fullPath.Substring(6); // Views/Scenes/Program/ProgramMain.tscn

        var dot = GetExtensionNameIndex(path);

        var builder = new System.Text.StringBuilder();
        for (int i = 0; i < dot; i++)
        {
            var current = path[i];
            if (current == '/')
            {
                current = '.';
            }
            builder.Append(current);
        }

        return builder.ToString();
    }

    private static int GetExtensionNameIndex(string path)
    {
        var dot = 0;
        for (int i = path.Length - 1; i >= 0; i--)
        {
            var current = path[i];
            if (current == '.')
            {
                dot = i;
            }
        }
        return dot;
    }
}
