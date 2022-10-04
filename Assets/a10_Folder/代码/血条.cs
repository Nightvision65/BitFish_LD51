using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 血条 : MonoBehaviour
{
    public UnitPlaced 车辆;
    RectTransform rectTransform;
    float 最大宽度;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = transform as RectTransform;
        最大宽度 = rectTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        var r = rectTransform.sizeDelta;
        r.x = 最大宽度 * (车辆.nowHP / 车辆.maxHP);
        rectTransform.sizeDelta = r;
    }
}
