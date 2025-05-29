using UnityEngine;
using ZL.Unity.Unimo;

public partial class PlayerManager 
{
    [SerializeField]
    
    private static PlayerStatus playerStatus;
    private static PlayerStatus originStatus;
    public static PlayerStatus PlayerStatus {  get { return playerStatus; } set { playerStatus = value; } }
    public static PlayerStatus OriginStatus { get { return originStatus; } }
    


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
    public void ActiveRelic(string type,float value)//string? enum? 
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.layer);

        //Debug.Log(LayerMask.NameToLayer("Energy"));

        if (other.gameObject.layer == LayerMask.NameToLayer("Energy"))
        {
            //Debug.Log("tri");
            var temp = other.GetComponent<IEnergy>();
            GetEnergy(temp.energy);
            if(temp is IDamageable)
            {
                (temp as IDamageable).TakeDamage(0, Vector3.zero);
            }
            else
            {
                Debug.Log("bug");
            }
            
            //Destroy(other.gameObject);
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