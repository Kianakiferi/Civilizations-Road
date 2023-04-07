using Godot;

namespace CivilizationsRoad.Scripts.Common;

public partial class Configuration
{
    public partial struct Programs
    {
        public struct Graphics
        {
            public static int TargetFrameRate { get; set; } = 60;

            public static DisplayServer.VSyncMode VsyncMode { get; set; } = DisplayServer.VSyncMode.Enabled;
        }
    }
}
