
using System.Collections.Generic;

namespace CivilizationsRoad.Scripts.Common;

public static class Constants
{
    public struct Programs 
    {
        // TODO: 验证 readonly 的字段是否能在 Godot 中访问到
        public const string ProgramMainSceneKey = "Views.Scenes.Program.ProgramMain";
        
        public static class AutoLoad
        {
            public static readonly string ConfigurationNodePath = "/root/Configuration";
            public static readonly string FPSCounterNodePath = "/root/FPSCounter";
        }

        public struct Resources
        {
            public static readonly HashSet<string> ResourcePaths = new()
            {
                "res://Views/Scenes/Program/ProgramMain.tscn",
                "res://Views/Scenes/Game/TestGameScene.tscn"
            };
        }

    }
    

}
