using System;
using System.Linq;
using System.Collections.Generic;
using Godot;
using CivilizationsRoad.Scripts.Common;

namespace CivilizationsRoad.Scenes.Program;

public partial class ProgramLoading : Control
{
    [Export]
    public int LoadingProgress { get; private set; }

    [Export]
    public int ResourcesCount { get; private set; }

    //private Node rootNode;
    private ProgressBar progressBar;
    private Dictionary<string, bool> resourcesToLoad;

    public override void _EnterTree()
    {
        resourcesToLoad = GetResources();

        ResourcesCount = resourcesToLoad.Count;

        foreach (var item in resourcesToLoad)
        {
            // TODO: 确定CacheMode
            var error = ResourceLoader.LoadThreadedRequest(item.Key);
            if (error != Error.Ok)
            {
                GD.Print(error);
            }
        }
    }

    public override void _Ready()
    {
        Scripts.Common.Program.FrameRateHelper.SetPhysicsRate(1);
        SetFPSCounter();
    }

    public override void _Process(double delta)
    {
        foreach (var item in resourcesToLoad)
        {
            if (item.Value == false)
            {
                var state = ResourceLoader.LoadThreadedGetStatus(item.Key);

                if (state is ResourceLoader.ThreadLoadStatus.Loaded)
                {
                    var resource = ResourceLoader.LoadThreadedGet(item.Key);
                    Variables.Program.Resources.Add(resource.ResourceName, resource);

                    resourcesToLoad[item.Key] = true;
                    LoadingProgress++;
                }
            }
        }

        if (LoadingProgress == ResourcesCount)
        {
            GD.Print("加载完成");
            if (Variables.Program.Resources.ContainsKey(Constants.Program.ProgramMainSceneName))
            {
                var prograMain = (PackedScene)
                    Variables.Program.Resources[Constants.Program.ProgramMainSceneName];

                GetTree().ChangeSceneToPacked(prograMain);
                GetTree().CurrentScene.Free();
            }
            else
            {
                GD.PrintErr(
                    $"{Scripts.Localizations.Strings.Program.ErrorMessages.ResourceNotFoundOrLoaded} => {Constants.Program.ProgramMainSceneName}"
                );
            }
        }
    }

    private void SetFPSCounter()
    {
        var fpsCounter = GetNode<Control>(Constants.Program.AutoLoad.FPSCounterNodePath);

        fpsCounter.Visible = Configuration.Program.DisplayFPSCounter;
    }

    private Dictionary<string, bool> GetResources() =>
        Variables.Program.ResourcePaths.ToDictionary(i => i, i => false);
}
