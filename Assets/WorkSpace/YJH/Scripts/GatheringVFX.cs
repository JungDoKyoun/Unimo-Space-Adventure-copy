using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringVFX : MonoBehaviour
{
    public static PlayerManager targetPlayer;
    private void Start()
    {
        targetPlayer = PlayerManager.Instance;
        StartCoroutine(followCoroutine());
    }
    private IEnumerator followCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            transform.position = targetPlayer.transform.position + 0.5f * Vector3.up;
            yield return null;
        }
    }
}
