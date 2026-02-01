using BepInEx;
using GorillaServerStats.Behaviours;
using UnityEngine;

namespace GorillaServerStats
{
    [BepInPlugin(Constants.GUID, Constants.PluginName, Constants.Version)]

    public class Plugin : BaseUnityPlugin
    {
        void Awake() => DontDestroyOnLoad(gameObject);
        void Start() => new GameObject(Constants.PluginName, typeof(GStats), typeof(ServerStatsManager));
    }
}