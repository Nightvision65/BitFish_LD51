using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : UnitScript
{
    public GameObject bullet, fireEffect;
    public Transform firePosition, fireEffectPosition;
    public float fireScale, firerate, shotForce, deviation;
    public string shootSound;
    private float fireTimer = 0;
    private Rigidbody2D mRbody;
    private SpriteRenderer mSprite;
    // Start is called before the first frame update
    void Start()
    {
        mRbody = GetComponent<Rigidbody2D>();
        mSprite = GetComponent<SpriteRenderer>();
        fireTimer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (mSprite.isVisible)
        {
            WeaponFire();
        }
    }

    void WeaponFire()
    {
        if (fireTimer <= 0)
        {
            Global.instance.AudioPlay(shootSound);
            fireTimer = firerate;
            GameObject bul = Instantiate(bullet, firePosition.position, firePosition.rotation);
            Physics2D.IgnoreCollision(bul.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bul.GetComponent<Rigidbody2D>().velocity = mRbody.velocity;
            bul.GetComponent<Rigidbody2D>().AddForce(shotForce * (firePosition.right + (Vector3)Random.insideUnitCircle * deviation));
            mRbody.AddForceAtPosition(shotForce * firePosition.right * -1, firePosition.position);
            GameObject eff = Instantiate(fireEffect, fireEffectPosition.position, fireEffectPosition.rotation, transform);
            eff.transform.localScale = new Vector3(fireScale, fireScale, 1);
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }
}
