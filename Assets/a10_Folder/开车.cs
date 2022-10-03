using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 开车 : MonoBehaviour
{
    public Rigidbody2D rig;
    public float 设定速度;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 f = Vector2.right * (设定速度 - rig.velocity.magnitude) * 600f;
        rig.AddForce(f);
    }
}
