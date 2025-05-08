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

    IEnumerator GatheringCoroutine()// 아이템 채집중 사용할 코로틴
    {
        yield return null;



        if (true/* */)
        {

        }
    }


}
