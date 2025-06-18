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
    /// �ֱ� ���� �� ȣ��
    /// </summary>
    public void CalculateTotalScore()
    {
        // ���� ���� �� ��������(leftFuelScore)
        CountLeftFuel((int)PlayerFuelManager.Fuel);

        // �÷��̾� ü�� ���� ��������(leftHealthScore)
        CountLeftHP(PlayerManager.PlayerStatus.currentHealth, PlayerManager.PlayerStatus.maxHealth);

        totalScore = bossKill * 500 + stageScore + itemScore + leftFuelScore + leftHealthScore;

        updateScore = FirebaseDataBaseMgr.Instance.UpdateScore(totalScore);

        StartCoroutine(updateScore);

        InitScore();
    }

    private IEnumerator updateScore = null;

    // ���� ���� �� ȣ��
    public void CountBossKill()
    {
        bossKill++;
    }

    // �������� ���� �� ȣ��
    public void CountStageClear(int stageScore)
    {
        this.stageScore += stageScore;
    }

    // ���� ���� �� �� ȣ��
    public void CountGetItem(int itemScore)
    {
        this.itemScore += itemScore;
    }

    /// <summary>
    /// �ֱ� ���� �� ȣ��(���� ���� ��)
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
    /// �ֱ� ���� �� ȣ��(�÷��̾� ���� ü�� / �÷��̾� �ִ� ü��)
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