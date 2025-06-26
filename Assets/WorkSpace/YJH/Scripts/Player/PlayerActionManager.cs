using Photon.Pun;

using System;

using System.Collections;

using TMPro;

using UnityEngine;

using UnityEngine.UI;

using ZL.Unity.Unimo;

public partial class PlayerManager : IEnergizer
{
    [SerializeField]

    private GameObject gatheringAuraPlane;

    [SerializeField]

    private GameObject gatheringEffect;

    [Header("ä�� �Ҹ�")]

    [SerializeField]

    private AudioClip gatheringAudioClip;

    [SerializeField]

    private AudioSource gatheringAudioSource;

    [Header("Ž���� ������Ʈ�� ���̾�")]

    [SerializeField]

    private LayerMask itemLayerMask;

    [Header("Ž���� ���� ���̾�")]

    [SerializeField]

    private LayerMask enemyLayerMask;

    private static GameObject attackPrefab;

    public GameObject tempAttackPrefab;

    private static IAttackType playerAttackType;

    private static ISpellType playerSpellType = new Dash();

    private int playerOwnEnergy = 0;

    public int Energy
    {
        get => playerOwnEnergy;

        set => playerOwnEnergy = value;
    }

    private GameObject targetObject;

    private GameObject targetEnemyObject;

    private bool isGatheringCoroutineWork = false;

    public delegate void OnTargetSet();

    public event OnTargetSet OnTargetObjectSet;

    private Vector3 firePos;

    private static PlayerManager selfManager;

    public static PlayerManager SelfManager
    {
        get => selfManager;
    }

    [SerializeField]

    Image progressBarCircle;

    [SerializeField]

    TMP_Text progressBarText;

    [SerializeField]

    private TMP_Text skillRejectText;

    private bool isSkillRejectActive = false;

    private bool isItemNear = false;

    [SerializeField]

    private SphereCollider detectCollider;

    public static event Action<float> OnEnergyChanged = null;

    private Coroutine gatheringCoroutine;

    // ��Ƽ������ �����丵�Ѱ� ���߿� �� ��ü�ϱ�
    public void ActionStart()
    {
        if (attackPrefab == null)
        {
            SetAttackType(tempAttackPrefab);
        }

        else
        {
            SetAttackType(attackPrefab);
        }

        if (playerSpellType != null)
        {
            playerSpellType.InitSpell();
        }

        else
        {
            ISpellType temp = new Dash();

            SetSpellType(temp);

            playerSpellType.InitSpell();
        }
    }

    public void ActionUpdate()
    {
        if (playerSpellType != null)
        {
            playerSpellType.UpdateTime();

            switch (playerSpellType)
            {
                case IStackSpell:

                    if (progressBarCircle != null && progressBarText != null)
                    {
                        if ((playerSpellType as IStackSpell).NowStack == (playerSpellType as IStackSpell).MaxStack)
                        {
                            progressBarCircle.fillAmount = 1;
                        }

                        else
                        {
                            progressBarCircle.fillAmount = (playerSpellType as IStackSpell).Timer / (playerSpellType as IStackSpell).ChargeTime;
                        }

                        progressBarText.text = (playerSpellType as IStackSpell).NowStack.ToString();
                    }

                    break;

                case ICoolTimeSpell:

                    // ���߿� ��Ÿ�� ��ų �ʿ��ϸ� �����丵

                    //progressBarCircle.fillAmount = (playerSpellType as ICoolTimeSpell).Timer / (playerSpellType as IStackSpell).ChargeTime;

                    //progressBarText.text = (playerSpellType as ICoolTimeSpell).NowStack.ToString();

                    break;

                default:

                    break;
            }
        }

        FindItemUpdate();
    }

    public static void SetAttackType(GameObject attackType)
    {
        attackPrefab = attackType;

        playerAttackType = attackPrefab.GetComponent<IAttackType>();

        playerAttackType.Damage = playerStatus.playerDamage;
    }

    public static void SetSpellType(ISpellType spellType)
    {
        playerSpellType = spellType;

        playerSpellType.SetPlayer(selfManager);
    }

    public void GetItem(IGatheringObject temp)
    {
        temp.UseItem();
    }

    //��Ƽ������ ������ �ֳ�? -> ����
    public void GetEnergy(int value)
    {
        playerOwnEnergy += value;

        if (playerOwnEnergy >= playerAttackType.EnergyCost)
        {
            FindEnemy();

            Vector3 tempDirection;

            if (targetEnemyObject != null)
            {
                tempDirection = targetEnemyObject.transform.position - transform.position;

                firePos = transform.position + tempDirection.normalized * 1.5f;
            }

            else
            {
                tempDirection = transform.forward;

                firePos = transform.position + tempDirection.normalized * 1.5f;
            }

            Vector3 direction = tempDirection.normalized;

            Vector3 right = Vector3.Cross(Vector3.up, direction).normalized;

            float spacing = 1.5f;

            int fireCount = playerOwnEnergy / playerAttackType.EnergyCost;

            for (int i = 0; i < fireCount; i++)
            {
                int offsetIndex = 0;

                // ¦���� ��
                if (fireCount % 2 == 0)
                {
                    int step = (i / 2) + 1;

                    // �� �� ��
                    int sign = (i % 2 == 0) ? -1 : 1;

                    offsetIndex = step * sign;
                }

                else
                {
                    if (i == 0)
                    {
                        // �߾�
                        offsetIndex = 0;
                    }

                    else
                    {
                        int step = (i + 1) / 2;

                        int sign = (i % 2 == 1) ? -1 : 1;

                        offsetIndex = step * sign;
                    }
                }

                Vector3 spawnPos = firePos + right * offsetIndex * spacing;

                if (PhotonNetwork.IsConnected)
                {
                    photonView.RPC("PlayerAttack", RpcTarget.All, spawnPos);
                }

                else
                {
                    PlayerAttack(spawnPos);
                }
            }

            playerOwnEnergy %= playerAttackType.EnergyCost;
        }

        OnEnergyChanged?.Invoke(playerOwnEnergy);
    }

    // �ѹ��� 2�� �̻� ���� �� ���η� �÷��� �߻��ϴ� ������
    public void PlayerAttack()
    {
        playerOwnEnergy -= playerAttackType.EnergyCost;

        var bullet = Instantiate(attackPrefab, firePos, Quaternion.identity);

        if (targetEnemyObject != null)
        {
            bullet.GetComponent<IAttackType>().Shoot(targetEnemyObject.transform.position - firePos);
        }

        else
        {
            bullet.GetComponent<IAttackType>().Shoot(firePos - transform.position);
        }
    }

    [PunRPC]

    public void PlayerAttack(Vector3 firePosition)
    {
        playerOwnEnergy -= playerAttackType.EnergyCost;

        GameObject bullet;

        if (PhotonNetwork.IsConnected == false)
        {
            bullet = Instantiate(attackPrefab, firePosition, Quaternion.identity);
        }

        else
        {
            bullet = PhotonNetwork.Instantiate(attackPrefab.name, firePosition, Quaternion.identity);
        }

        if (targetEnemyObject != null)
        {
            bullet.GetComponent<IAttackType>().Shoot(targetEnemyObject.transform.position - firePos);
        }

        else
        {
            bullet.GetComponent<IAttackType>().Shoot(firePos - transform.position);
        }
    }

    public void GatheringItem()
    {
        if (isGatheringCoroutineWork == false)
        {
            isGatheringCoroutineWork = true;

            gatheringCoroutine = StartCoroutine(GatheringCoroutine());
        }

        else
        {
            return;
        }
    }

    public void FindEnemy()
    {
        Collider[] targetEnemies = Physics.OverlapSphere(transform.position, 100f, enemyLayerMask);

        float distance = float.MaxValue;

        if (targetEnemies.Length > 0)
        {
            foreach (Collider collider in targetEnemies)
            {
                // ������ �ݶ��̴����� �Ÿ�
                float distanceBetween = Vector3.Distance(transform.position, collider.transform.position);

                // 1.�Ÿ� �� ����
                if (distance > distanceBetween)
                {
                    distance = distanceBetween;

                    targetEnemyObject = collider.gameObject;
                }
            }
        }

        else
        {
            targetEnemyObject = null;
        }
    }

    [PunRPC]

    public void ActiveGatheringBeam()
    {
        gatheringEffect.SetActive(true);
    }

    [PunRPC]

    public void DeactiveGatheringBeam()
    {
        gatheringEffect.SetActive(false);
    }

    private void FindItemUpdate()
    {
        if (targetObject == null)
        {
            isGathering = false;
        }

        else
        {
            if (Vector3.Distance(transform.position, targetObject.transform.position) > playerStatus.itemDetectionRange + float.Epsilon)
            {
                isGathering = false;

                targetObject = null;
            }
        }

        if (isGathering == false && playerSpellType.ReturnState() == false)
        {
            Collider[] detectedColliders = Physics.OverlapSphere(transform.position, playerStatus.itemDetectionRange, itemLayerMask);

            if (detectedColliders.Length > 0)
            {
                float distance = float.MaxValue;

                foreach (Collider collider in detectedColliders)
                {
                    // ������ �ݶ��̴����� �Ÿ�
                    float distanceBetween = Vector3.Distance(transform.position, collider.transform.position);

                    // 1.�Ÿ� �� ����
                    if (distance > distanceBetween)
                    {
                        distance = distanceBetween;

                        targetObject = collider.gameObject;
                    }

                    else if (distance == distanceBetween)
                    {
                        if (targetObject != null)
                        {
                            var targetScript = targetObject.GetComponent<Gathering>();

                            var colliderScript = collider.GetComponent<Gathering>();

                            // 2. ü�� �� ����
                            if (targetScript.CurrentHealth > colliderScript.CurrentHealth)
                            {
                                targetObject = collider.gameObject;
                            }

                            // 3. ��� �� ����
                            else if (targetScript.CurrentHealth == colliderScript.CurrentHealth)
                            {
                                if (targetScript.GatheringData.MaxHealth < colliderScript.GatheringData.MaxHealth)
                                {
                                    targetObject = collider.gameObject;
                                }
                            }
                        }
                    }
                }

                isGathering = true;

                if (targetObject != null)
                {
                    ActiveGatheringBeam();
                }

                GatheringItem();
            }

            else
            {
                isGathering = false;

                DeactiveGatheringBeam();

                targetObject = null;
            }
        }
    }

    // ������ ä���� ����� �ڷ�ƾ
    private IEnumerator GatheringCoroutine()
    {
        Gathering targetScript = null;

        if (targetObject != null)
        {
            targetScript = targetObject.GetComponent<Gathering>();
        }

        while (true)
        {
            if (targetObject == null)
            {
                targetScript = null;

                isGatheringCoroutineWork = false;

                yield break;
            }

            yield return new WaitForSeconds(playerStatus.gatheringDelay);

            targetScript?.TakeDamage(playerStatus.gatheringSpeed);

            if (targetScript?.CurrentHealth <= 0f)
            {
                targetObject = null;

                isGathering = false;

                isGatheringCoroutineWork = false;

                yield break;
            }
        }
    }

    private Coroutine fadeCoroutine;

    public void ActiveSkillReject()
    {
        skillRejectText.text = "��ų�� ����� �� �����ϴ�.";

        // ȿ���� ���
        if (isSkillRejectActive == false)
        {
            isSkillRejectActive = true;

            fadeCoroutine = StartCoroutine(FadeOutReject());
        }

        else
        {
            StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeOutReject());
        }
    }

    private IEnumerator FadeOutReject()
    {
        Color color = Color.white;

        skillRejectText.color = color;

        yield return new WaitForSeconds(1f);

        while (true)
        {
            color.a -= Time.deltaTime;

            skillRejectText.color = color;

            if (color.a <= 0f)
            {
                isSkillRejectActive = false;

                yield break;
            }

            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, playerStatus.itemDetectionRange);
    }

    public void OnUseSpell()
    {
        playerSpellType.UseSpell();

        DeactiveGatheringBeam();
    }
}