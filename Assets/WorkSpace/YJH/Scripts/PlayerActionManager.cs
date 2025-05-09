using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    public float itemDetectionRange = 5f;//temp value
    private LayerMask itemLayerMask;
    private GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetItem(/*������ ������ �ֱ�*/)
    {

    }

    public void UseItem()//������ ��� �Ƹ� �ڷ�ƾ�� ���ؼ� �������� ����ҵ� 
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
                    float distanceBetween= Vector3.Distance(transform.position, collider.transform.position);
                    if (distance> distanceBetween)
                    {
                        distance = distanceBetween;
                        targetObject=collider.gameObject;
                    }
                    else if(distance==distanceBetween)
                    {
                        if (targetObject!=null)
                        {
                            if (true)
                            {

                            }
                            else
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
    IEnumerator GatheringCoroutine()// ������ ä���� ����� �ڷ�ƾ
    {
        yield return null;



        if (true/* */)
        {

        }
    }


}
