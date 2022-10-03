using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetScript : MonoBehaviour
{
    private ConstantForce2D force;
    // Start is called before the first frame update
    void Start()
    {
        force = GetComponent<ConstantForce2D>();
        if(GetComponentInParent<UnitPlaced>().unitFlip == 1)
        {
            Debug.Log("flipk");
            force.relativeForce = -force.relativeForce;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
