using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGrid : MonoBehaviour
{
    public static UnitGrid instance;
    public Vector2 startPosition;    //�����������ʼλ��
    public int width, height;   //���񳤿�
    public float gridWidth; //���ӿ��
    public UnitPlaced[,] gridUnit;   //����Ԫ
    private SpriteRenderer mSprite;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gridUnit = new UnitPlaced[width, height];
        mSprite = GetComponent<SpriteRenderer>();
        mSprite.size = new Vector2(width * 0.32f, height * 0.32f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�����������񲢷�������Ԫ���
    public Vector2Int AttachGrid(ref Vector2 vec, int w, int h)
    {
        int tx = Mathf.FloorToInt((vec.x - transform.position.x) / gridWidth);
        int ty = Mathf.FloorToInt((vec.y - transform.position.y) / gridWidth);
        if (tx >= 0 && tx < width && ty >= 0 && ty < height)
        {
            tx = Mathf.Clamp(tx, 0, width - w);
            ty = Mathf.Clamp(ty, 0, height - h);
            vec = new Vector2(transform.position.x + tx * gridWidth, transform.position.y + ty * gridWidth);
            return new Vector2Int(tx, ty);
        }
        else
        {
            return new Vector2Int(-1, -1);
        }
    }

    //�жϸ������Ƿ�Ϊ��
    public bool IsGridEmpty(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height && gridUnit[x, y] == null)
        {
            return true;
        }
        return false;
    }

    //��ȡ�ٽ�����ʵ��ű� 0�Լ�1��2��3��4��
    public UnitPlaced GetUnitScript(int x, int y, int dir)
    {
        int dx = 0, dy = 0;
        if (dir > 0)
        {
            dx = Mathf.RoundToInt(Mathf.Cos((dir - 1) * Mathf.PI / 2));
            dy = Mathf.RoundToInt(Mathf.Sin((dir - 1) * Mathf.PI / 2));
        }
        if (x + dx >= 0 && x + dx < width && y + dy >= 0 && y + dy <= height)
        {
            return gridUnit[x + dx, y + dy];
        }
        else
        {
            return null;
        }
    }
}
