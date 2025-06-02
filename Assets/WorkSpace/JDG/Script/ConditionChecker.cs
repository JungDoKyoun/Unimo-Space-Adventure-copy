using System.Collections.Generic;

namespace JDG
{
    public static class ConditionChecker
    {
        private static int GetPlayerResource(ResourcesType resourcesType)
        {
            if (resourcesType == ResourcesType.IngameCurrency)
                return FirebaseDataBaseMgr.IngameCurrency;

            else if (resourcesType == ResourcesType.MetaCurrency)
                return FirebaseDataBaseMgr.MetaCurrency;

            else if (resourcesType == ResourcesType.Blueprint)
                return FirebaseDataBaseMgr.Blueprint;

            else
                return 0;
        }

        public static bool IsEnoughPlayerResource(int resourceCost, ResourcesType resourcesType)
        {
            int playerResource = GetPlayerResource(resourcesType);

            if (playerResource >= resourceCost)
                return true;

            return false;
        }

        public static bool IsPlayerHasBuilding(List<string> buildings)
        {
            if (buildings == null || buildings.Count == 0)
                return true;

            List<string> playerBuildings = new List<string>(); //new List<string>();에 플레이어 건물 목록 가져오기

            foreach(string building in buildings)
            {
                if (!playerBuildings.Contains(building))
                    return false;
            }

            return true;
        }

        public static bool IsPlayerHasRelic(List<string> relics)
        {
            if (relics == null || relics.Count == 0)
                return true;

            List<string> playerRelic = new List<string>(); //플레이어 유물 목록 가져와서 비교

            foreach(string relic in relics)
            {
                if (!playerRelic.Contains(relic))
                    return false;
            }

            return true;
        }
    }
}
