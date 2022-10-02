using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 跟随鼠标 : MonoBehaviour
{
    public Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //获取鼠标位置
            var pos = Input.mousePosition;
            pos.z = Camera.main.transform.position.z;
            var mousePos = Camera.main.ScreenToWorldPoint(pos);

            Vector2 f = ((Vector2)mousePos - rig.position) * 10f;
            rig.AddForce(f);
        }
    }
}
