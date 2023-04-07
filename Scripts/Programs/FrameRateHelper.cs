using Godot;

namespace CivilizationsRoad.Scripts.Programs;
// TODO: 起一个新名字
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
