using JDG;

using System;

using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Player Inventory Manager (Singleton)")]

    public sealed class PlayerInventoryManager : MonoSingleton<PlayerInventoryManager>
    {
        private static readonly HashSet<RelicData> relicDatas = new();

        private static int relicRerollableCountMax = 99;

        private static int relicRerollableCount = 0;

        public static int RelicRerollableCount
        {
            get => relicRerollableCount;

            set => relicRerollableCount = Math.Clamp(value, 0, relicRerollableCountMax);
        }

        public static string RelicRerollableCountText
        {
            get => $"∏Æ∑— ({relicRerollableCount}/{relicRerollableCountMax})";
        }

        public static HashSet<RelicData> RelicDatas
        {
            get => relicDatas;
        }

        private void Start()
        {
            if (GameStateManager.IsClear == true)
            {
                return;
            }

            FixedDebug.Log("¿Œ∫•≈‰∏Æ √ ±‚»≠µ ");

            relicDatas.Clear();

            relicRerollableCountMax = 99;

            RelicRerollableCount = 99;
        }

        public static void AddRelic(RelicData relicData)
        {
            if (relicDatas.Contains(relicData) == true)
            {
                return;
            }

            relicDatas.Add(relicData);

            FixedDebug.Log($"{relicData.name} »πµÊ");
        }

        public static void RemoveRelic(RelicData relicData)
        {
            if (relicDatas.Contains(relicData) == false)
            {
                return;
            }

            relicDatas.Remove(relicData);

            FixedDebug.Log($"{relicData.name} ¿“¿Ω");
        }
    }
}