using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniEvent : MonoBehaviour
{
    public Transform createTr;
    public void OnAttack()
    {
        EffectMgr.instans.GetEffect(2, createTr.position, createTr.rotation);
        EffectMgr.instans.GetEffect(1, createTr.position, createTr.rotation);
    }
}
