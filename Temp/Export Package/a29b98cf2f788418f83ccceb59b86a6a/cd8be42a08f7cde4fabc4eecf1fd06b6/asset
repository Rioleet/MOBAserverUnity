using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHealthHelper : MonoBehaviour
{
    public HealthHelper Target { get; internal set; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
            return;

        if (GetComponent<Slider>().maxValue != Target.MaxHealth)
            GetComponent<Slider>().maxValue = Target.MaxHealth;

        if (GetComponent<Slider>().value != Target.Health)
            GetComponent<Slider>().value = Target.Health;

        Vector3 oldPos = new Vector3(Target.transform.position.x,
            Target.transform.position.y + Target.GetComponent<Collider>().bounds.size.y * 2
            , Target.transform.position.z);
        Vector3 newPos = Camera.main.WorldToScreenPoint(oldPos);

        GetComponent<RectTransform>().position = newPos;
    }
}
