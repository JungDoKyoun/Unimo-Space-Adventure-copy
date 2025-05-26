using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour
{
    public static SceneControl Instance;

    private Button pioneerButton;

    private Button competeButton;

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
        var pioneer = GameObject.Find("Mode Selection Canvas")?.transform.Find("Cards/Pioneer Button"); // PvE 버튼 할당

        var compete = GameObject.Find("Mode Selection Canvas")?.transform.Find("Cards/Compete Button"); // PvP 버튼 할당

        if (pioneer != null)
        {
            pioneerButton = pioneer.GetComponent<Button>();

            pioneerButton.onClick.AddListener(() => GoToSpaceship());
        }

        if (compete != null)
        {
            competeButton = compete.GetComponent<Button>();

            competeButton.onClick.AddListener(() => GoToCompete());
        }
    }

    public void GoToMain()
    {
        // 계정씬(메인) 이동
        SceneManager.LoadScene("Account");
    }

    private void GoToSpaceship() // start button에 임시 바인딩
    {
        // 기지 이동
        SceneManager.LoadScene("Station");
    }

    private void GoToCompete()
    {
        // 퀵매치 이동
        SceneManager.LoadScene("Compete");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
