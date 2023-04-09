using Godot;
using System;

public partial class ProgramMain : Control
{
    private Node programMain { get; set; }

    public override void _Ready()
    {
        programMain = GetTree().CurrentScene;

        var exitButton = GetNode<Button>("MarginContainer/Body/ButtonContainer/ButtonExit");
        exitButton.Pressed += OnButtonExitPressed;
    }

    private void OnButtonExitPressed()
    {
        GetTree().Quit();
    }

}
