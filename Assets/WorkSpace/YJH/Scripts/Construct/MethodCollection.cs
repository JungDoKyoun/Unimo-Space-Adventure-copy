using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using ZL.Unity.Unimo;
using UnityEngine.SceneManagement;
using JDG;
using GoogleSheetsToUnity;
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
                PlayerManager.PlayerStatus.currentHealth += healAmount;
            }
        }
        
        public static void DelinkHealPlayer()
        {
            SceneManager.sceneLoaded-=HealAfterStageEndAA;
            SceneManager.sceneLoaded-=HealAfterStageEndBB;
            SceneManager.sceneLoaded-=HealAfterStageEndCC;
        }
        public static void HealAfterStageEndA()//스테이지 종료를 나타내는 이벤트가 있다면 거기에 할당하기
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
        public static void IncreaseRelicChanceA()//관련 함수 만들어지면 사용 
        {
            PlayerInventoryManager.RelicRerollableCount += 1;
        }
        public static void IncreaseRelicChanceB()
        {
            PlayerInventoryManager.RelicRerollableCount += 1;
        }
        public static void IncreaseRelicChanceC()
        {
            PlayerInventoryManager.RelicRerollableCount += 1;
        }


        public static void IncreaseRerollChanceA()
        {
            //RelicDropTable.Instance.Import()
        }
        public static void IncreaseRerollChanceB()
        {

        }
        public static void IncreaseRerollChanceC()
        {

        }



        public static void ResurrectA()//이건 어케 하지?
        {

        }
        public static void ResurrectB()
        {

        }

        public static void IncreaseWorldMapMovingCount()//담당자 오면 하기
        {

        }


        
    }
}
