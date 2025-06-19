using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZL.Unity.Unimo;

public class RelicUISlot : MonoBehaviour
{
    [SerializeField] Image _relicImage;
    [SerializeField] TextMeshProUGUI _relicName;
    [SerializeField] private ImageTable _imageTable;

    public void Init(string name)
    {
        _relicImage.sprite = _imageTable[name];
        _relicName.text = name;
    }
}