using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using ZL.Unity.Unimo;
namespace YJH
{
    public static class MethodCollection
    {
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
        public static void HealAfterStageEndA()
        {

        }
        public static void HealAfterStageEndB()
        {

        }
        public static void HealAfterStageEndC()
        {

        }
        public static void IncreaseRelicChanceA()
        {

        }
        public static void IncreaseRelicChanceB()
        {

        }
        public static void IncreaseRelicChanceC()
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
