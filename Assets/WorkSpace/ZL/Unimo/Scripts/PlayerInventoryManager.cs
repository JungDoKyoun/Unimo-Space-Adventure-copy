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
            get => $"리롤 ({relicRerollableCount}/{relicRerollableCountMax})";
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

            Debug.Log("(테스트) 인벤토리 초기화");

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

            Debug.Log($"(테스트) {relicData.name} 획득");
        }

        public static void RemoveRelic(RelicData relicData)
        {
            if (relicDatas.Contains(relicData) == false)
            {
                return;
            }

            relicDatas.Remove(relicData);

            Debug.Log($"(테스트) {relicData.name} 삭제");
        }
    }
}