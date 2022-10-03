using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage, hitVel;
    void OnCollisionEnter2D(Collision2D other)
    {
      //  Debug.Log(other.relativeVelocity.magnitude);
        if (other.relativeVelocity.magnitude >= hitVel && other.gameObject.tag == "Unit")
        {
            other.gameObject.GetComponentInParent<UnitPlaced>().TakeDamage(damage);
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude < hitVel) Destroy(gameObject);
    }
}
