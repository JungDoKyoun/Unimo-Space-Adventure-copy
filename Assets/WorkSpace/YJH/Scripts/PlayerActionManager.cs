using System.Collections;
using TMPro;
using UnityEngine;

public partial class PlayerManager 
{
    [Header("채집")]

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

    [Header("채집 소리")]

    [SerializeField]
    
    private AudioClip gatheringAudioClip;

    [SerializeField]
    
    private AudioSource gatheringAudioSource;

    [Header("탐지할 오브젝트의 레이어")]

    [SerializeField]
    
    private LayerMask itemLayerMask;

    [Header("탐지할 적의 레이어")]

    [SerializeField]
    
    private LayerMask enemyLayerMask;

    [SerializeField]
    
    private GameObject attackPrefab;

    private IAttackType playerAttackType;

    //[SerializeField]

    //GameObject spellPrefab;

    private ISpellType playerSpellType;

    private int playerOwnEnergy = 0;

    private GameObject targetObject;

    private GameObject targetEnemyObject;

    // Start is called before the first frame update

    private bool isGatheringCoroutineWork = false;

    public delegate void OnTargetSet();

    public event OnTargetSet OnTargetObjectSet;

    private Vector3 firePos;

    [SerializeField] float fireRate=0.3f;

    private float fireTimer = 0;
    [SerializeField] TMP_Text skillRejectText;
    public void ActionStart()
    {
        gatheringAudioSource.clip = gatheringAudioClip;
        StartDetectItem();
        //StartFindEnemy();
        OnTargetObjectSet += GatheringItem;
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
    // Update is called once per frame
    
    public void ActionUpdate()
    {
        playerSpellType.UpdateTime();
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
        Debug.Log("set spell");
        playerSpellType = spellType;
        playerSpellType.SetPlayer(this);
    }

    public void GetItem(IGatheringObject temp)
    {
        temp.UseItem();
    }

    public void GetEnergy(int energyNum)//딜레이 두고 발사하게 수정중
    {
        playerOwnEnergy += energyNum;
        if(playerOwnEnergy >= playerAttackType.EnergyCost)
        {
            FindEnemy();
            playerOwnEnergy -= playerAttackType.EnergyCost;
            if(playerOwnEnergy >= playerAttackType.EnergyCost)
            {
                PlayerAttack();
                
            }
            else
            {
                PlayerAttack();
            }
                
        }
    }
    
    public void PlayerAttack()
    {
        

        if (targetEnemyObject != null)
        {
            Vector3 tempDirection = new Vector3();
            tempDirection = targetEnemyObject.transform.position - transform.position;
            firePos = transform.position + (tempDirection).normalized * 1.5f;

        }
        else
        {
            firePos = transform.forward * 1.5f;
        }
        var bullet = Instantiate(attackPrefab, firePos, Quaternion.identity);
        //bullet.transform.LookAt(targetEnemyObject.transform);
        bullet.GetComponent<IAttackType>().Shoot(targetEnemyObject.transform.position - firePos);
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
                float distanceBetween = Vector3.Distance(transform.position, collider.transform.position);//감지된 콜라이더와의 거리
                if (distance > distanceBetween)//1.거리 비교 조건
                {
                    distance = distanceBetween;
                    targetEnemyObject = collider.gameObject;
                    //Debug.Log("detected");
                    //Debug.Log(targetObject.name);
                }
            }
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

    private IEnumerator FindItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (targetObject == null)
            {
                //Debug.Log("null");
                isGathering = false;
                //gatheringEffect.SetActive(false);
            }
            else
            {
               // Debug.Log(Vector3.Distance(transform.position, targetObject.transform.position));
                if (Vector3.Distance(transform.position, targetObject.transform.position) > itemDetectionRange+float.Epsilon)
                {
                    isGathering = false;
                    targetObject = null;
                    //gatheringEffect.SetActive(false);
                }
            }

            while (isGathering == false&&playerSpellType.ReturnState()==false)
            {
                
                yield return new WaitForSeconds(0.1f);
                Collider[] detectedColliders = Physics.OverlapSphere(transform.position, itemDetectionRange, itemLayerMask);

                if (detectedColliders.Length > 0)
                {
                    
                    float distance = float.MaxValue;
                    foreach (Collider collider in detectedColliders)
                    {
                        float distanceBetween = Vector3.Distance(transform.position, collider.transform.position);//감지된 콜라이더와의 거리
                        if (distance > distanceBetween)//1.거리 비교 조건
                        {
                            distance = distanceBetween;
                            targetObject = collider.gameObject;
                            
                        }
                        else if (distance == distanceBetween)
                        {
                            if (targetObject != null)
                            {
                                var targetScript = targetObject.GetComponent<IGatheringObject>();
                                var colliderScript = collider.GetComponent<IGatheringObject>();

                                if (targetScript.NowHP > colliderScript.NowHP)//2. 체력 비교 조건
                                {
                                    targetObject = collider.gameObject;
                                }
                                else if (targetScript.NowHP == colliderScript.NowHP)//3. 등급 비교 조건
                                {
                                    if (targetScript.MaxHP < colliderScript.MaxHP)
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
    private IEnumerator GatheringCoroutine()// 아이템 채집중 사용할 코로틴
    {
        IGatheringObject targetScript = null;
        if (targetObject != null)
        {
             targetScript= targetObject.GetComponent<IGatheringObject>();
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
            targetScript.NowHP -= gatheringSpeed;
            if(targetScript.NowHP < 0)
            {
                targetScript.OnGatheringEnd();
                targetObject = null;
                isGathering= false;
                isGatheringCoroutineWork = false;
                yield break;
            }
            
        }
    }
    public void ActiveSkillReject()
    {
        skillRejectText.text = "스킬을 사용할 수 없습니다.";
        StartCoroutine(FadeOutReject());
    }

    IEnumerator FadeOutReject()
    {
        Color color = Color.white;
        color.a = 1;
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            skillRejectText.color = color;
            color.a-=Time.deltaTime;


            if(color.a <= 0)
            {
                yield break;
            }


        }
    }

    private void OnDrawGizmos()
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