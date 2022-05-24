using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyNatworkManager : MonoBehaviourPunCallbacks
{
    [Header("Page_01(서버연결)")]
    public InputField nameText;

    [Header("Page_02(로비)")]
    public GameObject page_02;
    public InputField roomInput;
    public Text playerNicname;
    public Button[] cellBtns;

    [Header("Page_03(룸)")]
    public GameObject page_03;
    public Text listText;
    public Text roomName;
    public Text[] chatText;
    public InputField chatInput;
    public Button startBtn;

    [Header("ETC")]
    public Text statusText;
    public PhotonView pv;

    private List<RoomInfo> myList = new List<RoomInfo>();

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Update()
    {
        statusText.text = PhotonNetwork.NetworkClientState.ToString();
    }

    public void Button_Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        page_02.SetActive(true);
        page_03.SetActive(false);
        PhotonNetwork.LocalPlayer.NickName = nameText.text;
        playerNicname.text = nameText.text + " 님 환영합니다.";
        myList.Clear();
    }

    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (page_02.activeSelf != false) page_02.SetActive(false);
        if (page_03.activeSelf != false) page_03.SetActive(false);
    }

    public void CreateRoom() => PhotonNetwork.CreateRoom(
        roomInput.text == ""
        ? "Room" + Random.Range(0, 100)
        : roomInput.text,
        new RoomOptions { MaxPlayers = 4 });

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public void LeaveRoom()
    {
        if (!PhotonNetwork.IsMasterClient)
            PhotonNetwork.LeaveRoom();
        else
            pv.RPC("DestroyRoom",RpcTarget.All);
    }

    [PunRPC]
    void DestroyRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message) { roomInput.text = ""; CreateRoom(); }

    public override void OnJoinRandomFailed(short returnCode, string message) { roomInput.text = ""; CreateRoom(); }

    public override void OnJoinedRoom()
    {
        page_03.SetActive(true);

        if (!PhotonNetwork.IsMasterClient)
            startBtn.interactable = false;

        RoomRenewal();
        chatInput.text = "";
        for (int i = 0; i < chatText.Length; i++) chatText[i].text = "";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + newPlayer.NickName + "님이 참가하셨습니다</color>");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다</color>");
    }

    public void MyRoomClick(int num)
    {
        PhotonNetwork.JoinRoom(myList[num].Name);
        MyListRenewal();
    }

    private void RoomRenewal()
    {
        listText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (i == 0)
                listText.text += "방장 : " + PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : "\n");
            else
                listText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : "\n");

        }
        roomName.text = PhotonNetwork.CurrentRoom.Name;
    }


    private void MyListRenewal()
    {
        for (int i = 0; i < cellBtns.Length; i++)
        {
            cellBtns[i].interactable = i < myList.Count;
            cellBtns[i].transform.GetChild(0).GetComponent<Text>().text
                = i < myList.Count ? myList[i].Name : "";
            cellBtns[i].transform.GetChild(1).GetComponent<Text>().text
                = i < myList.Count ? myList[i].PlayerCount + "/" + myList[i].MaxPlayers : "";
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }

    public void Send()
    {
        if (chatInput.text != "")
        {
            pv.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + chatInput.text);
            chatInput.text = "";
        }

        chatInput.Select();
        chatInput.ActivateInputField();
    }

    [PunRPC]
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < chatText.Length; i++)
        {
            if (chatText[i].text == "")
            {
                isInput = true;
                chatText[i].text = msg;
                break;
            }
        }
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < chatText.Length; i++)
                chatText[i - 1].text = chatText[i].text;
            chatText[chatText.Length - 1].text = msg;
        }
    }

    public void GameReadyBtn()
    {
        PhotonNetwork.LoadLevel(1);
    }

}