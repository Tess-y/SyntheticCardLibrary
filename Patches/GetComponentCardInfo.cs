using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System.Linq;
using System;

namespace SyntheticCardLibrary.Patches {
    [Serializable]
    [HarmonyPatch()]
    internal class GetComponentCardInfo {
        public static MethodInfo TargetMethod() {
            return typeof(CardInfo)
                .GetMethod("GetComponent",new Type[] { })
                .MakeGenericMethod(typeof(object));
        }
        public static void Prefix(Component __instance) {
            UnityEngine.Debug.Log(__instance.GetType());
        }
    }
}
