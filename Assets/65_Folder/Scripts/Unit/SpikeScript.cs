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
            }
        }
    }
}
