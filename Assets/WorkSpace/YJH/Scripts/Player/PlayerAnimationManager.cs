using System.Collections;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] Animator playerBodyAnimator;
    [SerializeField] Animator playerCartAnimator;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] float delayTime;

    private void Start()
    {
        PlayerManager.OnPlayerDead += PlayDeadAnimation;
    }

    public void PlayDeadAnimation()
    {
        Debug.Log("deadanim");
        playerBodyAnimator.SetTrigger("blink");
        StartCoroutine(PlayDisappear());
    }

    private void OnDestroy()
    {
        PlayerManager.OnPlayerDead -= PlayDeadAnimation;
    }

    private IEnumerator PlayDisappear()
    {
        Debug.Log("gone");
        yield return new WaitForSeconds(delayTime);
        playerBodyAnimator.SetTrigger("disappear");
        playerCartAnimator.SetTrigger("disappear");
        yield break;
    }
}