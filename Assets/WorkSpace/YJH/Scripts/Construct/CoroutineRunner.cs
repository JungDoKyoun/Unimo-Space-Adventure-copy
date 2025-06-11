using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
       // DontDestroyOnLoad(gameObject);
    }


    public Coroutine Run(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }
    public Task RunCoroutine(IEnumerator coroutine,MonoBehaviour targetScript)
    {
        var coroTask=new TaskCompletionSource<bool>();
        targetScript.StartCoroutine(Wrap(coroutine,coroTask));
        return coroTask.Task;
    }
    public IEnumerator Wrap(IEnumerator coroutine, TaskCompletionSource<bool> tcs)
    {
        yield return coroutine;
        tcs.SetResult(true);
    }
}
