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

    public void GoToSpaceship() // start button�� �ӽ� ���ε�
    {
        // ���� �̵�
        SceneManager.LoadScene("Station");
    }

    public void GoToRegister()
    {
        // ������ �̵�
        SceneManager.LoadScene("Register");
    }
}
