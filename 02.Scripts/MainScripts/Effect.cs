using System.Collections;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public AudioClip ac;
    private readonly WaitForSeconds ws = new WaitForSeconds(1.5f);

    private void OnEnable()
    {
        GetComponent<AudioSource>().PlayOneShot(ac);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return ws;
        gameObject.SetActive(false);
    }

}
