using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDamage : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Unit")
        {
            //高速滑地板扣血
            UnitPlaced script = other.gameObject.GetComponentInParent<UnitPlaced>();
            if (script.unitType != 1 || script.isShut)
            {
                if (other.relativeVelocity.magnitude > 1)
                {
                    script.TakeDamage(2);
                }
            }
            else
            {
                特效引用.instance.生成尘土(script.gameObject.transform.GetChild(1).position + new Vector3(0, -0.1f, 0), 0.1f, 0.1f, 1);
            }
        }
    }
}
