using System.Collections.Generic;
using Godot;

namespace CivilizationsRoad.Scripts.Managers;

public static class AssetManager
{
    public static Dictionary<string, Resource> Resources = new();

    //, string typeHint = "", bool useSubThreads = false, ResourceLoader.CacheMode cacheMode = ResourceLoader.CacheMode.Reuse
    public static Error TryLoadResourceThreaded(string path) => ResourceLoader.LoadThreadedRequest(path);

    public static bool TryGetResource(KeyValuePair<string, bool> item, out Resource resource)
    {
        var state = ResourceLoader.LoadThreadedGetStatus(item.Key);

        if (state is ResourceLoader.ThreadLoadStatus.Loaded)
        {
            resource = ResourceLoader.LoadThreadedGet(item.Key);

            return true;
        }

        resource = null;
        return false;
    }
}
