using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 轮子转动 : MonoBehaviour
{

    public float 转动角速度;
    Transform myTransform;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        var angle =  myTransform.eulerAngles;
        angle.z += 转动角速度 * Time.deltaTime ;
        myTransform.eulerAngles = angle;
    }
}
