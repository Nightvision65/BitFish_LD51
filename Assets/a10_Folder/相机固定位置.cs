using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 相机固定位置 : MonoBehaviour
{
    public Transform 目标物体;
    Transform myTransform;
    float 差距;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        差距 = myTransform.position.x - 目标物体.position.x  ;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = myTransform.position;
        pos.x = 目标物体.position.x + 差距;
        myTransform.position = pos;
    }
}
