using Godot;

namespace CivilizationsRoad.Components.Program;

public partial class FPSCounter : Label
{
    public override void _Ready()
    {
        this.Visible = CivilizationsRoad.Scripts.Configuration.Programs.UserInterface.DisplayFPSCounter;
    }

	public override void _Process(double delta)
	{
		var fps = Engine.GetFramesPerSecond().ToString();

		this.Text = fps;
	}
}
