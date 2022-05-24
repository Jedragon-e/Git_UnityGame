using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LivingEntity : MonoBehaviourPun, IDamageable
{
    public float startingHealth = 100f;
    public float health { get; protected set; }
    public bool dead { get; protected set; }

    protected virtual void SetState()
    {
        dead = false;
        health = startingHealth;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= damage;

        if (health <= 0 && !dead)
            Die();
    }

    public virtual void Die()
    {
        dead = true;
    }
}
