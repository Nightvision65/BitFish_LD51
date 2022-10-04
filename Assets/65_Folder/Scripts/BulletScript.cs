using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage;
    private Rigidbody2D mRbody;

    private void Start()
    {
        mRbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector3 nowdir = mRbody.velocity;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, nowdir);
        transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
      //  Debug.Log(other.relativeVelocity.magnitude);
        if (other.gameObject.tag == "Unit")
        {
            other.gameObject.GetComponentInParent<UnitPlaced>().TakeDamage(damage);
            特效引用.instance.生成火花特效(transform.position, (Vector2.SignedAngle(Vector2.right, transform.right) + 180) % 360, 5);
        }
        Destroy(gameObject);
    }
}
