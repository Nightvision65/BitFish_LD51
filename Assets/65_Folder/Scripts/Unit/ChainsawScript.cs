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
            特效引用.instance.生成火花特效(transform.position, Vector2.SignedAngle(Vector2.right, transform.right), 2);
            特效引用.instance.生成火花特效(transform.position, (Vector2.SignedAngle(Vector2.right, transform.right) + 180) % 360, 2);
            Global.instance.AudioPlay("chainsaw_hit");
        }
    }
}
