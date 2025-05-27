using UnityEngine;

public partial class PlayerManager 
{
    [SerializeField]
    
    private PlayerStatus playerStatus;

    public PlayerStatus PlayerStatus {  get { return playerStatus; } }
    


    private void Start()
    {
        ActionStart();

        MoveStart();

        currentHealth = maxHP;//기획 의도를 보니 이 코드는 조정이 필요함 한 스테이지에서 까인 체력은 안돌아오는듯?
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
    public void SetPlayerStatus(PlayerStatus status)
    {
        playerStatus = status;
        SetPlayerStatus();
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
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Gathering"))
        {
            isItemNear = true;
        }
        else
        {
            isItemNear = false;
        }
    }


}