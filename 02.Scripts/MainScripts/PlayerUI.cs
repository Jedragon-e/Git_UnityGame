using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image[] uiBar;// 0.Energy 1.Health
    public UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("UImanager").GetComponent<UIManager>();
        for(int i =0; i<uiManager.images.Length; i++)
        {
            uiBar[i] = uiManager.images[i];
        }
    }

}
