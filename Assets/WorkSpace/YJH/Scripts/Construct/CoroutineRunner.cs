using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{

    private static CoroutineRunner instance;
    public static CoroutineRunner Instance {  get { return instance; } set { instance = value; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    public Coroutine Run(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

}
