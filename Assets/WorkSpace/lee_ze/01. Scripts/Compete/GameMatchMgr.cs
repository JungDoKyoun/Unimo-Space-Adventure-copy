using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMatchMgr : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject profileCanvas;

    [SerializeField]
    private GameObject matchingCanvas;

    [SerializeField]
    private GameObject playerCheckBox;

    private GameObject[] tempCheckBox;

    private Transform[] placeForCheckBox;

    private Button quickMatchButton;

    private Button stopMatchButton;

    private Image star;

    public static bool IsMatching { get; private set; } = false;

    public static bool IsMatched { get; private set; } = false;

    string gameVersion = "1";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true; // ������ ���� �ٲٸ� �ٸ� ��� Ŭ��鵵 ���� �ڵ�����Ǵ� �ɼ�
    }

    void Start()
    {
        ConnectToServer();

        StartCoroutine(SetQuickMatchButton());

        SetStopMatchButton();

        star = GameObject.Find("Matching Canvas").transform.Find("Matching Panel/Star").GetComponent<Image>();

        SetCanvas();

        SetStar(IsMatched);
    }

    private void ConnectToServer() // ���� ����
    {
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.GameVersion = gameVersion; //���۰� ���ÿ� �츮�� ������ �� Ŭ���̾�Ʈ ������ ����Ŵ

            PhotonNetwork.ConnectUsingSettings(); //�츮�� �����ص� �۾��̵� �������� ��� ������ �������� ���濡 ����
        }
    }

    #region Canvas

    private void SetCanvas()
    {
        profileCanvas.SetActive(!IsMatching);

        matchingCanvas.SetActive(IsMatching);

        IsMatching = !IsMatching;
    }

    private void SetStar(bool isMatched)
    {
        if (isMatched == false)
        {
            star.color = new Color32(150, 150, 150, 255);
        }
        else
        {
            star.color = Color.white;
        }
    }

    private void SetPlayerCheckBox()
    {
        if (placeForCheckBox == null)
        {
            placeForCheckBox = new Transform[PhotonNetwork.CurrentRoom.MaxPlayers];

            tempCheckBox = new GameObject[PhotonNetwork.CurrentRoom.MaxPlayers];

            for (int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
            {
                placeForCheckBox[i] = GameObject.Find("Matching Canvas").transform.Find($"Matching Panel/Player Checker/Player_{i + 1}");

                tempCheckBox[i] = Instantiate(playerCheckBox, placeForCheckBox[i].position, placeForCheckBox[i].rotation, placeForCheckBox[i]);
            }

            for (int j = 0; j < PhotonNetwork.CurrentRoom.PlayerCount; j++)
            {
                tempCheckBox[j].GetComponent<SetPlayerCheckBox>().SetCheckBox(true);
            }
        }
        else
        {
            for (int k = 0; k < PhotonNetwork.CurrentRoom.PlayerCount; k++)
            {
                tempCheckBox[k].GetComponent<SetPlayerCheckBox>().SetCheckBox(true);
            }
        }
    }

    private void QuickMatch()
    {
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 2
        };

        // ���� �� ���� �� �� ������ �ִ��ο� �� ���� �� ����
        PhotonNetwork.JoinRandomOrCreateRoom(

            roomName: null,

            roomOptions: options
        );
    }

    public void StopMatch()
    {
        PhotonNetwork.LeaveRoom();
    }

    private IEnumerator SetQuickMatchButton() // ����ġ ��ư�� �Ҵ�
    {
        quickMatchButton = GameObject.Find("Profile Canvas").transform.Find("Buttons/Quick Match Button").GetComponent<Button>();

        quickMatchButton.GetComponentInChildren<TextMeshProUGUI>().text = "���� ���� ��...";

        quickMatchButton.interactable = false;

        yield return new WaitUntil(() => PhotonNetwork.IsConnectedAndReady == true && PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer);

        quickMatchButton.GetComponentInChildren<TextMeshProUGUI>().text = "�� ��ġ";

        quickMatchButton.interactable = true;

        quickMatchButton.onClick.AddListener(() => SetCanvas());

        quickMatchButton.onClick.AddListener(() => QuickMatch());
    }

    private IEnumerator StartMatch()
    {
        if (PhotonNetwork.IsMasterClient == false)
        {
            yield break;
        }

        yield return new WaitForSeconds(3f); // 3�� �ڿ� ���� ����

        // TODO: �� �Ѿ�� ��ũ��Ʈ
        PhotonNetwork.LoadLevel("Test");
    }

    private void SetStopMatchButton()
    {
        stopMatchButton = GameObject.Find("Matching Canvas").transform.Find("Under Bar Panel/Stop Match Button").GetComponent<Button>();

        stopMatchButton.onClick.AddListener(() => StopMatch());

        stopMatchButton.onClick.AddListener(() => SetCanvas()); 
    }

    #endregion

    #region Photon

    public override void OnConnectedToMaster() // ���� ���� �Ǹ� ȣ��Ǵ� �̺�Ʈ �Լ�
    {
        Debug.Log(10);
    }

    public override void OnDisconnected(DisconnectCause cause) // ���� ���� �������� ȣ��Ǵ� �̺�Ʈ �Լ�
    {
        Debug.Log(11);

        Debug.Log(cause);
    }

    public override void OnJoinedRoom() // ���� �� ������� ȣ���
    {
        IsMatched = PhotonNetwork.CurrentRoom.PlayerCount < PhotonNetwork.CurrentRoom.MaxPlayers ? false : true;

        if (IsMatched == true)
        {
            stopMatchButton.interactable = false;
        }

        SetStar(IsMatched);

        SetPlayerCheckBox();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) // ���ο� ����� �濡 ������ �濡 �ִ� ������� ȣ���
    {
        // �� �Լ��� ȣ�� �ƴٴ� ���� ������ �����̰� ��Ī�� �ƴٴ� ��

        IsMatched = true;

        stopMatchButton.interactable = false;

        SetStar(IsMatched);

        SetPlayerCheckBox();

        // ���常 ȣ���ϴ� �޼���(���� ����)
        if (PhotonNetwork.IsMasterClient == true)
        {
            StartCoroutine(StartMatch());
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(13);

        Debug.Log(message);
    }

    public override void OnLeftRoom()
    {
        Debug.Log(14);
    }

    #endregion

    private void OnDestroy()
    {
        quickMatchButton.onClick.RemoveListener(() => SetCanvas());

        quickMatchButton.onClick.RemoveListener(() => QuickMatch());

        stopMatchButton.onClick.RemoveListener(() => StopMatch());

        stopMatchButton.onClick.RemoveListener(() => SetCanvas());
    }
}