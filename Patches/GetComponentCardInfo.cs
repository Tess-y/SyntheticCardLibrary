using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Label = System.Reflection.Emit.Label;

namespace SyntheticCardLibrary.Patches {
    [Serializable]
    [HarmonyPatch(typeof(GameObject), MethodType.Constructor, new Type[] { typeof(string)})]
    internal class GetComponentCardInfo {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il) {

            var Code = instructions.ToList();
            Label done = il.DefineLabel();
            List<CodeInstruction> newCode = new List<CodeInstruction>() {
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Ldstr, "NO-BUILD"),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(String),"op_Equality")),
                new CodeInstruction(OpCodes.Brfalse_S, done),
        };

            List<CodeInstruction> tranpledCode = new List<CodeInstruction>();
            foreach (var instruction in instructions) {
                tranpledCode.Add(instruction);
                if(instruction.opcode == OpCodes.Nop) {
                    tranpledCode.Add(new CodeInstruction(OpCodes.Ret));
                    var nop = new CodeInstruction(OpCodes.Nop);
                    List<Label> list = (List<Label>)typeof(CodeInstruction).GetField("labels", BindingFlags.Public | BindingFlags.Instance).GetValue(nop);
                    list.Add(done);
                    tranpledCode.Add(instruction);
                }
            }
            return tranpledCode;
        }

        /*
        public static MethodInfo TargetMethod() {
            return typeof(CardInfo)
                .GetMethod("GetComponent",new Type[] { })
                .MakeGenericMethod(typeof(object));
        }
        public static void Prefix(Component __instance) {
            UnityEngine.Debug.Log(__instance.GetType());
        }*/
    }
}
