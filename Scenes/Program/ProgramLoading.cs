using System;
using System.Linq;
using System.Collections.Generic;
using Godot;
using Array = Godot.Collections.Array;
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
    private Array Progress;

    private Dictionary<string, bool> resourcesToLoad;
    public override void _Ready()
    {
        //rootNode = GetTree().Root;
        resourcesToLoad = GetResources();
        ResourcesCount = resourcesToLoad.Count;

        foreach (var item in resourcesToLoad)
        {
            // TODO: 确定CacheMode
            var error = ResourceLoader.LoadThreadedRequest(item.Key);
            if (error is not Error.Ok)
            {
                GD.Print(error);
            }
        }
    }

    public override void _Process(double delta)
    {
        foreach (var item in resourcesToLoad)
        {
            if (item.Value is false)
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
            if (Variables.Program.Resources.ContainsKey(Variables.Program.ProgramMainSceneName))
            {
                var prograMain = (PackedScene)Variables.Program.Resources[Variables.Program.ProgramMainSceneName];

                GetTree().ChangeSceneToPacked(prograMain);
            }
            else
            {
                GD.PrintErr($"{Strings.Program.Error.ResourceNotFoundOrLoaded} => {Variables.Program.ProgramMainSceneName}");
            }

        }
    }

    private Dictionary<string, bool> GetResources() => Variables.Program.ResourcePaths.ToDictionary(i => i, i => false);


}
