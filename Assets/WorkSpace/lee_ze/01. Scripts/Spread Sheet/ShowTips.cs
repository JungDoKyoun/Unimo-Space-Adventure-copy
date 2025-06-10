using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTips : MonoBehaviour
{
    private TextMeshProUGUI gameTip;

    private void OnEnable()
    {
        //if (gameTip == null)
        //{
        //    gameTip = GameObject.Find("Matching Canvas")?.transform.Find("Under Bar Panel/Game Tips").GetComponent<TextMeshProUGUI>();
        //}
        //else
        //{
        //    gameTip.text = "Tips \n\n" + TipsFromSpreadSheet.tips[Random.Range(0, TipsFromSpreadSheet.tips.Length)];
        //}
    }

    public void Tips()
    {
        if (gameTip == null)
        {
            gameTip = GameObject.Find("Matching Canvas")?.transform.Find("Under Bar Panel/Game Tips").GetComponent<TextMeshProUGUI>();
        }
        if (gameTip != null)
        {
            gameTip.text = "Tips \n\n" + TipsFromSpreadSheet.tips[Random.Range(0, TipsFromSpreadSheet.tips.Length)];
        }
    }
}
