using Photon.Pun;

using UnityEngine;

using ZL.Unity;

using ZL.Unity.Phys;

using ZL.Unity.Unimo;

public partial class PlayerManager 
{
    private static PlayerStatus originStatus = new PlayerStatus(10, 10, 5, 5, 4, 0.5f, 4);

    private static PlayerStatus playerStatus = originStatus.Clone();
    
    public static PlayerStatus PlayerStatus {  get { return playerStatus; } set { playerStatus = value; } }

    public static PlayerStatus OriginStatus { get { return originStatus; } }

    private void OnDestroy()
    {
        if (selfManager == this)
        {
            selfManager = null;
        }
    }

    private void Awake()
    {
        if (selfManager != null)
        {
            return;
        }

        else
        {
            selfManager = this;

            //Debug.Log(playerSpellType);

            if (playerSpellType != null)
            {
                playerSpellType.SetPlayer(selfManager);
            }
        }

        //selfManager = this;
    }

    private void Start()
    {
        ActionStart();

        MoveStart();

        //currentHealth = maxHP;//기획 의도를 보니 이 코드는 조정이 필요함 한 스테이지에서 까인 체력은 안돌아오는듯?
        SetPlayerStatus(playerStatus);
    }

    private void Update()
    {
        ActionUpdate();

        MoveUpdate();
    }

    public void SetPlayerStatus()
    {
        currentHealth = playerStatus.currentHealth;

        maxHP = playerStatus.maxHealth;

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

    public void ActiveRelic(string type, float value)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            var item = other.GetComponent<Item>();

            item.GetItem(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (PhotonNetwork.IsConnected == false)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Gathering"))
            {
                isItemNear = true;
            }

            else
            {
                isItemNear = false;
            }
        }

        else
        {
            if (photonView.IsMine == true)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Gathering"))
                {
                    isItemNear = true;
                }

                else
                {
                    isItemNear = false;
                }
            }

            else
            {
                return;
            }
        }

        if (isOnHit == true)
        {
            return;
        }

        if (enemyLayerMask.Contains(other.gameObject.layer) == false)
        {
            return;
        }

        if (other.gameObject.TryGetComponent<IDamager>(out var damager) == true)
        {
            var contact = mainCollider.ClosestPoint(other);

            damager.GiveDamage(this, contact);
        }
    }
}