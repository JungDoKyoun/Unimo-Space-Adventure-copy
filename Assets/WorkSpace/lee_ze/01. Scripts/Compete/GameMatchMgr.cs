using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameMatchMgr : MonoBehaviourPunCallbacks
{
    private Button quickMatchButton;

    string gameVersion = "1";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true; // 방장이 씬을 바꾸면 다른 모든 클라들도 씬이 자동변경되는 옵션
    }

    void Start()
    {
        SetQuickMatchButton();

        ConnectToServer();
    }

    private void ConnectToServer()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.GameVersion = gameVersion; //시작과 동시에 우리가 지정해 준 클라이언트 버전을 기억시킴

            PhotonNetwork.ConnectUsingSettings(); //우리가 기입해둔 앱아이디 정보들이 담긴 파일을 바탕으로 포톤에 연결
        }
    }

    private void SetQuickMatchButton() // 퀵매치 버튼에 할당
    {
        quickMatchButton = GameObject.Find("Profile Canvas").transform.Find("Buttons/Quick Match Button").GetComponent<Button>();

        quickMatchButton.onClick.AddListener(() => QuickMatch());
    }

    public void QuickMatch()
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

        Debug.Log("방 생성됨");
    }

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

    private void OnDestroy()
    {
        quickMatchButton.onClick.RemoveListener(() => QuickMatch());
    }
}