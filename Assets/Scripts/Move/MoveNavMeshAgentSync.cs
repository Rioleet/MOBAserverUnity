using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class MoveNavMeshAgentSync : NetworkBehaviour
{
    [Header("Network Settings")]
    public float SyncRate = 3;

    [SyncVar]
    Vector3 _syncPosition;
    [SyncVar]
    Quaternion _syncRotation;

    Vector3 _target;
    Animator _anim;
    UnityEngine.AI.NavMeshAgent _navMeshAgent;


    private bool _isStop;

    public bool IsStop
    {
        get { return _isStop; }
        set
        {

            _isStop = value;
            if (_isStop)
                _navMeshAgent.Stop();
            else
                _navMeshAgent.Resume();
        }
    }
    HealthHelper _healthHelper;
    // Use this for initialization
    void Start()
    {
        _healthHelper = GetComponent<HealthHelper>();

        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }
    [ClientCallback]
    void FixedUpdate()
    {
        if (_healthHelper &&
            _healthHelper.IsDead)
            return;

        ///Синкаем позицию
        transform.position = Vector3.Lerp(transform.position, _syncPosition, Time.deltaTime * SyncRate);
        transform.rotation = Quaternion.Lerp(transform.rotation, _syncRotation, Time.deltaTime * SyncRate);
    }

    [ServerCallback]
    // Update is called once per frame
    void Update()
    {
        if (_healthHelper &&
            _healthHelper.IsDead)
            return;

        _navMeshAgent.SetDestination(_target);
        Animation();

        if (_syncPosition == transform.position &&
            _syncRotation == transform.rotation)
            ///Если обект оставался на месте
            return;

        _syncPosition = transform.position;
        _syncRotation = transform.rotation;
    }

    private void Animation()
    {
        bool idle = false;
        if (Vector3.Distance(transform.position, _target) < _navMeshAgent.stoppingDistance)
            idle = true;
        if (_anim && _anim.GetBool("Idle") != idle)
            _anim.SetBool("Idle", idle);
    }

    /// <summary>
    /// Когда нужно двигатся к цели
    /// </summary>
    public void Move(Vector3 target)
    {
        CmdMove(target);
    }
    [Command]
    public void CmdMove(Vector3 target)
    {
        _target = target;
    }
}
