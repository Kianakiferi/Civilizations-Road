using Godot;

namespace CivilizationsRoad.Scripts.Configuration;

public partial struct Programs
{
    // TODO: 验证非 [Export] 的元素 Godot 是否能访问到
    [Export]
    public static bool IsDebug { get; set; } = true;
    
    public static string Version { get; set; } = "0.0.1";
}
