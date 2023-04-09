using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using CivilizationsRoad.Scripts.Common;
using CivilizationsRoad.Scripts.Localizations;
using CivilizationsRoad.Scripts.Managers;

namespace CivilizationsRoad.Scenes.Program;

public partial class ProgramLoading : Control
{
    [Export]
    public int LoadingProgress { get; private set; }

    //private Node rootNode;
    private ProgressBar progressBar;
    private Timer timer;
    public override void _EnterTree()
    {
        Scripts.Programs.FrameRateHelper.SetPhysicsRate(1);
    }

    public override void _Ready()
    {
        progressBar = GetNode<ProgressBar>("LoadingProgressBar");
        progressBar.MaxValue = AssetManager.Resources.Count;
        progressBar.Value = 0;

        AssetManager.PrepareResource();

        _ = AssetManager.LoadResourcesAsync();

        AddProgressBarUpdateTimer();
        
        // TODO: 移动至 Configration 中
        PlayMainThemeMusic();
    }

    private void UpdateProgressBar()
    {
        if (!AssetManager.IsLoadComplete)
        {
            progressBar.Value = AssetManager.Resources.Count;
        }
        else
        {
            timer.Stop();
            LoadComplete();
        }
    }

    private void LoadComplete()
    {
        if (AssetManager.TryGetPackedScene(Constants.Programs.ProgramMainSceneKey, out var scene))
        {
            CR.PrintLocalized(Scripts.Localizations.Programs.Key_ResourceLoadComplete);
            
            GetTree().ChangeSceneToPacked(scene);
            //GetTree().CurrentScene.Free();
        }
        else
        {
            var message = LocalizationManager.GetTranslatedText(Scripts.Localizations.Exceptions.Assets.Key_ResourceNotFoundOrLoadedException);
            throw new Scripts.Exceptions.Assets.ResourceNotFoundOrLoadedException
            (
                CR.FormatMessage(message, Constants.Programs.ProgramMainSceneKey)
            );
        }
    }
    
    private void AddProgressBarUpdateTimer()
    {
        timer = new()
        {
            WaitTime = 0.05,
            OneShot = false,
            Autostart = true
        };
        timer.Timeout += UpdateProgressBar;
        AddChild(timer);
    }
    
    private void PlayMainThemeMusic() 
    { 

    }
}
