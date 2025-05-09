using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerManager : MonoBehaviour
{



    [Header("채집")]
    [SerializeField] float itemDetectionRange = 5f;//temp value
    [SerializeField] float gatheringSpeed = 4f;
    [SerializeField] float gatheringDelay = 0.5f;


    [Header("소리")]
    [SerializeField] AudioClip gatheringAudioClip;
    [SerializeField] AudioSource gatheringAudioSource;

    [Header("탐지할 오브젝트의 레이어")]
    [SerializeField] LayerMask itemLayerMask;

    private GameObject targetObject;
    // Start is called before the first frame update
    
    public void ActionStart()
    {
        gatheringAudioSource.clip = gatheringAudioClip;
    }
    // Update is called once per frame
    
    public void ActionUpdate()
    {

    }
    public void StartDetectItem()
    {
        StartCoroutine(FindItem());
    }

    public void GetItem(/*아이템 변수형 넣기*/)
    {
        if (true)//공격 아이템을 채취했다면
        {
            UseItem();//아이템 변수
        }
        else//재화 아이템을 채취했다면
        {
            GetHoney(1f);
        }
    }

    public void GetHoney(float earnHoney)
    {

    }



    public void UseItem()//아이템 사용 아마 코루틴을 통해서 아이템을 사용할듯 인터페이스를 통해서 작용
    {
        
    }
    





    public void GatheringItem()
    {
        StartCoroutine(GatheringCoroutine());
    }
    IEnumerator FindItem()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(0.01f);
            Collider[] detectedColliders = Physics.OverlapSphere(transform.position, itemDetectionRange, itemLayerMask);
            if(detectedColliders.Length > 0)
            {
                float distance=float.MaxValue;
                foreach(Collider collider in detectedColliders)
                {
                    float distanceBetween= Vector3.Distance(transform.position, collider.transform.position);//감지된 콜라이더와의 거리
                    if (distance> distanceBetween)//1.거리 비교 조건
                    {
                        distance = distanceBetween;
                        targetObject=collider.gameObject;
                    }
                    else if(distance==distanceBetween)
                    {
                        if (targetObject!=null)
                        {
                            if (true)//2. 체력 비교 조건
                            {

                            }
                            else if(true)//3. 등급 비교 조건
                            {

                            }
                        }
                        

                        
                    }
                }



            }
            else
            {
                isGathering = false;
            }

        }
    }
    IEnumerator GatheringCoroutine()// 아이템 채집중 사용할 코로틴
    {
       
        while (targetObject != null)
        {
            yield return new WaitForSeconds(gatheringDelay);
            //타겟 오브젝트의 스크립트를 가져와서 타겟 스크립트에 저장
            //타겟 스크립트의 체력을 gatheringSpeed에 비례하게 감소 -> 정확하게 gatheringSpeed만큼 감소할 필요는 없기 때문
            //만약 타겟 스크립트의 체력이 0보다 아래로 내려가면
            //타겟 오브젝트를 null로 지정하고
            //재화를 증가시키고
            //이동 매니저에 채취중을 활성화 시킨다



        }
        


        
    }


}
