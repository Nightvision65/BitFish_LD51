using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGrid : MonoBehaviour
{
    public static UnitGrid instance;
    public int width, height;   //���񳤿�
    public float gridWidth; //���ӿ��
    public UnitPlaced[,] gridUnit;   //����Ԫ
    private SpriteRenderer mSprite;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gridUnit = new UnitPlaced[width, height];
        mSprite = GetComponent<SpriteRenderer>();
        mSprite.size = new Vector2(width * gridWidth, height * gridWidth);
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
            tx = Mathf.Clamp(tx, 0 - w, width - w - 1);
            ty = Mathf.Clamp(ty, 0 - h, height - h - 1);
            vec = new Vector2(transform.position.x + tx * gridWidth + gridWidth / 2, transform.position.y + ty * gridWidth + gridWidth / 2);
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
        if (x + dx >= 0 && x + dx < width && y + dy >= 0 && y + dy < height)
        {
            return gridUnit[x + dx, y + dy];
        }
        else
        {
            return null;
        }
    }
    
    //���׺��壡����
    public void ChariotCombine()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (gridUnit[i, j])
                {
                    gridUnit[i, j].UnitCombine(i, j);
                }
            }
        }
    }

    public void ClearGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                gridUnit[i, j] = null;
            }
        }
    }
}
