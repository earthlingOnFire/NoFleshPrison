using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace NoFleshPrison;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin {	
  public const string PLUGIN_GUID = "com.earthlingOnFire.NoFleshPrison";
  public const string PLUGIN_NAME = "NoFleshPrison";
  public const string PLUGIN_VERSION = "1.0.0";
  public static ManualLogSource logger;

  private void Awake() {
    gameObject.hideFlags = HideFlags.HideAndDontSave;
    Plugin.logger = Logger;
  }

  private void Start() {
    new Harmony(PLUGIN_GUID).PatchAll();
    Plugin.logger.LogInfo($"Plugin {PLUGIN_GUID} is loaded!");
  }
}

[HarmonyPatch]
public static class Patches {
  [HarmonyPostfix]
  [HarmonyPatch(typeof(EnemyIdentifier), "Start")]
  public static void REMOVEFLESHPRISON(EnemyIdentifier __instance) {
    if (__instance.enemyType == EnemyType.FleshPrison) {
      AssistController.Instance.cheatsEnabled = true;
      __instance.statue.eid = __instance;
      __instance.statue.DeathEnd();
    }
  }
}
