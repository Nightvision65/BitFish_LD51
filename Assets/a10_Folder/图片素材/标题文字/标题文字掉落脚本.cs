using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 标题文字掉落脚本 : MonoBehaviour
{
    public float 起始掉落时间;
    RectTransform rectTransform;
    float time, setY;

    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = transform as RectTransform;
        var p = rectTransform.anchoredPosition;
        setY = p.y;
        p.y += 300;
        rectTransform.anchoredPosition = p;

        time = -0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time> 起始掉落时间)
        {
            var p = rectTransform.anchoredPosition;
            p.y += 0.1f * (setY - p.y);
            rectTransform.anchoredPosition = p;

        }
    }
}
