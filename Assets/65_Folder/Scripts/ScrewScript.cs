using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewScript : MonoBehaviour
{
    private float a = 0;
    private Rigidbody2D mRbody;
    void Start()
    {
        mRbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!mRbody.IsSleeping())
        {
            a += Time.deltaTime * 50;
            float yscale = Mathf.Cos(a);
            transform.localScale = new Vector3(1, yscale, 1);
        }
    }
}
