using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffect : UnitScript
{
    public float smokeTime;
    private float smokeTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (smokeTimer < 0)
        {
            smokeTimer = smokeTime;
            特效引用.instance.生成喷烟(transform.position, Vector2.SignedAngle(Vector2.right, transform.right), 1);
        }
        else
        {
            smokeTimer -= Time.deltaTime;
        }
    }
}
