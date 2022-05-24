using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    //private Animator animator;
    //private PlayerInput playerInput;
    //private PlayerMove playerMove;
    ////private PlayerUI playerUI;

    //private float energy;
    //private readonly float energyMax = 100;
    //private float levelDelay;
    //private int energyLevel;

    //private void Start()
    //{
    //    animator = GetComponentInChildren<Animator>();
    //    playerInput = GetComponent<PlayerInput>();
    //    playerMove = GetComponent<PlayerMove>();
    //    //playerUI = GetComponent<PlayerUI>();

    //    energy = 0;
    //}

    //private void Update()
    //{
    //    //BasicAttack();
    //    //Casting();
    //}

    //private void Casting()
    //{
    //    if (playerInput.Mouse1)
    //    {
    //        if (energy < energyMax)
    //        {
    //            //if ((int)energy % 25 == 0 && (int)energy != 0.0f)
    //            //{
    //            //    levelDelay += Time.deltaTime;
    //            //    if (levelDelay > 1.0f)
    //            //    {
    //            //        //pr.Play();
    //            //        energy++;
    //            //    }
    //            //}
    //            //else
    //            //{
    //            //    if (levelDelay != 0)
    //            //        levelDelay = 0;

    //            //    energy += Time.deltaTime * 50.0f;
    //            //    if (energy >= energyMax)
    //            //        energy = energyMax;
    //            //}
    //            energy++;


    //        }
    //        else
    //        {
    //            levelDelay += Time.deltaTime;
    //            if (levelDelay > 1.5f )
    //            {
    //                //pr.Play();
    //                energy = 0;
    //                energyLevel++;
    //                levelDelay = 0;

    //            }
    //        }

    //    }
    //    else
    //    {
    //        energy = 0;
    //        energyLevel = 0;
    //    }
    //    //playerUI.UpdateUI(0, energy, energyMax);
    //    animator.SetBool("casting", playerInput.Mouse1);
    //}

    //private void BasicAttack()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        playerMove.LookForward();
    //        animator.SetTrigger("basicAttack");
    //    }
    //}
}
