using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    // Start is called before the first frame update


    public static GameManager Instance { get; private set; }
    [SerializeField] ConstructManager constructManager;
    private PlayerManager playerManager;
    public PlayerStatus playerStatus= new PlayerStatus();
    [SerializeField] PlayerStatus originPlayerStatus= new PlayerStatus();
    public PlayerStatus OriginPlayerStatus { get {  return originPlayerStatus; } }
    private List<BuildEffect> buildEffects = new List<BuildEffect>();

    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
    void Start()
    {
        playerManager= FindObjectOfType<PlayerManager>();
        playerStatus= originPlayerStatus;
    }

   
   
    public void AddStatEffect(BuildEffect buildEffect)
    {
        buildEffects.Add(buildEffect);
    }
    public void ModifieStat()
    {
        float speedSum=originPlayerStatus.moveSpeed;
        float maxHPSum = originPlayerStatus.maxHP;
        float gatherSpeedSum=originPlayerStatus.gatheringSpeed;
        float gatherDelaySum=originPlayerStatus.gatheringDelay;
        float damageSum=originPlayerStatus.playerDamage;
        float gatherRangeSum=originPlayerStatus.itemDetectionRange;
        
        foreach (var buildeffect in buildEffects)
        {
            switch(buildeffect)
            {
                case Speed speed:
                    speedSum += buildeffect.ReturnFinalStat(originPlayerStatus.moveSpeed);

                    break;
                case MaxHp maxHP:
                    maxHPSum += buildeffect.ReturnFinalStat(originPlayerStatus.maxHP);

                    break;
                case GatheringSpeed gatheringSpeed:
                    gatherSpeedSum += buildeffect.ReturnFinalStat(originPlayerStatus.gatheringSpeed);
                    break;
                case GatheringDelay gatheringDelay:
                    gatherDelaySum += buildeffect.ReturnFinalStat(originPlayerStatus.gatheringDelay);
                    break;
                case Damage damage:
                    damageSum += buildeffect.ReturnFinalStat(originPlayerStatus.playerDamage);
                    break;
                case ItemDetectionRange itemDetectionRange:
                    gatherRangeSum += buildeffect.ReturnFinalStat(originPlayerStatus.itemDetectionRange);
                    break;
                default:

                    break;

            }
        }
        
        playerStatus=originPlayerStatus.Clone();
        playerStatus.moveSpeed += speedSum;
        playerStatus.maxHP += maxHPSum;
        playerStatus.gatheringSpeed += gatherSpeedSum;
        playerStatus.gatheringDelay += gatherDelaySum;
        playerStatus.playerDamage += damageSum;
        playerStatus.itemDetectionRange += gatherRangeSum;





    }
}
