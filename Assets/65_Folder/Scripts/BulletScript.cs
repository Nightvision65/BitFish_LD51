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
            特效引用.instance.生成火花特效(transform.position, (transform.rotation.z + 180) % 360, 5);
        }
        Destroy(gameObject, 0.1f);
    }
}
