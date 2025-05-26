using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WhereToGo : MonoBehaviour
{
    [SerializeField]
    private Button mainButton;

    [SerializeField]
    private Button worldMapButton;

    //[SerializeField]
    //private Button developButton;

    private GameObject worldMap;

    //private GameObject develop;

    private void Start()
    {
        worldMap = GameObject.Find("World Map");

        mainButton.onClick.AddListener(() => SceneControl.Instance.GoToMain());

        worldMapButton.onClick.AddListener(() => ShowWorldMap());
    }

    private void ShowWorldMap()
    {
        worldMap.SetActive(true);

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        mainButton.onClick.RemoveListener(() => SceneControl.Instance.GoToMain());

        worldMapButton.onClick.RemoveListener(() => ShowWorldMap());
    }
}
