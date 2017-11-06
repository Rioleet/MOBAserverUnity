using UnityEngine;
using UnityEngine.Networking;

public class HealthHelper : NetworkBehaviour
{
    [SyncVar]
    public int MaxHealth = 100;
    [SyncVar]
    public int Health = 100;
    [SyncVar]
    public int Group = 0;



    [SyncVar]
    private bool _isDead;
    public bool IsDead
    {
        get { return _isDead; }
        set { _isDead = value; }
    }
    Animator _animator;
    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        GameObject slider = Instantiate<GameObject>(Resources.Load<GameObject>("HealthSlider"));
        slider.transform.SetParent(GameObject.Find("UICanvas").transform);

        slider.GetComponent<UIHealthHelper>().Target = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Server]
    public void GetDamage(int damage) /// Кто убил?
    {
        int newHealth = Health - damage;

        if (newHealth <= 0)
        {
            Health = 0;
            IsDead = true;

            if (_animator)
                _animator.SetBool("Dead", true);
        }
        else
            Health = newHealth;
    }
}
