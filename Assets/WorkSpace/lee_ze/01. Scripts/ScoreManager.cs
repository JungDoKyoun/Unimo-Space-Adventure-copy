using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private static int bossKill;

    private static int hardStageClear;

    private static int getItem;

    private static float leftTime;

    private static float leftHP;

    #region ������Ƽ

    public static int BossKill
    {
        get => bossKill;

        private set
        {
            bossKill = value;
        }
    }

    public static int HardStageClear
    {
        get => hardStageClear;

        private set
        {
            hardStageClear = value;
        }
    }

    public static int GetItem
    {
        get => getItem;

        private set
        {
            getItem = value;
        }
    }

    public static float LeftTime
    {
        get => leftTime;

        private set
        {
            leftTime = value;
        }
    }

    public static float LeftHP
    {
        get => leftHP;

        private set
        {
            leftHP = value;
        }
    }

    #endregion

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

    public void CountReset()
    {
        BossKill = 0;

        HardStageClear = 0;

        GetItem = 0;

        LeftTime = 0;

        LeftHP = 0;
    }

    public void CountBossKill()
    {
        // ���� ų Ƚ��
    }

    public void CountHardStageClear()
    {
        // ����� �������� Ŭ���� Ƚ��
    }

    public void CountGetItem()
    {
        // ���� ȹ�� ����
    }

    public void CountLeftTime()
    {
        // ���� �ð�
    }

    public void CountLeftHP()
    {
        // ���� HP ����
    }
}
