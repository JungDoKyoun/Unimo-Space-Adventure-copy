using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCoroutineLuncher : MonoBehaviour
{
    public static EffectCoroutineLuncher _instance;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static EffectCoroutineLuncher Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<EffectCoroutineLuncher>();
            }
            return _instance;
        }
    }

    public void RunCoroutine(IEnumerator coroutine, Action onComplete = null)
    {
        StartCoroutine(WrappedCoroutine(coroutine, onComplete));
    }

    private IEnumerator WrappedCoroutine(IEnumerator coroutine, Action onComplete)
    {
        yield return coroutine;

        onComplete?.Invoke();
    }
}
