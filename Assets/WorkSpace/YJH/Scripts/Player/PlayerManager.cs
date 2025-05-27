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

        currentHealth = maxHP;//��ȹ �ǵ��� ���� �� �ڵ�� ������ �ʿ��� �� ������������ ���� ü���� �ȵ��ƿ��µ�?
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

        //�����ӵ�
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