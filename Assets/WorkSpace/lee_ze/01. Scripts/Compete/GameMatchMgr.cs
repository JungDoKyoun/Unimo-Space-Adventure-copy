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
        PhotonNetwork.AutomaticallySyncScene = true; // 방장이 씬을 바꾸면 다른 모든 클라들도 씬이 자동변경되는 옵션
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

    private void ConnectToServer() // 서버 접속
    {
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.GameVersion = gameVersion; //시작과 동시에 우리가 지정해 준 클라이언트 버전을 기억시킴

            PhotonNetwork.ConnectUsingSettings(); //우리가 기입해둔 앱아이디 정보들이 담긴 파일을 바탕으로 포톤에 연결
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

        // 랜덤 방 참가 및 방 없으면 최대인원 두 명인 방 생성
        PhotonNetwork.JoinRandomOrCreateRoom(

            roomName: null,

            roomOptions: options
        );
    }

    public void StopMatch()
    {
        PhotonNetwork.LeaveRoom();
    }

    private IEnumerator SetQuickMatchButton() // 퀵매치 버튼에 할당
    {
        quickMatchButton = GameObject.Find("Profile Canvas").transform.Find("Buttons/Quick Match Button").GetComponent<Button>();

        quickMatchButton.GetComponentInChildren<TextMeshProUGUI>().text = "서버 접속 중...";

        quickMatchButton.interactable = false;

        yield return new WaitUntil(() => PhotonNetwork.IsConnectedAndReady == true && PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer);

        quickMatchButton.GetComponentInChildren<TextMeshProUGUI>().text = "퀵 매치";

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

        yield return new WaitForSeconds(3f); // 3초 뒤에 게임 시작

        // TODO: 씬 넘어가는 스크립트
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

    public override void OnConnectedToMaster() // 서버 연결 되면 호출되는 이벤트 함수
    {
        Debug.Log(10);
    }

    public override void OnDisconnected(DisconnectCause cause) // 서버 연결 끊어지면 호출되는 이벤트 함수
    {
        Debug.Log(11);

        Debug.Log(cause);
    }

    public override void OnJoinedRoom() // 입장 한 사람에게 호출됨
    {
        IsMatched = PhotonNetwork.CurrentRoom.PlayerCount < PhotonNetwork.CurrentRoom.MaxPlayers ? false : true;

        if (IsMatched == true)
        {
            stopMatchButton.interactable = false;
        }

        SetStar(IsMatched);

        SetPlayerCheckBox();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) // 새로운 사람이 방에 들어오면 방에 있는 사람에게 호출됨
    {
        // 이 함수가 호출 됐다는 뜻은 본인이 방장이고 매칭이 됐다는 뜻

        IsMatched = true;

        stopMatchButton.interactable = false;

        SetStar(IsMatched);

        SetPlayerCheckBox();

        // 방장만 호출하는 메서드(게임 시작)
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