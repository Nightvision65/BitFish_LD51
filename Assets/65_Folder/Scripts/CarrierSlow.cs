using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierSlow : MonoBehaviour
{
    public GameObject carrier;
    public float motorCorrect, distance;
    private 开车 driveScript;
    private float motorSpeed;
    private void Start()
    {
        driveScript = GetComponent<开车>();
        motorSpeed = driveScript.设定速度;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, carrier.transform.position) > distance)
        {
            if (driveScript.设定速度 == motorSpeed)
            {
                driveScript.设定速度 += motorCorrect;
            }
        }
        else
        {
            if (driveScript.设定速度 != motorSpeed)
            {
                driveScript.设定速度 = motorSpeed;
            }
        }
    }
}
