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
    }
    // Update is called once per frame
    
    public void ActionUpdate()
    {

    }
    public void StartDetectItem()
    {
        StartCoroutine(FindItem());
    }

    public void GetItem(/*������ ������ �ֱ�*/)
    {
        if (true)//���� �������� ä���ߴٸ�
        {
            UseItem();//������ ����
        }
        else//��ȭ �������� ä���ߴٸ�
        {
            GetHoney(1f);
        }
    }

    public void GetHoney(float earnHoney)
    {

    }



    public void UseItem()//������ ��� �Ƹ� �ڷ�ƾ�� ���ؼ� �������� ����ҵ� �������̽��� ���ؼ� �ۿ�
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
                    float distanceBetween= Vector3.Distance(transform.position, collider.transform.position);//������ �ݶ��̴����� �Ÿ�
                    if (distance> distanceBetween)//1.�Ÿ� �� ����
                    {
                        distance = distanceBetween;
                        targetObject=collider.gameObject;
                    }
                    else if(distance==distanceBetween)
                    {
                        if (targetObject!=null)
                        {
                            if (true)//2. ü�� �� ����
                            {

                            }
                            else if(true)//3. ��� �� ����
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
    IEnumerator GatheringCoroutine()// ������ ä���� ����� �ڷ�ƾ
    {
       
        while (targetObject != null)
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
