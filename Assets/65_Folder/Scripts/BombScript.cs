using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public GameObject bombEffect;
    public float damage, radius, power;
    void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(bombEffect, transform.position, Quaternion.Euler(0, 0, 0));
        ExplosionDamage();
        Destroy(gameObject);
    }

    void ExplosionDamage()
    {
        Debug.Log("exp");
        List<UnitPlaced> unitScript = new List<UnitPlaced>();
        List<Rigidbody2D> unitRbody = new List<Rigidbody2D>();
        List<Collider2D> unitCollider = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();
        Physics2D.OverlapCircle(transform.position, radius, filter, unitCollider);
        foreach(Collider2D collider in unitCollider)
        {
            if (collider.gameObject.tag == "Unit" && !unitRbody.Contains(collider.attachedRigidbody)) unitRbody.Add(collider.attachedRigidbody);
        }
        foreach(Rigidbody2D rbody in unitRbody)
        {
            float dis = Mathf.Min(Vector2.Distance(transform.position, rbody.transform.position), radius);
            rbody.AddForce(power * (rbody.transform.position - transform.position).normalized * (radius - dis) / radius);
            if (!unitScript.Contains(rbody.GetComponentInParent<UnitPlaced>()))
            {
                UnitPlaced script = rbody.GetComponentInParent<UnitPlaced>();
                unitScript.Add(script);
                script.TakeDamage(damage * (radius - dis) / radius);
                
            }
        }
    }
}
