using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraControl : MonoBehaviour
{

    [SerializeField] PlayerManager player;
    // Start is called before the first frame update
    
    private void FixedUpdate()
    {
        transform.position = player.transform.position;
        //transform.localScale.x = player.ItemDetectionRange;
        SetAuraScale(PlayerManager.PlayerStatus.itemDetectionRange);
    }

    public void SetAuraScale(float playerDetectionRange)//5f -> 1.216
    {
        transform.localScale = new Vector3((playerDetectionRange / 5) * 1.216f, (playerDetectionRange / 5) * 1.216f, (playerDetectionRange / 5) * 1.216f);
    }



}
