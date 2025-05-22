using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public static SceneControl Instance;

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

    public void GoToSpaceship() // start button에 임시 바인딩
    {
        // 기지 이동
        SceneManager.LoadScene("Station");
    }

    public void GoToRegister()
    {
        // 계정씬 이동
        SceneManager.LoadScene("Register");
    }
}
