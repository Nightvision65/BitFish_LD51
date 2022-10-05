using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 进度条 : MonoBehaviour
{
    public RectTransform 猫猫图标, 鼠鼠图标;
    public Transform 猫猫车, 鼠鼠车;
    public float 起始x, 终点x,路途长度, 鼠鼠距离倍数;
    public UnitPlaced Cat;
    float 总长x, 车起始x;
    // Start is called before the first frame update
    void Start()
    {
        总长x = 终点x - 起始x;
        车起始x = 猫猫车.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        var 猫猫x = 起始x + 总长x * ((猫猫车.position.x - 车起始x) / 路途长度);
        var 鼠鼠x = 猫猫x + 总长x * ((鼠鼠车.position.x - 猫猫车.position.x) / 路途长度) * 鼠鼠距离倍数;

        var p = 猫猫图标.anchoredPosition;
        p.x = 猫猫x;
        猫猫图标.anchoredPosition = p;

        p = 鼠鼠图标.anchoredPosition;
        p.x = 鼠鼠x;
        鼠鼠图标.anchoredPosition = p;
        if(鼠鼠车.position.x - 车起始x >= 路途长度)
        {
            Cat.TakeDamage(114514);
        }
    }
}
