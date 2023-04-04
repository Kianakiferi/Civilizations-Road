using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilizationsRoad.Scripts.Common;

public partial class Configuration : Node
{
    public static class Program
    {
        [Export]
        public static bool IsDebug = true;
        [Export]
        public static bool DisplayFPSCounter { get; set; } = IsDebug;
    }
}
