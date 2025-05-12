using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerManager : MonoBehaviour
{



    [Header("ä��")]
    [SerializeField] float itemDetectionRange = 5f;//temp value
    [SerializeField] float gatheringSpeed = 4f;
    [SerializeField] float gatheringDelay = 0.5f;


    [Header("�Ҹ�")]
    [SerializeField] AudioClip gatheringAudioClip;
    [SerializeField] AudioSource gatheringAudioSource;

    [Header("Ž���� ������Ʈ�� ���̾�")]
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

                        float distanceBetween = Vector3.Distance(transform.position, collider.transform.position);//������ �ݶ��̴����� �Ÿ�
                        if (distance > distanceBetween)//1.�Ÿ� �� ����
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


                                if (targetScript.NowHP > colliderScript.NowHP)//2. ü�� �� ����
                                {
                                    targetObject = collider.gameObject;

                                }
                                else if (targetScript.NowHP == colliderScript.NowHP)//3. ��� �� ����
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
    IEnumerator GatheringCoroutine()// ������ ä���� ����� �ڷ�ƾ
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
            //Ÿ�� ������Ʈ�� ��ũ��Ʈ�� �����ͼ� Ÿ�� ��ũ��Ʈ�� ���� s
            //Ÿ�� ��ũ��Ʈ�� ü���� gatheringSpeed�� ����ϰ� ���� -> ��Ȯ�ϰ� gatheringSpeed��ŭ ������ �ʿ�� ���� ����s
            //���� Ÿ�� ��ũ��Ʈ�� ü���� 0���� �Ʒ��� ��������s
            //Ÿ�� ������Ʈ�� null�� �����ϰ�s
            //��ȭ�� ������Ű�� ->use�� 
            //�̵� �Ŵ����� ä������ Ȱ��ȭ ��Ų��s



        }
        


        
    }


}
