using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Effect_Projectile : MonoBehaviour
{
    public float Damage;
    private void OnEnable()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(transform.forward * 2000f);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Senser"))
        {
            print(other.name);

            gameObject.SetActive(false);
            EffectMgr.instans.GetEffect(3, transform.position, transform.localRotation
                * Quaternion.Euler(0,90,90));


            LivingEntity le = other.GetComponent<LivingEntity>();
            if (le != null)
                le.photonView.RPC("OnDamage", RpcTarget.All, Damage, Vector3.zero, Vector3.zero);
        }


        
    }
}
