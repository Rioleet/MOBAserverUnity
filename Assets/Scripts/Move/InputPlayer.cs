using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(MoveNavMeshAgentSync))]
public class InputPlayer : NetworkBehaviour
{
    AttackHelper _attackHelper;
    // Use this for initialization
    void Start()
    {
        _attackHelper = GetComponent<AttackHelper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Ground")
                {
                    GetComponent<MoveNavMeshAgentSync>().Move(hit.point);
                }
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (_attackHelper != null)
                _attackHelper.Attack();
        }
    }
}
