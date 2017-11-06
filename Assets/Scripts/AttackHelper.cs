using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AttackHelper : NetworkBehaviour
{
    const float AttackRadius = 1;
    const float AttackRange = 2;
    Animator _animator;

    public int Damage = 35;
    public float AttackSpeed = 0.5f;

    float _lastAttack;

    HealthHelper _healthHelper;
    // Use this for initialization
    void Start()
    {
        _healthHelper = GetComponent<HealthHelper>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        if (_healthHelper &&
            _healthHelper.IsDead)
            return;

        CmdAttack();
    }

    [Command]
    private void CmdAttack()
    {
        if (Time.time < _lastAttack + AttackSpeed)
            return;
        if (_animator)
            RpcAttackAnimation();

        Vector3 attackPosition = transform.position + transform.forward * AttackRange;
        Debug.DrawLine(transform.position, attackPosition, Color.red, 10);

        StartCoroutine(AttackCoroutine(attackPosition));

    }

    private IEnumerator AttackCoroutine(Vector3 attackPosition)
    {
        yield return new WaitForSeconds(AttackSpeed/2);

        Collider[] allCollider = Physics.OverlapSphere(attackPosition, AttackRadius);
        foreach (var item in allCollider)///Group
            if (item.GetComponent<HealthHelper>() && 
                item.GetComponent<HealthHelper>().Group != GetComponent<HealthHelper>().Group)
            {
                item.GetComponent<HealthHelper>().GetDamage(Damage);
            }

        _lastAttack = Time.time;
    }

    [ClientRpc]
    private void RpcAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
}
