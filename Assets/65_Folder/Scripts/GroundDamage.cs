using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDamage : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Unit")
        {
            //���ٻ��ذ��Ѫ
            UnitPlaced script = other.gameObject.GetComponentInParent<UnitPlaced>();
            if ((script.unitType != 1 || script.isShut) && other.relativeVelocity.magnitude > 1)
            {
                script.TakeDamage(2);
            }
        }
    }
}
