using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardSlot : MonoBehaviour
{
    [SerializeField] Image _rewardImage;
    [SerializeField] TextMeshProUGUI _rewardName;
    [SerializeField] TextMeshProUGUI _rewardCount;

    public void SetRewardSlot(Sprite sprite, string name, int count)
    {
        _rewardImage.sprite = sprite;
        _rewardName.text = name;
        _rewardCount.text = count.ToString();
    }
}
