using JetBrains.Annotations;
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

    private void CalculateScore()
    {
        totalScore = bossKill * 500 + stageScore + itemScore + fuelScore + healthScore;

        StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateScore(totalScore));
    }

    public void CountBossKill()
    {
        bossKill++;
    }

    public void CountStageClear(int stageScore)
    {
        this.stageScore += stageScore;
    }

    public void CountGetItem(int itemScore)
    {
        this.itemScore += itemScore;
    }

    public void CountLeftFuel(int leftFuel)
    {
        if (leftFuel <= 0)
        {
            fuelScore = 0;

            return;
        }

        fuelScore = leftFuel;
    }

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