using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
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

    IEnumerator GatheringCoroutine()// ������ ä���� ����� �ڷ�ƾ
    {
        yield return null;



        if (true/* */)
        {

        }
    }


}
