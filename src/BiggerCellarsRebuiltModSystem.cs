using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Vintagestory.API.Common;

namespace BiggerCellarsRebuilt
{
    public class BiggerCellarsRebuiltModSystem : ModSystem
    {
        private Harmony harmony;

        public override void Start(ICoreAPI api)
        {
            api.Logger.Warning("[BiggerCellarsRebuilt] Starting transpiler version.");

            harmony = new Harmony(Mod.Info.ModID);
            harmony.PatchAll();

            api.Logger.Warning("[BiggerCellarsRebuilt] Loaded successfully.");
        }

        public override void Dispose()
        {
            harmony?.UnpatchAll(Mod.Info.ModID);
        }
    }

    [HarmonyPatch]
    public static class RoomRegistry_FindRoomForPosition_TranspilerPatch
    {
        public static MethodBase TargetMethod()
        {
            Type roomRegistryType = AccessTools.TypeByName("Vintagestory.GameContent.RoomRegistry");

            if (roomRegistryType == null)
            {
                throw new Exception("[BiggerCellarsRebuilt] Could not find RoomRegistry type.");
            }

            MethodInfo method = AccessTools.Method(roomRegistryType, "FindRoomForPosition");

            if (method == null)
            {
                throw new Exception("[BiggerCellarsRebuilt] Could not find FindRoomForPosition method.");
            }

            return method;
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                // Vanilla max room size: 14
                // New max room size: 24
                if (IsIntConstant(instruction, 14))
                {
                    yield return NewIntConstant(24);
                    continue;
                }

                // Vanilla normal cellar size: 7
                // New normal cellar size: 14
                if (IsIntConstant(instruction, 7))
                {
                    yield return NewIntConstant(14);
                    continue;
                }

                // Vanilla alternate cellar size: 9
                // New alternate cellar size: 18
                if (IsIntConstant(instruction, 9))
                {
                    yield return NewIntConstant(18);
                    continue;
                }

                // Vanilla alternate cellar volume: 150
                // New alternate cellar volume: 600
                if (IsIntConstant(instruction, 150))
                {
                    yield return NewIntConstant(600);
                    continue;
                }

                yield return instruction;
            }
        }

        private static bool IsIntConstant(CodeInstruction instruction, int value)
        {
            if (instruction.opcode == OpCodes.Ldc_I4 && instruction.operand is int intOperand)
            {
                return intOperand == value;
            }

            if (instruction.opcode == OpCodes.Ldc_I4_S && instruction.operand is sbyte sbyteOperand)
            {
                return sbyteOperand == value;
            }

            if (instruction.opcode == OpCodes.Ldc_I4_0) return value == 0;
            if (instruction.opcode == OpCodes.Ldc_I4_1) return value == 1;
            if (instruction.opcode == OpCodes.Ldc_I4_2) return value == 2;
            if (instruction.opcode == OpCodes.Ldc_I4_3) return value == 3;
            if (instruction.opcode == OpCodes.Ldc_I4_4) return value == 4;
            if (instruction.opcode == OpCodes.Ldc_I4_5) return value == 5;
            if (instruction.opcode == OpCodes.Ldc_I4_6) return value == 6;
            if (instruction.opcode == OpCodes.Ldc_I4_7) return value == 7;
            if (instruction.opcode == OpCodes.Ldc_I4_8) return value == 8;

            return false;
        }

        private static CodeInstruction NewIntConstant(int value)
        {
            return new CodeInstruction(OpCodes.Ldc_I4, value);
        }
    }

    [HarmonyPatch]
    public static class RoomRegistry_GetRoomForPosition_Patch
    {
        public static MethodBase TargetMethod()
        {
            Type roomRegistryType = AccessTools.TypeByName("Vintagestory.GameContent.RoomRegistry");

            if (roomRegistryType == null)
            {
                throw new Exception("[BiggerCellarsRebuilt] Could not find RoomRegistry type.");
            }

            MethodInfo method = AccessTools.Method(roomRegistryType, "GetRoomForPosition");

            if (method == null)
            {
                throw new Exception("[BiggerCellarsRebuilt] Could not find GetRoomForPosition method.");
            }

            return method;
        }

        public static void Postfix(ref object __result)
        {
            if (__result == null) return;

            Type roomType = __result.GetType();

            FieldInfo isSmallRoomField = roomType.GetField(
                "IsSmallRoom",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
            );

            if (isSmallRoomField != null && isSmallRoomField.FieldType == typeof(bool))
            {
                isSmallRoomField.SetValue(__result, true);
            }
        }
    }
}