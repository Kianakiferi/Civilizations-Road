using Godot;
using System.Collections.Generic;

namespace CivilizationsRoad.Scripts.Common;

public static class Variables
{
    public static class Program
    {
        public static HashSet<string> ResourcePaths = new()
        {
            "res://Views/Scenes/Program/ProgramMain.tscn",
            "res://Views/Scenes/Game/TestGameScene.tscn"
        };
    }

   
}
