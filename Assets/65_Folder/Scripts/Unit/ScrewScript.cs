using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewScript : UnitScript
{
    private float a = 0;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        a += Time.deltaTime * 50;
        float yscale = Mathf.Cos(a);
        transform.localScale = new Vector3(1, yscale, 1);
    }
}
