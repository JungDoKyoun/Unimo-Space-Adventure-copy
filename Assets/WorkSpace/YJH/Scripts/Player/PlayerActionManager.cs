using System.Collections;

using TMPro;

using UnityEngine;

using ZL.Unity.Unimo;

public partial class PlayerManager 
{
    [Header("ä��")]

    [SerializeField]

    private float itemDetectionRange = 5f;

    public float ItemDetectionRange => itemDetectionRange;

    [SerializeField]

    private float gatheringSpeed = 4f;
    
    [SerializeField]

    private float gatheringDelay = 0.5f;
    
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

    [SerializeField]
    
    private GameObject attackPrefab;

    private IAttackType playerAttackType;

    //[SerializeField]

    //GameObject spellPrefab;

    private ISpellType playerSpellType = null;

    private int playerOwnEnergy = 0;

    private GameObject targetObject;

    private GameObject targetEnemyObject;

    // Start is called before the first frame update

    private bool isGatheringCoroutineWork = false;

    public delegate void OnTargetSet();

    public event OnTargetSet OnTargetObjectSet;

    private Vector3 firePos;

    [SerializeField]

    private float fireRate = 0.3f;

    private float fireTimer = 0f;

    [SerializeField]

    private TMP_Text skillRejectText;

    private bool isSkillRejectActive = false;

    private bool isItemNear = false;

    [SerializeField]

    private SphereCollider detectCollider;

    public void ActionStart()
    {
        gatheringAudioSource.clip = gatheringAudioClip;

        //StartDetectItem();

        //StartFindEnemy();

        OnTargetObjectSet += GatheringItem;

        detectCollider.radius = itemDetectionRange;

        SetAttackType(attackPrefab);

        if(playerSpellType != null)
        {
            //Debug.Log("notnullspell");

            playerSpellType.InitSpell();
        }

        else
        {
            //Debug.Log("nullspell");

            ISpellType temp = new Dash();

            //Debug.Log(temp);

            SetSpellType(temp);

            playerSpellType.InitSpell();
        }

        //SetAttackType(new EnergyBolt());
    }

    public void ActionUpdate()
    {
        playerSpellType.UpdateTime();
        if (isItemNear == true)
        {
            FindItemUpdate();
        }
    }

    public void StartDetectItem()
    {
        StartCoroutine(FindItem());
    }

    //public void StartFindEnemy()
    //{
    //    //StartCoroutine (FindEnemy());
    //}

    public void SetAttackType(GameObject attackType)
    {
        attackPrefab = attackType;

        playerAttackType = attackPrefab.GetComponent<IAttackType>();

        playerAttackType.Damage = playerDamage;
    }

    public void SetSpellType(ISpellType spellType)
    {
        //Debug.Log("set spell");

        playerSpellType = spellType;

        playerSpellType.SetPlayer(this);
    }

    public void GetItem(IGatheringObject temp)
    {
        temp.UseItem();
    }

    public void GetEnergy(int energyNum)//������ �ΰ� �߻��ϰ� ������
    {
        playerOwnEnergy += energyNum;

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

                if (fireCount % 2 == 0) // ¦���� ��
                {
                    int step = (i / 2) + 1;

                    int sign = (i % 2 == 0) ? -1 : 1; // �� �� ��

                    offsetIndex = step * sign;
                }

                else
                {
                    if (i == 0)
                    {
                        offsetIndex = 0; // �߾�
                    }

                    else
                    {
                        int step = (i + 1) / 2;

                        int sign = (i % 2 == 1) ? -1 : 1;

                        offsetIndex = step * sign;
                    }
                }

                Vector3 spawnPos = firePos + right * offsetIndex * spacing;

                PlayerAttack(spawnPos);
            }

            playerOwnEnergy %= playerAttackType.EnergyCost;
        }
    }

    // �ѹ��� 2�� �̻� ���� �� ���η� �÷��� �߻��ϴ� ������
    public void PlayerAttack()
    {
        playerOwnEnergy -= playerAttackType.EnergyCost;
        
        var bullet = Instantiate(attackPrefab, firePos, Quaternion.identity);

        //bullet.transform.LookAt(targetEnemyObject.transform);

        if (targetEnemyObject != null)
        {
            bullet.GetComponent<IAttackType>().Shoot(targetEnemyObject.transform.position - firePos);
        }

        else
        {
            bullet.GetComponent<IAttackType>().Shoot(firePos - transform.position);
        }
    }

    public void PlayerAttack(Vector3 firePosition)
    {
        playerOwnEnergy -= playerAttackType.EnergyCost;

        var bullet = Instantiate(attackPrefab, firePosition, Quaternion.identity);

        //bullet.transform.LookAt(targetEnemyObject.transform);

        if (targetEnemyObject != null)
        {
            bullet.GetComponent<IAttackType>().Shoot(targetEnemyObject.transform.position - firePos);
        }

        else
        {
            bullet.GetComponent<IAttackType>().Shoot(firePos-transform.position);
        }
    }
   
    public void GatheringItem()
    {
        //Debug.Log("gathering2");

        if (isGatheringCoroutineWork == false)
        {
            isGatheringCoroutineWork = true;

            StartCoroutine(GatheringCoroutine());
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
                //������ �ݶ��̴����� �Ÿ�
                float distanceBetween = Vector3.Distance(transform.position, collider.transform.position);

                //1.�Ÿ� �� ����
                if (distance > distanceBetween)
                {
                    distance = distanceBetween;

                    targetEnemyObject = collider.gameObject;

                    //Debug.Log("detected");

                    //Debug.Log(targetObject.name);
                }
            }
        }

        else
        {
            targetEnemyObject = null;
        }
    }

    public void ActiveGatheringBeam()
    {
        gatheringEffect.SetActive(true);
    }

    public void DeactiveGatheringBeam()
    {
        gatheringEffect.SetActive(false);
    }

    private void FindItemUpdate()
    {
        

            if (targetObject == null)
            {
                //Debug.Log("null");

                isGathering = false;

                //gatheringEffect.SetActive(false);
            }

            else
            {
                //Debug.Log(Vector3.Distance(transform.position, targetObject.transform.position));

                if (Vector3.Distance(transform.position, targetObject.transform.position) > itemDetectionRange + float.Epsilon)
                {
                    isGathering = false;

                    targetObject = null;

                    //gatheringEffect.SetActive(false);
                }
            }

            if (isGathering == false && playerSpellType.ReturnState() == false)
            {
                

                Collider[] detectedColliders = Physics.OverlapSphere(transform.position, itemDetectionRange, itemLayerMask);

                if (detectedColliders.Length > 0)
                {
                    float distance = float.MaxValue;

                    foreach (Collider collider in detectedColliders)
                    {
                        //������ �ݶ��̴����� �Ÿ�
                        float distanceBetween = Vector3.Distance(transform.position, collider.transform.position);

                        //1.�Ÿ� �� ����
                        if (distance > distanceBetween)
                        {
                            distance = distanceBetween;

                            targetObject = collider.gameObject;
                        }

                        else if (distance == distanceBetween)
                        {
                            if (targetObject != null)
                            {
                                //var targetScript = targetObject.GetComponent<IGatheringObject>();

                                var targetScript = targetObject.GetComponent<Gathering>();

                                //var colliderScript = collider.GetComponent<IGatheringObject>();

                                var colliderScript = collider.GetComponent<Gathering>();

                                //2. ü�� �� ����
                                if (targetScript.CurrentHealth > colliderScript.CurrentHealth)
                                {
                                    targetObject = collider.gameObject;
                                }

                                //3. ��� �� ����
                                else if (targetScript.CurrentHealth == colliderScript.CurrentHealth)
                                {
                                    //if (targetScript.MaxHealth < colliderScript.MaxHealth)

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

                    OnTargetObjectSet?.Invoke();
                }

                else
                {
                    isGathering = false;

                    DeactiveGatheringBeam();

                    targetObject = null;
                }
            }
        
    }
    private IEnumerator FindItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (targetObject == null)
            {
                //Debug.Log("null");

                isGathering = false;

                //gatheringEffect.SetActive(false);
            }

            else
            {
                //Debug.Log(Vector3.Distance(transform.position, targetObject.transform.position));

                if (Vector3.Distance(transform.position, targetObject.transform.position) > itemDetectionRange+float.Epsilon)
                {
                    isGathering = false;

                    targetObject = null;

                    //gatheringEffect.SetActive(false);
                }
            }

            while (isGathering == false && playerSpellType.ReturnState() == false)
            {
                yield return new WaitForSeconds(0.1f);

                Collider[] detectedColliders = Physics.OverlapSphere(transform.position, itemDetectionRange, itemLayerMask);

                if (detectedColliders.Length > 0)
                {
                    float distance = float.MaxValue;

                    foreach (Collider collider in detectedColliders)
                    {
                        //������ �ݶ��̴����� �Ÿ�
                        float distanceBetween = Vector3.Distance(transform.position, collider.transform.position);

                        //1.�Ÿ� �� ����
                        if (distance > distanceBetween)
                        {
                            distance = distanceBetween;

                            targetObject = collider.gameObject;
                        }

                        else if (distance == distanceBetween)
                        {
                            if (targetObject != null)
                            {
                                //var targetScript = targetObject.GetComponent<IGatheringObject>();

                                var targetScript = targetObject.GetComponent<Gathering>();

                                //var colliderScript = collider.GetComponent<IGatheringObject>();

                                var colliderScript = collider.GetComponent<Gathering>();

                                //2. ü�� �� ����
                                if (targetScript.CurrentHealth > colliderScript.CurrentHealth)
                                {
                                    targetObject = collider.gameObject;
                                }

                                //3. ��� �� ����
                                else if (targetScript.CurrentHealth == colliderScript.CurrentHealth)
                                {
                                    //if (targetScript.MaxHealth < colliderScript.MaxHealth)

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

                    OnTargetObjectSet?.Invoke();
                }

                else
                {
                    isGathering = false;

                    DeactiveGatheringBeam();

                    targetObject = null;
                }
            }
        }
    }

    // ������ ä���� ����� �ڷ�ƾ
    private IEnumerator GatheringCoroutine()
    {
        //IGatheringObject targetScript = null;

        Gathering targetScript = null;

        if (targetObject != null)
        {
            //targetScript = targetObject.GetComponent<IGatheringObject>();

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

            //Debug.Log("gathering");

            yield return new WaitForSeconds(gatheringDelay);

            //targetScript.CurrentHealth -= gatheringSpeed;

            targetScript?.TakeDamage(gatheringSpeed);

            if(targetScript?.CurrentHealth <= 0f)
            {
                //targetScript.OnGatheringEnd();

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

        //ȿ���� ���
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

        Gizmos.DrawWireSphere(transform.position,itemDetectionRange);
    }

    public void OnUseSpell()
    {
        //Debug.Log("pressedQ");

        playerSpellType.UseSpell();

        DeactiveGatheringBeam() ;
    }
}