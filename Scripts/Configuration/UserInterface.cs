namespace CivilizationsRoad.Scripts.Common;

public partial class Configuration
{
    public partial struct Programs
    {
        public struct  UserInterface
        {
            public static bool DisplayFPSCounter { get; set; } = IsDebug;

        }
    }
}
