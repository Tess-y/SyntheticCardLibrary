using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnboundLib.GameModes;
using UnityEngine;

namespace SyntheticCardLibrary.Utilities {
    public class SetUpHooks:MonoBehaviour {
        internal static void Regester() {
            List<string> hooks = new List<string>();
            typeof(GameModeHooks).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(field => field.DeclaringType == typeof(string) && (/*IsConst?*/field.IsLiteral && !field.IsInitOnly))
                .ToList().ForEach(f => hooks.Add((string)f.GetValue(null)));
            foreach(Type type in typeof(Main).Assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract)) {
                foreach(string hook in hooks) {
                    var method = type.GetMethod(hook, BindingFlags.NonPublic | BindingFlags.Static);  
                    if (method != null)
                        GameModeManager.AddHook(hook, gm => (IEnumerator)method.Invoke(null,new object[] { gm }));
                }
            }
        }
    }
}
