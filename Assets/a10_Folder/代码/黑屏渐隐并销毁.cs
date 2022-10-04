using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 黑屏渐隐并销毁 : MonoBehaviour
{
    SpriteRenderer sr;
    float alpha;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        alpha = 1f;
        var c = sr.color;
        c.a = alpha;
        sr.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        alpha -= Time.deltaTime * 5f;
        var c = sr.color;
        c.a = alpha;
        sr.color = c;
        if (alpha <= 0) Destroy(gameObject);

    }
}
