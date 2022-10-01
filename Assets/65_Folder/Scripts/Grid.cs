using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridUnit     //����Ԫ
{
    public int unitIndex, unitAngle;  //����ı�ź���ת
    public bool unitFlip, unitParent;   //���巭ת���Ƿ��Ǵ�����е�һԱ
    public int unitParentX, unitParentY;    //����ӵ��±�
    public Rigidbody2D unitRbody;   //ʵ��ĸ���
}
public class Grid : MonoBehaviour
{
    public Vector2 startPosition;    //�����������ʼλ��
    public int width, height, unitWidth;   //���񳤿�͸��ӿ��
    public GridUnit[,] gridUnit;   //����Ԫ

    // Start is called before the first frame update
    void Start()
    {
        gridUnit = new GridUnit[height, width];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //����x�������񲢷�������Ԫ���
    public int attachGridX(ref float x)
    {
        int t = (int)(x - transform.position.x) / unitWidth;
        x = t + transform.position.x;
        return t;
    }

    //����y�������񲢷�������Ԫ���
    public int attachGridY(ref float y)
    {
        int t = (int)(y - transform.position.y) / unitWidth;
        y = t + transform.position.y;
        return t;
    }

    //���óɹ�����¼������Ϣ
    public void placeUnit(int x, int y, int index, int angle, bool flip)
    {
        gridUnit[x, y].unitIndex = index;
        gridUnit[x, y].unitAngle = angle;
        gridUnit[x, y].unitFlip = flip;
    }
}
