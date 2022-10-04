using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public GameObject bombEffect;
    public float damage, radius, power, effectScale;
    private Rigidbody2D mRbody;
    private Vector2 startVelocity;
    private SpriteRenderer mSprite;

    private void Start()
    {
        mRbody = GetComponent<Rigidbody2D>();
        startVelocity = mRbody.velocity;
        mSprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (!mSprite.isVisible) Destroy(gameObject);
        Vector3 nowdir = mRbody.velocity - startVelocity;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, nowdir);
        transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(bombEffect, transform.position, Quaternion.Euler(0, 0, 0));
        bombEffect.transform.localScale = new Vector3(effectScale, effectScale, 1);
        ExplosionDamage();
        Destroy(gameObject);
    }

    void ExplosionDamage()
    {
        Dictionary<UnitPlaced, float> unitScript = new Dictionary<UnitPlaced, float>();
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
            Vector2 dir = (rbody.transform.position - transform.position).normalized;
            float dis = Vector2.Distance(transform.position, rbody.ClosestPoint(transform.position));
            rbody.AddForce(power * dir * (radius - dis) / radius);
            UnitPlaced script = rbody.GetComponentInParent<UnitPlaced>();
            if (!unitScript.ContainsKey(script))
            {
                unitScript.Add(script, dis);
            }
            else
            {
                float tdis;
                unitScript.TryGetValue(script, out tdis);
                if (tdis > dis)
                {
                    unitScript.Remove(script);
                    unitScript.Add(script, dis);
                }
            }
        }
        foreach (UnitPlaced script in unitScript.Keys)
        {
            float tdis;
            unitScript.TryGetValue(script, out tdis);
            script.TakeDamage(damage * (radius - tdis) / radius);
        }
    }
}
