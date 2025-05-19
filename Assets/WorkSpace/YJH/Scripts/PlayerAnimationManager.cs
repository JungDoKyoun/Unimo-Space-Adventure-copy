using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] Animator playerBodyAnimator;
    [SerializeField] Animator playerCartAnimator;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] float delayTime;
    // Start is called before the first frame update
    void Start()
    {
        playerManager.OnPlayerDead += PlayDeadAnimation;
    }

     public void PlayDeadAnimation()
    {
        Debug.Log("deadanim");
        playerBodyAnimator.SetTrigger("blink");
        StartCoroutine(PlayDisappear());
    }
    private void OnDestroy()
    {
        playerManager.OnPlayerDead-=PlayDeadAnimation;
    }
    IEnumerator PlayDisappear()
    {
        Debug.Log("gone");
        yield return new WaitForSeconds(delayTime);
        playerBodyAnimator.SetTrigger("disappear");
        playerCartAnimator.SetTrigger("disappear");
        yield break;
    }
}
