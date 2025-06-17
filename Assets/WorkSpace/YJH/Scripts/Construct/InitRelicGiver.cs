using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZL.Unity.Unimo;
namespace YJH
{
    
    public class InitRelicGiver : MonoBehaviour
    {
        public static InitRelicGiver Instance { get; private set; }
        public GameObject relicCardObject1;
        public GameObject relicCardObject2;
        public GameObject relicCardObject3;
        public RelicCard relicCardScript1;
        public RelicCard relicCardScript2;
        public RelicCard relicCardScript3;
        private void Awake()
        {
            
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                return;
            }
        }
        public void SetRelicData()
        {
            RelicData[] relicDatas = RelicDropTable.Instance.GetRandomRelics(3);
            relicCardScript1.Initialize(relicDatas[0]);
            relicCardScript2.Initialize(relicDatas[1]);
            relicCardScript3.Initialize(relicDatas[2]);
        }
        public void ActiveRelicCard()
        {
            relicCardObject1.SetActive(true);
            relicCardObject2.SetActive(true);
            relicCardObject3.SetActive(true);
        }


        


    }
}
