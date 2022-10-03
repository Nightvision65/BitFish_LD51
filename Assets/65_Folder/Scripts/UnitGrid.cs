using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGrid : MonoBehaviour
{
    public static UnitGrid instance;
    public int width, height;   //网格长宽
    public float gridWidth; //格子宽度
    public UnitPlaced[,] gridUnit;   //网格单元
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

    //物体贴附网格并返回网格单元编号
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

    //判断该网格是否为空
    public bool IsGridEmpty(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height && gridUnit[x, y] == null)
        {
            return true;
        }
        return false;
    }

    //获取临近网格实体脚本 0自己1右2上3左4下
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
    
    //铠甲合体！变身！
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
