using Godot;

namespace CivilizationsRoad.Scripts.Common.Program;

public static class FrameRateHelper
{
    public static float GetScreenRefreshRate() => DisplayServer.ScreenGetRefreshRate();

    public static void SetVsyncMode(DisplayServer.VSyncMode mode) => DisplayServer.WindowSetVsyncMode(mode);

    public static void SetFrameRate(int rate)
    {
        
    }

    public static void SetPhysicsRate(int rate)
    {
        Engine.MaxPhysicsStepsPerFrame = rate;
    }
}
