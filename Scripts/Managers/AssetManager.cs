using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CivilizationsRoad.Scripts.Common;
using CivilizationsRoad.Scripts.Localizations;
using Godot;

namespace CivilizationsRoad.Scripts.Managers;

public static class AssetManager
{
    // TODO: 自动扫描文件夹中的资源并加载
    public static Dictionary<string, Resource> Resources = new();

    public static HashSet<string> ResourcePathsToLoad = new();

    // public static int ResourceCount 
    // { 
    //     get => Resources.Count;
    // }
    // public static int RestCount
    // {
    //     get => ResourcePathsToLoad.Count;
    // } 

    public static bool IsLoadComplete 
    { 
        get => ResourcePathsToLoad.Count == 0;
    }

    public static void LoadResourcs()
    {
        PrepareResource();
    }

    public static void PrepareResource()
    {
        ResourcePathsToLoad = Variables.Program.ResourcePaths;
        // 读取外部资源文件

        // TODO: 根据资源的类型，分批次加载
        foreach (var item in ResourcePathsToLoad)
        {
            // TODO: 确定CacheMode
            var error = TryLoadResourceThreaded(item);
            if (error != Error.Ok)
            {
                CR.PrintLocalized
                (
                    Localizations.Exceptions.Assets.Key_ResourceLoadFailException,
                    error.ToString(),
                    CR.PathToKey(item)
                );
            }
        }
    }
   // 生成一个C#函数，扫描项目文件夹"Views"，将所有后缀名为".tscn"的文件的路径添加到一个HashSet<string> 中
    
    public static async Task LoadResourcesAsync()
    {   
        try
        {
            while (!IsLoadComplete)
            {
                foreach (var path in ResourcePathsToLoad)
                {
                    if (AssetManager.TryGetResource(path, out Resource resource))
                    {
                        AssetManager.Resources.Add(CR.PathToKey(path), resource);
                        ResourcePathsToLoad.Remove(path);
                    }
                }
                await Task.Delay(15);
            }
        }
        catch (Exception e)
        {
            // TODO: 细分 catch
            GD.PrintErr(e.Message);
        }
        // TODO: 可能的循环超时
        
    }

    public static Error TryLoadResourceThreaded(string path) => ResourceLoader.LoadThreadedRequest(path);//, string typeHint = "", bool useSubThreads = false, ResourceLoader.CacheMode cacheMode = ResourceLoader.CacheMode.Reuse

    public static bool TryGetResource(string path, out Resource resource)
    {
        var state = ResourceLoader.LoadThreadedGetStatus(path);

        switch (state)
        {
            case ResourceLoader.ThreadLoadStatus.Loaded:
            {
                resource = ResourceLoader.LoadThreadedGet(path);
                //resource.ResourceName = "ProgramMain";
                return true;
            }
            case ResourceLoader.ThreadLoadStatus.Failed:
            case ResourceLoader.ThreadLoadStatus.InvalidResource:
            {
                resource = null;
                
                var message = LocalizationManager.GetTranslatedText(Localizations.Exceptions.Assets.Key_ResourceLoadFailException);
                throw new Exceptions.Assets.ResourceLoadFailException(CR.FormatMessage(message, path));
            }
            default:
            {
                resource = null;
                return false;
            }
        }
    }

    public static bool TryGetPackedScene(string sceneKey, out PackedScene scene)
    {
        if (AssetManager.Resources.ContainsKey(sceneKey))
        {
            scene = AssetManager.Resources[sceneKey] as PackedScene;
            return true;
        }

        scene = null;
        return false;
    }

  
}
