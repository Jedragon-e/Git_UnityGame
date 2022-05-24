using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSenser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<MonsterAI>().OnSenser(other);
        GetComponent<SphereCollider>().enabled = false;
    }

    public void OnSenser()
    {
        GetComponent<SphereCollider>().enabled = true;

    }
}
