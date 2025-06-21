using JDG;

using System.Collections;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

using ZL.Unity.Unimo;

namespace YJH
{
    public static class MethodCollection
    {
        public static string sheetName;

        #region 시작시 인게임 재화 추가 함수

        public static void IncreaseIngameCurrencyA()
        {
            IEnumerator method = FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(50);

            CoroutineRunner.Instance.Run(method);

            //Debug.Log("work!");
        }

        public static void IncreaseIngameCurrencyB()
        {
            IEnumerator method = FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(100);

            CoroutineRunner.Instance.Run(method);
        }

        public static void IncreaseIngameCurrencyC()
        {
            IEnumerator method = FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(150);

            CoroutineRunner.Instance.Run(method);
        }

        public static void IncreaseIngameCurrencyD()
        {
            IEnumerator method = FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(200);

            CoroutineRunner.Instance.Run(method);
        }

        public static void IncreaseIngameCurrencyE()
        {
            IEnumerator method = FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(250);

            CoroutineRunner.Instance.Run(method);
        }

        #endregion 

        #region 게임종료시 힐관련 함수

        private static void HealPlayer(float healAmount)
        {
            if (GameStateManager.IsClear == true)
            {
                PlayerManager.Instance.CurrentHealth += healAmount;
                PlayerManager.gainDemage -= healAmount;
            }
        }
        
        public static void DelinkHealPlayer()
        {
            SceneManager.sceneLoaded-=HealAfterStageEndAA;

            SceneManager.sceneLoaded-=HealAfterStageEndBB;

            SceneManager.sceneLoaded-=HealAfterStageEndCC;
        }

        //스테이지 종료를 나타내는 이벤트가 있다면 거기에 할당하기
        public static void HealAfterStageEndA()
        {
            SceneManager.sceneLoaded += HealAfterStageEndAA;
        }

        private static void HealAfterStageEndAA(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Station")
            {
                HealPlayer(1);
            }
        }

        public static void HealAfterStageEndB()
        {
            SceneManager.sceneLoaded += HealAfterStageEndBB;
        }

        private static void HealAfterStageEndBB(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Station")
            {
                HealPlayer(2);
            }
        }

        public static void HealAfterStageEndC()
        {
            SceneManager.sceneLoaded += HealAfterStageEndCC;
        }

        private static void HealAfterStageEndCC(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Station")
            {
                HealPlayer(3);
            }
        }

        #endregion

        //관련 함수 만들어지면 사용 
        public static void IncreaseRelicChanceA()
        {

        }

        public static void IncreaseRelicChanceB()
        {

        }
        public static void IncreaseRelicChanceC()
        {

        }

        public static void IncreaseRerollChanceA()
        {
            PlayerInventoryManager.RelicRerollableCount += 1;
        }

        public static void IncreaseRerollChanceB()
        {
            PlayerInventoryManager.RelicRerollableCount += 1;
        }

        public static void IncreaseRerollChanceC()
        {
            PlayerInventoryManager.RelicRerollableCount += 1;
        }

        public static void GiveStartRelicA()
        {
            ConstructManager.Instance.isGiveStartRellic=true;
            //Debug.Log("시작시 유물 지급 건물 작동");
        }

        public static void ResurrectA()//이건 어케 하지?
        {

        }

        public static void ResurrectB()
        {

        }

        //담당자 오면 하기
        public static void IncreaseWorldMapMovingCount()
        {

        }
    }
}