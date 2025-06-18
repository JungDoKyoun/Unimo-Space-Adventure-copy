using System.Collections;
using UnityEngine;
using ZL.Unity.Unimo;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int bossKill;

    private int stageScore;

    private int itemScore;

    private int leftFuelScore;

    private int leftHealthScore;

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

        leftFuelScore = 0;

        leftHealthScore = 0;

        totalScore = 0;
    }

    /// <summary>
    /// 주기 끝날 때 호출
    /// </summary>
    public void CalculateTotalScore()
    {
        // 남은 연료 량 가져오기(leftFuelScore)
        CountLeftFuel((int)PlayerFuelManager.Fuel);

        // 플레이어 체력 비율 가져오기(leftHealthScore)
        CountLeftHP(PlayerManager.PlayerStatus.currentHealth, PlayerManager.PlayerStatus.maxHealth);

        totalScore = bossKill * 500 + stageScore + itemScore + leftFuelScore + leftHealthScore;

        updateScore = FirebaseDataBaseMgr.Instance.UpdateScore(totalScore);

        StartCoroutine(updateScore);

        InitScore();
    }

    private IEnumerator updateScore = null;

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
            leftFuelScore = 0;

            return;
        }

        leftFuelScore = leftFuel;
    }

    /// <summary>
    /// 주기 끝날 때 호출(플레이어 현재 체력 / 플레이어 최대 체력)
    /// </summary>
    /// <param name="currentPlayerHP"></param>
    /// <param name="maxPlayerHP"></param>
    public void CountLeftHP(float currentPlayerHP, float maxPlayerHP)
    {
        if (maxPlayerHP <= 0)
        {
            leftHealthScore = 0;

            return;
        }

        leftHealthScore = (int)((currentPlayerHP / maxPlayerHP) * 100);
    }
}