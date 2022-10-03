using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDamage : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Unit")
        {
            //¸ßËÙ»¬µØ°å¿ÛÑª
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

            }
        }
    }
}
