using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class NetworkPlayerHelper : NetworkBehaviour
{
    //Network syncvar
    [SyncVar]
    public Color color;
    [SyncVar]
    public string playerName;
    [SyncVar(hook = "ChangeGroup")]
	public int Group = 0;

    protected bool _canControl = true;

    public void ChangeGroup(int group)
    {
        if (GetComponent<HealthHelper>())
            GetComponent<HealthHelper>().Group = group;
    }
    public void Init()
    {
        if (_wasInit)
            return;

        Debug.Log("Init()");
        _wasInit = true;
    }

    protected bool _wasInit = false;

    void Awake()
    {
        MobaGameManager.sPlayers.Add(this);
    }

    void Start()
    {

        if (MobaGameManager.sInstance != null)
        {
            Init();
        }
    }



    void OnDestroy()
    {
        MobaGameManager.sPlayers.Remove(this);
    }

    [ClientCallback]
    void Update()
    {

    }


    [ClientCallback]
    void FixedUpdate()
    {
        if (!hasAuthority)
            return;

    }

    public void Respawn()
    {
        Debug.Log("Respawn()");
    }
}
