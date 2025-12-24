using BepInEx;
using UnityEngine;
using HarmonyLib;

internal static class PluginInfo
{
	public const string Author = "lokzz";
	public const string Name = "BulkSorterOverflowLess";
	public const string GUID = "com." + Author + "." + Name;
	public const string Version = "0.1.0";
}

[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
public class Plugin : BaseUnityPlugin
{
	private Harmony _harmony;

	void Awake()
	{
		_harmony = new Harmony(PluginInfo.GUID);
		_harmony.PatchAll();  // Applies all [HarmonyPatch] annotations in your assembly
		Logger.LogInfo($"{PluginInfo.Name} loaded. (v{PluginInfo.Version})");
	}

	private void OnDestroy()
	{
		_harmony.UnpatchSelf();
	}
}