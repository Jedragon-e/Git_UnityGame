using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerCol : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
