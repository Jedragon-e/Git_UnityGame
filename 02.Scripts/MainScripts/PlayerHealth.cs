using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private UIManager um;

    private void Start()
    {
        base.SetState();
        um = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!photonView.IsMine)
            return;

        base.OnDamage(damage, hitPoint, hitNormal);
        um.UpdateUI(0, health, startingHealth);
        
        float x = Random.Range(-.5f, .5f);
        float y = Random.Range(-.5f, .5f);
        Vector3 v = new Vector3(x, 1, y);
        EffectMgr.instans.GetEffect(0, transform.position + v, Quaternion.identity);
    }

    public override void Die()
    {
        GetComponentInChildren<Animator>().SetTrigger("Die");
        GameObject.Find("Fogs").GetComponent<Fogs>().PlayerDie();
        base.Die();

    }
}
