using BepInEx;
using HarmonyLib;
using SyntheticCardLibrary.Utilities;
using System.Collections.Generic;
using System.Reflection;
using UnboundLib;
using UnityEditor.VersionControl;
using UnityEngine;

namespace SyntheticCardLibrary {
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.tabinfo", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("ot.dan.rounds.gamesaver", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class Main:BaseUnityPlugin {
        private const string ModId = "systems.r00t.syntheticcards";
        private const string ModName = "Synthetic Card Library";
        public const string Version = "0.1.0";
        internal static AssetBundle Assets;
        internal static Harmony harmony;
        internal static GameObject test;
        public static Main instance { get; private set; }

        void Awake() {
            harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        void Start() {
            var plugins = (List<BaseUnityPlugin>)typeof(BepInEx.Bootstrap.Chainloader).GetField("_plugins", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            SetUpHooks.Regester();
            if(plugins.Exists(plugin => plugin.Info.Metadata.GUID == "ot.dan.rounds.gamesaver")) {
                UnityEngine.Debug.LogWarning("////////////////////////////");
                for(int _ = 0; _ < 10; _++)
                    UnityEngine.Debug.LogWarning("WARNING GAMESAVER DETECTED, LOADING SAVES WITH SYNTHEDIC CARDS MAY RESOLT IN CARD LOSS");
                UnityEngine.Debug.LogWarning("////////////////////////////");
            }
            test = new GameObject("test gameobject NO-BUILD");
            
        }
    }
}
