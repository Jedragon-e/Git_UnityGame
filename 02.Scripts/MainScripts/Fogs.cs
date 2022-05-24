using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fogs : MonoBehaviour
{

    public int level1_m_Count;
    public int level2_m_Count;

    private int playerCount;

    private NextLevel nl1;
    private NextLevel nl2;

    private void Start()
    {
        nl1 = transform.GetChild(0).GetComponent<NextLevel>();
        nl2 = transform.GetChild(1).GetComponent<NextLevel>();
        playerCount = PhotonNetwork.PlayerList.Length;
    }

    public void MonsterDie(int type)
    {
        if (type == 1)
        {
            level1_m_Count--;
            if (level1_m_Count <= 0)
            {
                UIManager.instans.ViewMessage("첫번째 막혔던 안개가 사라졌습니다. 다음 맵으로 이동하세요.");
                nl1.OpenFog();
            }

        }
        else if(type == 2)
        {
            level2_m_Count--;
            if (level2_m_Count <= 0)
            {
                UIManager.instans.ViewMessage("두번째 막혔던 안개가 사라졌습니다. 다음 맵으로 이동하세요.");
                nl2.OpenFog();
            }
        }
        else
        {
            UIManager.instans.ViewMessage("게임을 클리어하셨습니다. 다음 스테이지는 DLC를 구입하여 주세요.");
            StartCoroutine(Clear());
        }

    }

    public void PlayerDie()
    {
        playerCount--;
        if(playerCount == 0)
            UIManager.instans.ViewMessage("모든 플레이어가 사망하였습니다.");
        StartCoroutine(Clear());
    }

    IEnumerator Clear()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        yield return new WaitForSeconds(6f);
        UIManager.instans.ViewMessage("잠시후 로비로 돌아갑니다.");
        yield return new WaitForSeconds(5f);
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    }
}
