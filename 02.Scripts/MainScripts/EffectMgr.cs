using UnityEngine;
using Photon.Pun;

public class EffectMgr : MonoBehaviourPun
{
    public static EffectMgr instans;

    [Header("Effect Prefab")]
    public GameObject[] effects;
    public int createCount;

    private GameObject[,] pools;
    private int[] index;

    private void Awake()
    {
        if (instans == null)
            instans = this;
        else
            Destroy(gameObject);

        pools = new GameObject[effects.Length, createCount];
        index = new int[effects.Length];

        for (int i = 0; i < effects.Length; i++)
        {
            for (int j = 0; j < createCount; j++)
            {
                pools[i, j] = Instantiate(effects[i], transform);
                pools[i, j].SetActive(false);
            }
        }
    }

    [PunRPC]
    public void GetEffect(int id, Vector3 pos, Quaternion rot)
    {
        pools[id, index[id]].transform.position = pos;
        pools[id, index[id]].transform.rotation = rot;
        pools[id, index[id]].SetActive(true);

        index[id]++;
        if (index[id] >= createCount)
            index[id] = 0;
    }
}
