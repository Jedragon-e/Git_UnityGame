using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instans;
    public Image[] images;

    public GameObject escPanel;
    public Slider s1;
    public Slider s2;

    public Text eventText;
    private bool playEventText = false;
    public float eventTextTime;
    private readonly WaitForSeconds ws = new WaitForSeconds(3f);

    private void Awake()
    {
        if (instans == null)
            instans = this;
        else
            Destroy(gameObject);
    }

    public void UpdateUI(int type, float velue, float velueMax)
    {
        images[type].fillAmount = velue / velueMax;
    }

    public void SetESC(bool active)
    {
        escPanel.SetActive(active);
    }

    public void SetSound()
    {
        Camera.main.GetComponent<AudioSource>().volume = s1.value;
    }

    public void SetSensitivity()
    {

    }

    public void ViewMessage(string str)
    {
        if (playEventText)
            return;

        eventText.text = str;
        eventText.color = Color.white;
        StartCoroutine(StartMessage());
    }

    IEnumerator StartMessage()
    {
        playEventText = true;
        Color c = eventText.color;
        float m_time = 0;

        yield return ws;
        while (c.a > 0f)
        {
            m_time += Time.deltaTime / eventTextTime;
            c.a = Mathf.Lerp(1, 0, m_time);
            eventText.color = c;
            yield return null;
        }

        playEventText = false;

    }
}
