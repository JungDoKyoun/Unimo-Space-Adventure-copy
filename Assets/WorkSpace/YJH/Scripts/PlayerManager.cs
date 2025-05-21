using UnityEngine;

using ZL.Unity.Unimo;

public partial class PlayerManager 
{
    [SerializeField] PlayerStatus playerStatus;
    private void OnEnable()
    {
        MonsterManager.Instance.Target = transform;
    }

    private void OnDisable()
    {
        if (MonsterManager.Instance != null)
        {
            MonsterManager.Instance.Target = null;
        }
    }

    private void Start()
    {
        ActionStart();

        MoveStart();
        StatusStart();
    }

    private void Update()
    {
        ActionUpdate();

        MoveUpdate();
    }
    public void SetPlayerStatus()
    {
        currentHealth=             playerStatus.currentHealth;
        maxHP =                    playerStatus.maxHP ;
        playerDamage =             playerStatus.playerDamage;
        itemDetectionRange =       playerStatus.itemDetectionRange;
        gatheringSpeed =           playerStatus.gatheringSpeed;
        gatheringDelay =           playerStatus.gatheringDelay;
        moveSpeed =                playerStatus.moveSpeed;//최종속도
        baseSpeed =                playerStatus.baseSpeed;
}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        Debug.Log(LayerMask.NameToLayer("Energy"));
        if (other.gameObject.layer == LayerMask.NameToLayer("Energy"))
        {
            Debug.Log("tri");
            GetEnergy(3);

            Destroy(other.gameObject);
        }
    }
}