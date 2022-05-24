using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NatworkManager : MonoBehaviourPunCallbacks
{
    private Transform startingTr;
    private void Start()
    {
        startingTr = transform.GetChild(0);
        Vector3 randomSpawnPos = Random.insideUnitSphere * 5.0f;
        randomSpawnPos.y = 1.0f;
        PhotonNetwork.Instantiate("Player_v1", startingTr.position + randomSpawnPos, Quaternion.identity);

        //print(PhotonNetwork.PlayerList.Length);
    }


    //#region 로비완성후 메인신 테스트용 룸생성 코드
    //private void Awake()
    //{
    //    PhotonNetwork.ConnectUsingSettings();
    //    startingTr = transform.GetChild(0);
    //}

    //public override void OnConnectedToMaster()
    //{
    //    PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, null);
    //}

    //public override void OnJoinedRoom()
    //{
    //    Vector3 randomSpawnPos = Random.insideUnitSphere * 5.0f;
    //    randomSpawnPos.y = 1.0f;

    //    PhotonNetwork.Instantiate("Player_v1", startingTr.position + randomSpawnPos, Quaternion.identity);
    //}
    //#endregion
}
