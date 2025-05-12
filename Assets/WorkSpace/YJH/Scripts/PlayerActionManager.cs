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

    private bool isGatheringCoroutineWork = false;

    public delegate void OnTargetSet();
    public event OnTargetSet OnTargetObjectSet;

    
    public void ActionStart()
    {
        gatheringAudioSource.clip = gatheringAudioClip;
        StartDetectItem();
        OnTargetObjectSet += GatheringItem;
    }
    // Update is called once per frame
    
    public void ActionUpdate()
    {
        
        
    }
    public void StartDetectItem()
    {
        
        StartCoroutine(FindItem());
    }

    public void GetItem(IGatheringObject temp)
    {
        temp.UseItem();
    }

    

    public void GatheringItem()
    {
        Debug.Log("gathering2");
        if (isGatheringCoroutineWork == false)
        {
            isGatheringCoroutineWork = true;
            StartCoroutine(GatheringCoroutine());
        }
        else
        {
            return;
        }
    }
    IEnumerator FindItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (targetObject == null)
            {
                //Debug.Log("null");
                isGathering = false;
            }
            else
            {
               // Debug.Log(Vector3.Distance(transform.position, targetObject.transform.position));
                if (Vector3.Distance(transform.position, targetObject.transform.position) > itemDetectionRange)
                {

                    isGathering = false;
                    targetObject = null;
                }
            }

            while (isGathering == false)
            {
                //Debug.Log("enterwhile");
                yield return new WaitForSeconds(0.1f);
                Collider[] detectedColliders = Physics.OverlapSphere(transform.position, itemDetectionRange, itemLayerMask);



                if (detectedColliders.Length > 0)
                {
                    //Debug.Log("detected more than 1");
                    //Debug.Log(detectedColliders[0].gameObject.name);
                    float distance = float.MaxValue;
                    foreach (Collider collider in detectedColliders)
                    {

                        float distanceBetween = Vector3.Distance(transform.position, collider.transform.position);//감지된 콜라이더와의 거리
                        if (distance > distanceBetween)//1.거리 비교 조건
                        {
                            distance = distanceBetween;
                            targetObject = collider.gameObject;
                            //Debug.Log("detected");
                            //Debug.Log(targetObject.name);

                        }
                        else if (distance == distanceBetween)
                        {
                            if (targetObject != null)
                            {
                                var targetScript = targetObject.GetComponent<IGatheringObject>();
                                var colliderScript = collider.GetComponent<IGatheringObject>();


                                if (targetScript.NowHP > colliderScript.NowHP)//2. 체력 비교 조건
                                {
                                    targetObject = collider.gameObject;

                                }
                                else if (targetScript.NowHP == colliderScript.NowHP)//3. 등급 비교 조건
                                {
                                    if (targetScript.MaxHP < colliderScript.MaxHP)
                                    {
                                        targetObject = collider.gameObject;

                                    }

                                }
                            }



                        }
                    }


                    isGathering = true;
                    OnTargetObjectSet?.Invoke();
                }
                else
                {
                    Debug.Log("no target");
                    isGathering = false;
                }

            }
        }
    }
    IEnumerator GatheringCoroutine()// 아이템 채집중 사용할 코로틴
    {
        IGatheringObject targetScript = null;
        if (targetObject != null)
        {
             targetScript= targetObject.GetComponent<IGatheringObject>();
        }
        while (true)
        {
            Debug.Log("gathering");
            yield return new WaitForSeconds(gatheringDelay);
            targetScript.NowHP -= gatheringSpeed;
            if(targetScript.NowHP < 0)
            {
                targetScript.OnGatheringEnd();
                targetObject = null;
                isGathering= false;
                isGatheringCoroutineWork = false;
                yield break;
            }
            //타겟 오브젝트의 스크립트를 가져와서 타겟 스크립트에 저장 s
            //타겟 스크립트의 체력을 gatheringSpeed에 비례하게 감소 -> 정확하게 gatheringSpeed만큼 감소할 필요는 없기 때문s
            //만약 타겟 스크립트의 체력이 0보다 아래로 내려가면s
            //타겟 오브젝트를 null로 지정하고s
            //재화를 증가시키고 ->use에 
            //이동 매니저에 채취중을 활성화 시킨다s



        }
        


        
    }


}
