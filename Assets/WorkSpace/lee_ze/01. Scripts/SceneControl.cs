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
        var pioneer = GameObject.Find("Mode Selection Canvas")?.transform.Find("Cards/Pioneer Button"); // PvE ��ư �Ҵ�

        var compete = GameObject.Find("Mode Selection Canvas")?.transform.Find("Cards/Compete Button"); // PvP ��ư �Ҵ�

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
        // ������(����) �̵�
        SceneManager.LoadScene("Account");
    }

    private void GoToSpaceship() // start button�� �ӽ� ���ε�
    {
        // ���� �̵�
        SceneManager.LoadScene("Station");
    }

    private void GoToCompete()
    {
        // ����ġ �̵�
        SceneManager.LoadScene("Compete");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
