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
        public RelicCard selectedRelicCardScript;
        public RelicDropTable initRelicDropTable;
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
            relicCardScript1.OnSelectAction += SetRelicCard;
            relicCardScript2.OnSelectAction += SetRelicCard;
            relicCardScript3.OnSelectAction += SetRelicCard;
            relicCardScript1.OnDeselectAction += DeSetRelicCard;
            relicCardScript2.OnDeselectAction += DeSetRelicCard;
            relicCardScript3.OnDeselectAction += DeSetRelicCard;
        }

        

        public void SetRelicData()
        {
            
            //RelicData[] relicDatas = new RelicData[3];
            RelicData[] relicDatas = initRelicDropTable.GetRandomRelics(3);
            relicCardScript1.Initialize(relicDatas[0]);
            relicCardScript2.Initialize(relicDatas[1]);
            relicCardScript3.Initialize(relicDatas[2]);
            //Debug.Log("SetRelic");
        }
        public void ActiveRelicCard()
        {
            relicCardObject1.SetActive(true);
            relicCardObject2.SetActive(true);
            relicCardObject3.SetActive(true);
        }

        public void AddRelicByCard()
        {
            
            
        }
        public void SetRelicCard(RelicCard card)
        {
            selectedRelicCardScript= card;
        }
        public void DeSetRelicCard(RelicCard card)
        {
            if(selectedRelicCardScript != null)
            {
                if (selectedRelicCardScript == card)
                {
                    selectedRelicCardScript = null;
                }
            }
        }

    }
}
