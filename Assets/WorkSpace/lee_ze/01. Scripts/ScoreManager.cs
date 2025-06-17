using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int bossKill;

    private int stageScore;

    private int itemScore;

    private int fuelScore;

    private int healthScore;

    private int totalScore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

            return;
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitScore();
    }

    private void InitScore()
    {
        bossKill = 0;

        stageScore = 0;

        itemScore = 0;

        fuelScore = 0;

        healthScore = 0;

        totalScore = 0;
    }

    /// <summary>
    /// 주기 끝날 때 호출
    /// </summary>
    public void CalculateScore()
    {
        totalScore = bossKill * 500 + stageScore + itemScore + fuelScore + healthScore;

        StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateScore(totalScore));

        InitScore();
    }

    // 보스 죽을 때 호출
    public void CountBossKill()
    {
        bossKill++;
    }

    // 스테이지 끝날 때 호출
    public void CountStageClear(int stageScore)
    {
        this.stageScore += stageScore;
    }

    // 유물 선택 할 때 호출
    public void CountGetItem(int itemScore)
    {
        this.itemScore += itemScore;
    }

    /// <summary>
    /// 주기 끝날 때 호출(남은 연료 량)
    /// </summary>
    /// <param name="leftFuel"></param>
    public void CountLeftFuel(int leftFuel)
    {
        if (leftFuel <= 0)
        {
            fuelScore = 0;

            return;
        }

        fuelScore = leftFuel;
    }

    /// <summary>
    /// 주기 끝날 때 호출(현재 체력 / 최대 체력)
    /// </summary>
    /// <param name="currentPlayerHP"></param>
    /// <param name="maxPlayerHP"></param>
    public void CountLeftHP(float currentPlayerHP, float maxPlayerHP)
    {
        if (maxPlayerHP <= 0)
        {
            healthScore = 0;

            return;
        }

        healthScore = (int)((currentPlayerHP / maxPlayerHP) * 100);
    }
}