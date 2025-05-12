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
    
    public void ActionStart()
    {
        gatheringAudioSource.clip = gatheringAudioClip;
        StartDetectItem();
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
        StartCoroutine(GatheringCoroutine());
    }
    IEnumerator FindItem()
    {
        while (true)
        {
            //if (isGathering==true)
            //{
            //    continue;
            //}
            yield return new WaitForSeconds(0.1f);
            Collider[] detectedColliders = Physics.OverlapSphere(transform.position, itemDetectionRange, itemLayerMask);



            if (detectedColliders.Length > 0)
            {
                float distance = float.MaxValue;
                foreach (Collider collider in detectedColliders)
                {

                    float distanceBetween = Vector3.Distance(transform.position, collider.transform.position);//������ �ݶ��̴����� �Ÿ�
                    if (distance > distanceBetween)//1.�Ÿ� �� ����
                    {
                        distance = distanceBetween;
                        targetObject = collider.gameObject;
                        Debug.Log("detected");
                        Debug.Log(targetObject.name);

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

            }
            else
            {
                isGathering = false;
            }

        }
    }
    IEnumerator GatheringCoroutine()// ������ ä���� ����� �ڷ�ƾ
    {
        if (targetObject != null)
        {
            var targetScript= targetObject.GetComponent<IGatheringObject>();
        }
        while (true)
        {
            yield return new WaitForSeconds(gatheringDelay);
            
            //Ÿ�� ������Ʈ�� ��ũ��Ʈ�� �����ͼ� Ÿ�� ��ũ��Ʈ�� ����
            //Ÿ�� ��ũ��Ʈ�� ü���� gatheringSpeed�� ����ϰ� ���� -> ��Ȯ�ϰ� gatheringSpeed��ŭ ������ �ʿ�� ���� ����
            //���� Ÿ�� ��ũ��Ʈ�� ü���� 0���� �Ʒ��� ��������
            //Ÿ�� ������Ʈ�� null�� �����ϰ�
            //��ȭ�� ������Ű��
            //�̵� �Ŵ����� ä������ Ȱ��ȭ ��Ų��



        }
        


        
    }


}
