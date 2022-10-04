using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDamage : MonoBehaviour
{
    public float damage, velocity;
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Unit")
        {
            //高速滑地板扣血
            UnitPlaced script = other.gameObject.GetComponentInParent<UnitPlaced>();
            if (script.unitType != 1 || script.isShut)
            {
                if (other.relativeVelocity.magnitude > velocity)
                {
                    script.TakeDamage(damage);
                }
            }
            else
            {
                特效引用.instance.生成尘土(script.gameObject.transform.GetChild(1).position + new Vector3(0, -0.1f, 0), 0.1f, 0.1f, 1);
            }
        }
    }
}
