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

        #region ���۽� �ΰ��� ��ȭ �߰� �Լ�
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
        #region ��������� ������ �Լ�
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
        public static void HealAfterStageEndA()//�������� ���Ḧ ��Ÿ���� �̺�Ʈ�� �ִٸ� �ű⿡ �Ҵ��ϱ�
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
        public static void IncreaseRelicChanceA()//���� �Լ� ��������� ��� 
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



        public static void ResurrectA()//�̰� ���� ����?
        {

        }
        public static void ResurrectB()
        {

        }

        public static void IncreaseWorldMapMovingCount()//����� ���� �ϱ�
        {

        }


        
    }
}
