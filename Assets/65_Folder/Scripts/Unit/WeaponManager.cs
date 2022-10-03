using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : UnitScript
{
    public GameObject bullet, fireEffect;
    public Transform firePosition;
    public float fireScale, firerate, shotForce;
    private float fireTimer = 0;
    private Rigidbody2D mRbody;
    // Start is called before the first frame update
    void Start()
    {
        mRbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponFire();
    }

    void WeaponFire()
    {
        if (fireTimer <= 0)
        {
            fireTimer = firerate;
            GameObject bul = Instantiate(bullet, firePosition.position, firePosition.rotation);
            Physics2D.IgnoreCollision(bul.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bul.GetComponent<Rigidbody2D>().velocity = mRbody.velocity;
            bul.GetComponent<Rigidbody2D>().AddForce(shotForce * firePosition.right);
            mRbody.AddForceAtPosition(shotForce * firePosition.right * -1, firePosition.position);
            GameObject eff = Instantiate(fireEffect, firePosition.position, firePosition.rotation, transform);
            eff.transform.localScale = new Vector3(fireScale, fireScale, 1);
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }
}
