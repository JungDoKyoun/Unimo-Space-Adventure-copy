using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private Image _ingameImage;
    [SerializeField] private Image _metaImage;
    [SerializeField] private Image _blueprinImage;
    [SerializeField] private TextMeshProUGUI _ingameText;
    [SerializeField] private TextMeshProUGUI _metaText;
    [SerializeField] private TextMeshProUGUI _blueprintText;
    private int _ingame;
    private int _meta;
    private int _blueprint;

    private void OnEnable()
    {
        _ingameImage.sprite = Resources.Load<Sprite>($"WorldMap/Reward/InGameCurrencyIcon");
        _metaImage.sprite = Resources.Load<Sprite>($"WorldMap/Reward/OutGameCurrencyIcon");
        _blueprinImage.sprite = Resources.Load<Sprite>($"WorldMap/Reward/BluePrintIcon");
        PlayerEvents._OnCurrencyChanged += UpdateCurrencyUI;
        StartCoroutine(DelayedInit());
    }

    private void OnDisable()
    {
        PlayerEvents._OnCurrencyChanged -= UpdateCurrencyUI;
    }

    public void UpdateCurrencyUI()
    {
        _ingame = FirebaseDataBaseMgr.IngameCurrency;
        _meta = FirebaseDataBaseMgr.MetaCurrency;
        _blueprint = FirebaseDataBaseMgr.Blueprint;

        _ingameText.text = $"X {_ingame}";
        _metaText.text = $"X {_meta}";
        _blueprintText.text = $"X {_blueprint}";
    }

    private IEnumerator DelayedInit()
    {
        yield return new WaitUntil(() => FirebaseDataBaseMgr.IsDataBaseReady);

        yield return new WaitForSeconds(0.5f);

        UpdateCurrencyUI();
    }
}
