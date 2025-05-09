using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] float itemDetectionRange = 5f;//temp value
    [SerializeField] float gatheringSpeed = 4f;
    [SerializeField] AudioClip gatheringAudioClip;
    [SerializeField] AudioSource gatheringAudioSource;
    
    private LayerMask itemLayerMask;
    private GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {
        gatheringAudioSource.clip = gatheringAudioClip;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetItem(/*아이템 변수형 넣기*/)
    {

    }

    public void UseItem()//아이템 사용 아마 코루틴을 통해서 아이템을 사용할듯 
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

            }

        }
    }
    IEnumerator GatheringCoroutine()// 아이템 채집중 사용할 코로틴
    {
        yield return null;



        if (true/* */)
        {

        }
    }


}
