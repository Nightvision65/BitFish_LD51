using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : UnitScript
{
    public float damage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Unit")
        {
            float velocity = other.relativeVelocity.magnitude;
            if (other.relativeVelocity.magnitude > 1)
            {
                other.transform.GetComponentInParent<UnitPlaced>().TakeDamage(damage * velocity);
                特效引用.instance.生成火花特效(transform.position, (transform.rotation.z + 180) % 360, 10);
            }
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Unit")
        {
            float velocity = other.relativeVelocity.magnitude;
            if (other.relativeVelocity.magnitude > 1)
            {
                other.transform.GetComponentInParent<UnitPlaced>().TakeDamage(damage / 6);
                特效引用.instance.生成火花特效(transform.position, (transform.rotation.z + 180) % 360, 1);
            }
        }
    }
}
