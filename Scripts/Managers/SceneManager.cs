using System.Collections.Generic;
using Godot;

namespace CivilizationsRoad.Scripts.Managers;

public partial class SceneManager : Node
{
    public Node CurrentScene { get; set; }

    private Stack<Node> sceneStack;

    public override void _Ready()
    {
        CurrentScene = GetTree().CurrentScene;
    }

    public void GotoScene(string path)
    {
        CallDeferred(nameof(GotoSceneDeferred), path);
    }

    public void GotoSceneDeferred(string path)
    {
        // It is now safe to remove the current scene
        

        // Load a new scene.
        var nextScene = GD.Load(path) as PackedScene;

        // Instance the new scene.
        CurrentScene = nextScene.Instance();

        // Add it to the active scene, as child of root.
        GetTree().GetRoot().AddChild(CurrentScene);

        // Optionally, to make it compatible with the SceneTree.change_scene() API.
        GetTree().SetCurrentScene(CurrentScene);
    }


}
