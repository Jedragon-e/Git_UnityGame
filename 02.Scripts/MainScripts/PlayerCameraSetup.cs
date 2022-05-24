using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class PlayerCameraSetup : MonoBehaviourPun
{
    private void Start()
    {
        if (photonView.IsMine)
        {
            CinemachineFreeLook cam =
                FindObjectOfType<CinemachineFreeLook>();
            cam.Follow = transform;
            cam.LookAt = transform;
        }
    }
}


