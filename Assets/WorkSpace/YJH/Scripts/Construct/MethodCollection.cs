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

        //�������� ���Ḧ ��Ÿ���� �̺�Ʈ�� �ִٸ� �ű⿡ �Ҵ��ϱ�
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

        //���� �Լ� ��������� ��� 
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
            //Debug.Log("���۽� ���� ���� �ǹ� �۵�");
        }

        public static void ResurrectA()//�̰� ���� ����?
        {

        }

        public static void ResurrectB()
        {

        }

        //����� ���� �ϱ�
        public static void IncreaseWorldMapMovingCount()
        {

        }
    }
}