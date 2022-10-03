using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 背景移动 : MonoBehaviour
{
    public GameObject 跟随物体;
    public float 移动比例 = 0.8f;
    float startX, startOtherX;

    Transform myTransform, otherTransform;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        otherTransform = 跟随物体.transform;
        startOtherX = otherTransform.position.x;
        startX = myTransform.position.x;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = myTransform.position;
        pos.x = startX + 移动比例 * (otherTransform.position.x - startOtherX);
        myTransform.position = pos;
    }
}
