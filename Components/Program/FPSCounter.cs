using Godot;

namespace CivilizationsRoad.Components.Program;

public partial class FPSCounter : Label
{
	public override void _Process(double delta)
	{
		var fps = Engine.GetFramesPerSecond().ToString();

		Text = fps;
	}
}
