using UnityEngine;
using UnityEngine.Networking;
//using UnityStandardAssets.Network;
using System.Collections;
using System.Collections.Generic;

public class MobaGameManager : NetworkBehaviour
{
    static public List<NetworkPlayerHelper> sPlayers = new List<NetworkPlayerHelper>();
    static public MobaGameManager sInstance = null;


    [Header("Gameplay")]
    public GameObject[] creepsPrefabs;

    [Space]
    protected bool _spawningCreeps = true;
    protected bool _running = true;

    void Awake()
    {
        sInstance = this;
    }

    void Start()
    {
        if (isServer)
        {
            StartCoroutine(CreepsCoroutine());
        }

        for (int i = 0; i < sPlayers.Count; ++i)
        {
            sPlayers[i].Init();
        }
    }

    [ServerCallback]
    void Update()
    {
        if (!_running || sPlayers.Count == 0)
            return;




        //   StartCoroutine(ReturnToLoby());

    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        foreach (GameObject obj in creepsPrefabs)
        {
            ClientScene.RegisterPrefab(obj);
        }
    }

    IEnumerator ReturnToLoby()
    {
        _running = false;
        yield return new WaitForSeconds(3.0f);
        Prototype.NetworkLobby.LobbyManager.s_Singleton.ServerReturnToLobby();
    }

    IEnumerator CreepsCoroutine()
    {
        const float TIME = 5.0f;

        while (_spawningCreeps)
        {
            yield return new WaitForSeconds(TIME);

        }
    }


    public IEnumerator WaitForRespawn(NetworkPlayerHelper hero)
    {
        yield return new WaitForSeconds(4.0f);

        hero.Respawn();
    }
}
