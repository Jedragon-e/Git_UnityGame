using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAniEvent : MonoBehaviour
{
    private BoxCollider attack;
    private void Start()
    {
        attack = GetComponent<BoxCollider>();
        attack.enabled = false;
    }

    public void OnAttack()
    {
        attack.enabled = true;
    }
    public void OffAttack()
    {
        attack.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LivingEntity le = other.GetComponent<PlayerHealth>();
            le.OnDamage(1, Vector3.zero, Vector3.zero);
        }
    }
}
