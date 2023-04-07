using System;
using System.Linq;
using System.Collections.Generic;
using Godot;
using CivilizationsRoad.Scripts.Common;
using CivilizationsRoad.Scripts.Localizations;
using CivilizationsRoad.Scripts.Managers;

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
        Scripts.Programs.FrameRateHelper.SetPhysicsRate(1);
        PrepareResource();
        BeginLoadResource();
    }

    public override void _Ready()
    {
        // TODO: 移动至 Configration 中
        SetFPSCounter();
        PlayMainThemeMusic();
    }

    public override void _Process(double delta)
    {
        try
        {
            // TODO: 可能的循环超时
            foreach (var item in resourcesToLoad)
            {
                if (item.Value == false)
                {
                    if (AssetManager.TryGetResource(item, out Resource resource))
                    {
                        AssetManager.Resources.Add(resource.ResourceName, resource);
                        resourcesToLoad[item.Key] = true;
                        LoadingProgress++;
                    }
                }
            }
            if (LoadingProgress == ResourcesCount)
            {
                LoadComplete();
            }
        }
        catch (Exception e)
        {
            // TODO: 细分 catch
            GD.PrintErr(e.Message);
        }
    }

    private void PrepareResource()
    {
        resourcesToLoad = GetResources();
        ResourcesCount = resourcesToLoad.Count;
    }

    private void BeginLoadResource()
    {
        // TODO: 根据资源的类型，分批次加载
        foreach (var item in resourcesToLoad)
        {
            // TODO: 确定CacheMode
            var error = AssetManager.TryLoadResourceThreaded(item.Key);
            if (error != Error.Ok)
            {
                var message = Godot.TranslationServer.Translate(
                    Strings.Exceptions.Assets.Key_ResourceTryLoadException
                );
                GD.Print($"{message}: {error} => {item.Key}");
            }
        }
    }

    private void LoadComplete()
    {
        if (AssetManager.Resources.ContainsKey(Constants.Programs.ProgramMainSceneName))
        {
            var prograMainScene = (PackedScene)
                AssetManager.Resources[Constants.Programs.ProgramMainSceneName];
            var message = Godot.TranslationServer.Translate(Strings.Programs.Key_ResourceLoadComplete);
            
            GD.Print(message);   
            GetTree().ChangeSceneToPacked(prograMainScene);
            GetTree().CurrentScene.Free();
        }
        else
        {
            var message = LocalizationManager.GetTranslatedText(Strings.Exceptions.Assets.Key_ResourceNotFoundOrLoadedException);
            throw new Scripts.Exceptions.Assets.ResourceNotFoundOrLoadedException(
                $"{message} => {Constants.Programs.ProgramMainSceneName}"
            );
        }

         
    }
    private Dictionary<string, bool> GetResources() =>
        Variables.Program.ResourcePaths.ToDictionary(i => i, i => false);

    private void SetFPSCounter()
    {
        var fpsCounter = GetNode<Control>(Constants.Programs.AutoLoad.FPSCounterNodePath);

        fpsCounter.Visible = Configuration.Programs.UserInterface.DisplayFPSCounter;
    }

    private void PlayMainThemeMusic() { }
}
