using System.Collections.Generic;
using System.Reflection.Emit;

using HarmonyLib;
using UnityEngine;


[HarmonyPatch(typeof(BulkSorter), "FixedUpdate")]
class OverwriteBulkUpdateCount
{
    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);

        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode == OpCodes.Brtrue || codes[i].opcode == OpCodes.Brtrue_S)
            {
                if (codes[i - 1].OperandIs(AccessTools.Method(typeof(List<OrePiece>), "Contains")))
                {
                    codes[i].opcode = (codes[i].opcode == OpCodes.Brtrue) ? OpCodes.Brfalse : OpCodes.Brfalse_S;
                }
            }

            if (codes[i].OperandIs(AccessTools.Method(typeof(BulkSorter), "DumpAllStuckOre")))
            {
                for (int j = i; j < codes.Count; j++)
                {
                    if (codes[j].opcode == OpCodes.Ret)
                    {
                        codes[j].opcode = OpCodes.Nop;
                        break;
                    }
                }
            }
        }

        return codes;
    }
}