using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInput : MonoBehaviourPun
{
    private PlayerHealth ph;

    //Axis 이름
    private const string H = "Horizontal";
    private const string V = "Vertical";
    private const string X = "Mouse X";
    private const string Y = "Mouse Y";

    //마우스 민감도
    [Header("sensirivity (default 200)")]
    public float SENSITIVITY;
    private bool isRock;

    public float MoveX { get; private set; }
    public float MoveY { get; private set; }

    public float MouseX { get; private set; }
    public float MouseY { get; private set; }

    public bool LeftShift { get; private set; }

    private void Start()
    {
        ph = GetComponent<PlayerHealth>();

        isRock = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!photonView.IsMine || ph.dead)
            return;

        if (!isRock)
        {
            MoveX = Input.GetAxis(H);
            MoveY = Input.GetAxis(V);
            MouseX = Input.GetAxis(X) * SENSITIVITY * 1.5f;
            MouseY = Input.GetAxis(Y) * SENSITIVITY;

            //왼쪽 시프트
            if (Input.GetKeyDown(KeyCode.LeftShift))
                LeftShift = true;
            if (Input.GetKeyUp(KeyCode.LeftShift))
                LeftShift = false;

            if (Input.GetMouseButtonDown(0))
                GetComponent<PlayerMove>().Attack();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeButton();
        }
    }

    private void ResetInput()
    {
        MouseX = MouseY = MoveX = MoveY = 0;
        LeftShift = false;
    }

    private void EscapeButton()
    {
        ResetInput();
        isRock = !isRock;
        GameObject.Find("UIManager").GetComponent<UIManager>().SetESC(isRock);
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
