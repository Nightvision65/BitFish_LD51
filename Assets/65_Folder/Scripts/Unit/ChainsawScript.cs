using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawScript : UnitScript
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

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Unit")
        {
            other.transform.GetComponentInParent<UnitPlaced>().TakeDamage(damage);
        }
    }
}
