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
        PhotonNetwork.AutomaticallySyncScene = true; // ������ ���� �ٲٸ� �ٸ� ��� Ŭ��鵵 ���� �ڵ�����Ǵ� �ɼ�
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
            PhotonNetwork.GameVersion = gameVersion; //���۰� ���ÿ� �츮�� ������ �� Ŭ���̾�Ʈ ������ ����Ŵ

            PhotonNetwork.ConnectUsingSettings(); //�츮�� �����ص� �۾��̵� �������� ��� ������ �������� ���濡 ����
        }
    }

    private void SetQuickMatchButton() // ����ġ ��ư�� �Ҵ�
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

        // ���� �� ���� �� �� ������ �ִ��ο� �� ���� �� ����
        PhotonNetwork.JoinRandomOrCreateRoom(

            roomName: null,

            roomOptions: options
        );

        Debug.Log("�� ������");
    }

    public override void OnConnectedToMaster() // ���� ���� �Ǹ� ȣ��Ǵ� �̺�Ʈ �Լ�
    {
        Debug.Log(10);
    }

    public override void OnDisconnected(DisconnectCause cause) // ���� ���� �������� ȣ��Ǵ� �̺�Ʈ �Լ�
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