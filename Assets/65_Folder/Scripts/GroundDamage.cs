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
            //���ٻ��ذ��Ѫ
            UnitPlaced script = other.gameObject.GetComponentInParent<UnitPlaced>();
            if (script.unitType != 1 || script.isShut)
            {
                if (other.relativeVelocity.magnitude > velocity)
                {
                    script.TakeDamage(damage * ((script.unitHeight + 1) * (script.unitWidth + 1)));
                }
            }
            else
            {
                ��Ч����.instance.���ɳ���(script.gameObject.transform.GetChild(1).position + new Vector3(0, -0.1f, 0), 0.1f, 0.1f, 1);
            }
        }
    }
}
