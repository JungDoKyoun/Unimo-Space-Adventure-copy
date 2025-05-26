using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour
{
    public static SceneControl Instance;

    private Button pioneerButton;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var tempButton = GameObject.Find("Mode Selection Canvas")?.transform.Find("Cards/Pioneer Button");

        if (tempButton != null)
        {
            pioneerButton = tempButton.GetComponent<Button>();

            pioneerButton.onClick.AddListener(() => GoToSpaceship());
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

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
