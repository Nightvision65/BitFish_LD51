using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDamage : MonoBehaviour
{
    public float damage, velocity;
    public int team;
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Unit")
        {
            //���ٻ��ذ��Ѫ
            UnitPlaced script = other.gameObject.GetComponentInParent<UnitPlaced>();
            if (script.unitType != 1 || script.isShut || damage > 8)
            {
                if (other.relativeVelocity.magnitude > velocity)
                {
                    if (team == 1 && script.team == 0)
                    {
                        script.TakeDamage(damage / 5);
                    }
                    else
                    {
                        script.TakeDamage(damage);
                        if (script.unitWidth > 0 || script.unitHeight > 0) script.TakeDamage(damage);
                    }
                }
            }
            else
            {
                ��Ч����.instance.���ɳ���(script.gameObject.transform.GetChild(1).position + new Vector3(0, -0.1f, 0), 0.1f, 0.1f, 1);
            }
        }
    }
}
