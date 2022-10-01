using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridUnit     //网格单元
{
    public int unitIndex, unitAngle;  //物体的编号和旋转
    public bool unitFlip, unitParent;   //物体翻转，是否是大格子中的一员
    public int unitParentX, unitParentY;    //大格子的下标
    public Rigidbody2D unitRbody;   //实体的刚体
}
public class Grid : MonoBehaviour
{
    public Vector2 startPosition;    //创造的网格起始位置
    public int width, height, unitWidth;   //网格长宽和格子宽度
    public GridUnit[,] gridUnit;   //网格单元

    // Start is called before the first frame update
    void Start()
    {
        gridUnit = new GridUnit[height, width];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //物体x贴附网格并返回网格单元编号
    public int attachGridX(ref float x)
    {
        int t = (int)(x - transform.position.x) / unitWidth;
        x = t + transform.position.x;
        return t;
    }

    //物体y贴附网格并返回网格单元编号
    public int attachGridY(ref float y)
    {
        int t = (int)(y - transform.position.y) / unitWidth;
        y = t + transform.position.y;
        return t;
    }

    //放置成功，记录物体信息
    public void placeUnit(int x, int y, int index, int angle, bool flip)
    {
        gridUnit[x, y].unitIndex = index;
        gridUnit[x, y].unitAngle = angle;
        gridUnit[x, y].unitFlip = flip;
    }
}
