using Photon.Pun;
using System.Collections;

using TMPro;
using Unity.VisualScripting;
using UnityEngine;

using ZL.Unity.Unimo;

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
    
    private static GameObject attackPrefab;
    public GameObject tempAttackPrefab;

    private static IAttackType playerAttackType;

    //[SerializeField]

    //GameObject spellPrefab;

    private static ISpellType playerSpellType = null;

    private int playerOwnEnergy = 0;

    private GameObject targetObject;

    private GameObject targetEnemyObject;

    // Start is called before the first frame update

    private bool isGatheringCoroutineWork = false;

    public delegate void OnTargetSet();

    public event OnTargetSet OnTargetObjectSet;

    private Vector3 firePos;
    private static PlayerManager selfManager;
    public static PlayerManager SelfManager
    {
        get { return selfManager; }
    }
    //[SerializeField]

    //private float fireRate = 0.3f;
    //
    //private float fireTimer = 0f;

    [SerializeField]

    private TMP_Text skillRejectText;

    private bool isSkillRejectActive = false;

    private bool isItemNear = false;

    [SerializeField]

    private SphereCollider detectCollider;

    public void ActionStart()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            gatheringAudioSource.clip = gatheringAudioClip;
            
            //StartDetectItem();

            //StartFindEnemy();

            OnTargetObjectSet += GatheringItem;

            detectCollider.radius = itemDetectionRange;
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
                Debug.Log("notnullspell");

                playerSpellType.InitSpell();
            }

            else
            {
                Debug.Log("nullspell");

                ISpellType temp = new Dash();

                //Debug.Log(temp);

                SetSpellType(temp);

                playerSpellType.InitSpell();
            }
        }
        else if (photonView.IsMine == true)
        {
            gatheringAudioSource.clip = gatheringAudioClip;
            
            

            OnTargetObjectSet += GatheringItem;

            detectCollider.radius = itemDetectionRange;

            SetAttackType(attackPrefab);

            if (playerSpellType != null)
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
    }

    public void ActionUpdate()
    {
        if (PhotonNetwork.IsConnected == false || photonView.IsMine==true)
        {
            if (playerSpellType != null)
            {
                playerSpellType.UpdateTime();
            }
        }
        if (isItemNear == true)
        {
            if (PhotonNetwork.IsConnected == false)
            {
                FindItemUpdate();
            }else if (photonView.IsMine == true)
            {
                FindItemUpdate();
                //photonView.RPC("FindItemUpdate", RpcTarget.All);
            }
        }
    }

    //public void StartDetectItem()
    //{
    //    StartCoroutine(FindItem());
    //}

    //public void StartFindEnemy()
    //{
    //    //StartCoroutine (FindEnemy());
    //}

    public static void SetAttackType(GameObject attackType)
    {
        attackPrefab = attackType;

        playerAttackType = attackPrefab.GetComponent<IAttackType>();

        playerAttackType.Damage = playerDamage;
    }

    public static void SetSpellType(ISpellType spellType)
    {
        //Debug.Log("set spell");

        playerSpellType = spellType;

        playerSpellType.SetPlayer(selfManager);
    }

    public void GetItem(IGatheringObject temp)
    {
        temp.UseItem();
    }

    
    public void GetEnergy(int energyNum)//멀티에서도 공격이 있나? -> 있음
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

                if (fireCount % 2 == 0) // 짝수일 때
                {
                    int step = (i / 2) + 1;

                    int sign = (i % 2 == 0) ? -1 : 1; // 좌 → 우

                    offsetIndex = step * sign;
                }

                else
                {
                    if (i == 0)
                    {
                        offsetIndex = 0; // 중앙
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
    }

    // 한번에 2개 이상 먹을 시 가로로 늘려서 발사하는 것으로
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
                //감지된 콜라이더와의 거리
                float distanceBetween = Vector3.Distance(transform.position, collider.transform.position);

                //1.거리 비교 조건
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

    //[PunRPC]
    private void FindItemUpdate()
    {
        if (PhotonNetwork.IsConnected == true && photonView.IsMine != true)
        {
            return;
        }
        

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
                        //감지된 콜라이더와의 거리
                        float distanceBetween = Vector3.Distance(transform.position, collider.transform.position);

                        //1.거리 비교 조건
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

                                //2. 체력 비교 조건
                                if (targetScript.CurrentHealth > colliderScript.CurrentHealth)
                                {
                                    targetObject = collider.gameObject;
                                }

                                //3. 등급 비교 조건
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
                    if (PhotonNetwork.IsConnected)
                    {
                        photonView.RPC("ActiveGatheringBeam", RpcTarget.All);
                    }
                    else
                    {
                        ActiveGatheringBeam();
                    }
                    }

                    OnTargetObjectSet?.Invoke();
                }

                else
                {
                    isGathering = false;
                if (PhotonNetwork.IsConnected)
                {
                    photonView.RPC("DeactiveGatheringBeam", RpcTarget.All);
                }
                else
                {

                    DeactiveGatheringBeam();
                }

                    targetObject = null;
                }
            }
        
    }
    //private IEnumerator FindItem()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //
    //        if (targetObject == null)
    //        {
    //            //Debug.Log("null");
    //
    //            isGathering = false;
    //
    //            //gatheringEffect.SetActive(false);
    //        }
    //
    //        else
    //        {
    //            //Debug.Log(Vector3.Distance(transform.position, targetObject.transform.position));
    //
    //            if (Vector3.Distance(transform.position, targetObject.transform.position) > itemDetectionRange+float.Epsilon)
    //            {
    //                isGathering = false;
    //
    //                targetObject = null;
    //
    //                //gatheringEffect.SetActive(false);
    //            }
    //        }
    //
    //        while (isGathering == false && playerSpellType.ReturnState() == false)
    //        {
    //            yield return new WaitForSeconds(0.1f);
    //
    //            Collider[] detectedColliders = Physics.OverlapSphere(transform.position, itemDetectionRange, itemLayerMask);
    //
    //            if (detectedColliders.Length > 0)
    //            {
    //                float distance = float.MaxValue;
    //
    //                foreach (Collider collider in detectedColliders)
    //                {
    //                    //감지된 콜라이더와의 거리
    //                    float distanceBetween = Vector3.Distance(transform.position, collider.transform.position);
    //
    //                    //1.거리 비교 조건
    //                    if (distance > distanceBetween)
    //                    {
    //                        distance = distanceBetween;
    //
    //                        targetObject = collider.gameObject;
    //                    }
    //
    //                    else if (distance == distanceBetween)
    //                    {
    //                        if (targetObject != null)
    //                        {
    //                            //var targetScript = targetObject.GetComponent<IGatheringObject>();
    //
    //                            var targetScript = targetObject.GetComponent<Gathering>();
    //
    //                            //var colliderScript = collider.GetComponent<IGatheringObject>();
    //
    //                            var colliderScript = collider.GetComponent<Gathering>();
    //
    //                            //2. 체력 비교 조건
    //                            if (targetScript.CurrentHealth > colliderScript.CurrentHealth)
    //                            {
    //                                targetObject = collider.gameObject;
    //                            }
    //
    //                            //3. 등급 비교 조건
    //                            else if (targetScript.CurrentHealth == colliderScript.CurrentHealth)
    //                            {
    //                                //if (targetScript.MaxHealth < colliderScript.MaxHealth)
    //
    //                                if (targetScript.GatheringData.MaxHealth < colliderScript.GatheringData.MaxHealth)
    //                                {
    //                                    targetObject = collider.gameObject;
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //
    //                isGathering = true;
    //
    //                if (targetObject != null)
    //                {
    //                    ActiveGatheringBeam();
    //                }
    //
    //                OnTargetObjectSet?.Invoke();
    //            }
    //
    //            else
    //            {
    //                isGathering = false;
    //
    //                DeactiveGatheringBeam();
    //
    //                targetObject = null;
    //            }
    //        }
    //    }
    //}

    // 아이템 채집중 사용할 코로틴
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
        skillRejectText.text = "스킬을 사용할 수 없습니다.";

        //효과음 출력
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