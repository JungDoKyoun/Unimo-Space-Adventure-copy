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

    #region 프로퍼티

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
        // 보스 킬 횟수
    }

    public void CountHardStageClear()
    {
        // 어려운 스테이지 클리어 횟수
    }

    public void CountGetItem()
    {
        // 유물 획득 개수
    }

    public void CountLeftTime()
    {
        // 남은 시간
    }

    public void CountLeftHP()
    {
        // 남은 HP 비율
    }
}
