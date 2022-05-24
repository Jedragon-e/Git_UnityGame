using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestManager : MonoBehaviourPun
{
    void Start()
    {
        Invoke("OOOOOOO", 2f);
    }

    void OOOOOOO()
    {
        print(PhotonNetwork.IsMasterClient);
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate("Monster_Skel", transform.position, transform.rotation);
    }
}
