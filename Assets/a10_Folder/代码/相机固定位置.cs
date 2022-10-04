using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 相机固定位置 : MonoBehaviour
{
    public Transform 目标物体, Cat;
    public float 相机速度;
    Transform myTransform;
    float 差距;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        差距 = myTransform.position.x - 目标物体.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        目标物体 = FindFrontmostChariot();
        float tempx = 目标物体.position.x + 差距;
        if (tempx < myTransform.position.x)
        {
            tempx = Mathf.Max(-相机速度, tempx - myTransform.position.x);
        }
        else
        {
            tempx -= myTransform.position.x;
        }
        Vector3 pos = myTransform.position;
        pos.x += tempx;
        myTransform.position = pos;
    }

    //找到最前的载具
    Transform FindFrontmostChariot()
    {
        Transform chariot = Cat;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if (obj.GetComponent<Rigidbody2D>() && obj.transform.position.x > chariot.position.x && obj.GetComponentInParent<UnitPlaced>().team == 0) chariot = obj.transform;
        }
        return chariot;
    }
}