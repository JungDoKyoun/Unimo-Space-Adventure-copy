using UnityEngine;

public partial class PlayerManager 
{
    [SerializeField]
    
    private PlayerStatus playerStatus;

    private void Start()
    {
        ActionStart();

        MoveStart();

        currentHealth = maxHP;
    }

    private void Update()
    {
        ActionUpdate();

        MoveUpdate();
    }

    public void SetPlayerStatus()
    {
        currentHealth = playerStatus.currentHealth;

        maxHP = playerStatus.maxHP;

        playerDamage = playerStatus.playerDamage;

        itemDetectionRange = playerStatus.itemDetectionRange;

        gatheringSpeed = playerStatus.gatheringSpeed;

        gatheringDelay = playerStatus.gatheringDelay;

        //최종속도
        moveSpeed = playerStatus.moveSpeed;

        //baseSpeed = playerStatus.baseSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.layer);

        //Debug.Log(LayerMask.NameToLayer("Energy"));

        if (other.gameObject.layer == LayerMask.NameToLayer("Energy"))
        {
            //Debug.Log("tri");

            GetEnergy(3);

            Destroy(other.gameObject);
        }
    }
}