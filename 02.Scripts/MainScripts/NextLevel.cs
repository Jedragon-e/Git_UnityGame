using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private float downRage;

    public void OpenFog()
    {
        downRage = transform.position.y - 6f;
        StartCoroutine(StartUnLook());
    }

    IEnumerator StartUnLook()
    {
        //print(transform.position.y);
        while (transform.position.y >= downRage)
        {
            transform.Translate(transform.up * 1.5f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
