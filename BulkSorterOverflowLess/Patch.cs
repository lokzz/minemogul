using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using TMPro;
using System;
using System.Linq;


[HarmonyPatch(typeof(BulkSorter), "FixedUpdate")]
class OverwriteBulkUpdateCount
{
	static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            // this is so shit but works anyways (and technically has a slight anti-breakage feature)
            if (instruction.opcode == OpCodes.Ldc_I4_4)
            {
                instruction.opcode = OpCodes.Ldc_I4_8;
            }
            yield return instruction;
        }
    }
}
