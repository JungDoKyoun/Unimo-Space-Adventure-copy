using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JDG;

public class RewardSlot : MonoBehaviour
{
    [SerializeField] Image _rewardImage;
    [SerializeField] TextMeshProUGUI _rewardCount;

    public void SetRewardSlot(Sprite sprite, string count)
    {
        _rewardImage.sprite = sprite;
        _rewardCount.text = count;
    }
}
