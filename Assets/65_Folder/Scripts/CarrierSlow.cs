using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierSlow : MonoBehaviour
{
    public GameObject carrier;
    public float motorCorrect, distance;
    private ���� driveScript;
    private float motorSpeed;
    private void Start()
    {
        driveScript = GetComponent<����>();
        motorSpeed = driveScript.�趨�ٶ�;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, carrier.transform.position) > distance)
        {
            if (driveScript.�趨�ٶ� == motorSpeed)
            {
                driveScript.�趨�ٶ� += motorCorrect;
            }
        }
        else
        {
            if (driveScript.�趨�ٶ� != motorSpeed)
            {
                driveScript.�趨�ٶ� = motorSpeed;
            }
        }
    }
}
