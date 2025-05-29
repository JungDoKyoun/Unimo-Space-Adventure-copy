using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameMatchMgr : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject profileCanvas;

    [SerializeField]
    private GameObject matchingCanvas;

    private Button quickMatchButton;

    private Button stopMatchButton;

    //private bool isMatching = false;

    public static bool IsMatching { get; private set; } = false;

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

        SetCanvas();
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

    private void SetIsMatching()
    {
        IsMatching = !IsMatching;
    }

    private void SetCanvas()
    {
        profileCanvas.SetActive(!IsMatching);

        matchingCanvas.SetActive(IsMatching);
    }

    private IEnumerator SetQuickMatchButton() // 퀵매치 버튼에 할당
    {
        quickMatchButton = GameObject.Find("Profile Canvas").transform.Find("Buttons/Quick Match Button").GetComponent<Button>();

        quickMatchButton.interactable = false;

        yield return new WaitUntil(() => PhotonNetwork.IsConnectedAndReady == true);

        quickMatchButton.interactable = true;

        quickMatchButton.onClick.AddListener(() => SetIsMatching());

        quickMatchButton.onClick.AddListener(() => SetCanvas());

        quickMatchButton.onClick.AddListener(() => QuickMatch());
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

    private void SetStopMatchButton()
    {
        stopMatchButton = GameObject.Find("Matching Canvas").transform.Find("Stop Match Button").GetComponent<Button>();

        stopMatchButton.onClick.AddListener(() => StopMatch());

        stopMatchButton.onClick.AddListener(() => SetIsMatching());

        stopMatchButton.onClick.AddListener(() => SetCanvas());
    }

    public void StopMatch()
    {
        PhotonNetwork.LeaveRoom();
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

    public override void OnJoinedRoom()
    {
        Debug.Log(12);
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
        quickMatchButton.onClick.RemoveListener(() => SetIsMatching());

        quickMatchButton.onClick.RemoveListener(() => SetCanvas());

        quickMatchButton.onClick.RemoveListener(() => QuickMatch());

        stopMatchButton.onClick.RemoveListener(() => StopMatch());

        stopMatchButton.onClick.RemoveListener(() => SetIsMatching());

        stopMatchButton.onClick.RemoveListener(() => SetCanvas());
    }
}