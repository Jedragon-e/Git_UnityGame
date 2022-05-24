using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviourPun
{
    //GetCompornent
    private PlayerInput input;
    private Rigidbody rigid;
    private Transform model;
    private Animator animator;

    //movement
    private Vector3 direction;
    private Vector3 direction_delay;
    private float speed , speedMax;
    private readonly float WALK = 2f;
    private readonly float RUN = 10f;
    private readonly float ACCEL = 20f;

    //rotate
    private Transform lookAtTr;
    private float angleX, angleY;
    [Header("Vertical Max")]
    public float angleMax;
    //private readonly float maxY = 20;
    //private readonly float minY = -20;

    [Header("Camera Positon")]
    public float distance;  //타겟 대상과의 거리
    public float height;    //타겟 대상과의 높이
    public float offset;    //타겟 좌표의 오프셋

    private Camera mainCamera;
    private WaitForSeconds ws = new WaitForSeconds(1.1f);
    private bool isAttack = false;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        model = GetComponentInChildren<Animator>().transform;
        animator = model.GetComponent<Animator>();

        mainCamera = Camera.main;
        lookAtTr = transform.parent.Find("LookAt");

        lookAtTr.position = transform.position;
        mainCamera.transform.position =
        lookAtTr.position - (lookAtTr.forward * distance) + (lookAtTr.up * height);
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;
        animator.SetFloat("Speed", speed);
    }

    public void Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            speed = 0;
            LookForward();
            animator.SetTrigger("Attack");
            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay()
    {
        yield return ws;
        isAttack = false;
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;


        if (!isAttack)
            MoveSystem();
        RotSystem();
    }

    private void MoveSystem()
    {
        direction = (transform.forward * input.MoveY)
            + (transform.right * input.MoveX);

        if (direction.normalized != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0, lookAtTr.eulerAngles.y, 0);
            model.rotation = Quaternion.Slerp(model.rotation,
                Quaternion.LookRotation(direction), 20f * Time.fixedDeltaTime);

            speedMax = input.LeftShift ? RUN : WALK;
            if (speed < speedMax)
            {
                speed += Time.fixedDeltaTime * ACCEL;
                if (speed > speedMax)
                    speed = speedMax;
            }
            else if (speed > speedMax)
            {
                speed -= Time.fixedDeltaTime * ACCEL;
                if (speed < speedMax)
                    speed = speedMax;
            }
            direction_delay = direction.normalized;
            rigid.velocity = new Vector3(direction.normalized.x * speed,
                rigid.velocity.y,
                direction.normalized.z * speed);

        }
        else
        {
            speed -= Time.fixedDeltaTime * ACCEL;
            if (speed < 0.0f)
                speed = 0.0f;
            rigid.velocity = new Vector3(direction_delay.x * speed,
                 rigid.velocity.y,
                 direction_delay.z * speed);
        }
    }

    private void RotSystem()
    {
        lookAtTr.position = transform.position;
        mainCamera.transform.position = lookAtTr.position - (lookAtTr.forward * distance) + (lookAtTr.up * height);

        angleX += input.MouseX * Time.fixedDeltaTime;
        angleY -= input.MouseY * Time.fixedDeltaTime;
        angleY = Mathf.Clamp(angleY, -angleMax, angleMax);
        lookAtTr.rotation = Quaternion.Euler(angleY, angleX, 0);

        mainCamera.transform.LookAt(lookAtTr.position + new Vector3(0f, offset, 0f));
    }

    public void LookForward()
    {
        float yawCamera = lookAtTr.rotation.eulerAngles.y;
        if (transform.rotation.y != yawCamera)
        {
            transform.rotation = Quaternion.Euler(0, yawCamera, 0);
            model.rotation = transform.rotation;
        }
    }
}
