using Godot;
using System;

using CivilizationsRoad.Scripts.Managers;

public partial class LableVersion : Label
{
    public override void _EnterTree()
    {
        this.Text = $"V{CivilizationsRoad.Scripts.Configuration.Programs.Version}";
    }
}
