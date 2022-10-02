using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacing : MonoBehaviour
{
    public bool placeable;
    public int unitIndex, unitAngle, unitFlip;
    private Color outGrid = new Color(1, 1, 1, 0), inGrid = new Color(1, 1, 1, 0.49f), unplaceable = new Color(0.5f, 1, 1, 0.49f);
    private SpriteRenderer mSprite;
    private int unitWidth, unitHeight;
    // Start is called before the first frame update
    void Start()
    {
        mSprite = GetComponent<SpriteRenderer>();
        mSprite.sprite = Global.instance.vehicleUnit[unitIndex].GetComponent<SpriteRenderer>().sprite;
        unitWidth = Global.instance.vehicleUnit[unitIndex].GetComponent<UnitPlaced>().unitWidth;
        unitHeight = Global.instance.vehicleUnit[unitIndex].GetComponent<UnitPlaced>().unitHeight;
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
    }

    //判断能否放置该物体
    bool CanPlace()
    {
        bool can = true;
        for(int i = 0; i < unitWidth; i++)
        {
            for (int j = 0; j < unitHeight; j++) {
                if (!UnitGrid.instance.IsGridEmpty(i, j)) can = false;
            }
        }
        return can;
    }

    //附着网格位置
    void SetPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 tpos = mousePos;
        Vector2Int gridpos, fvec = new Vector2Int(-1, -1);
        gridpos = UnitGrid.instance.AttachGrid(ref tpos, unitWidth, unitHeight);
        if (gridpos != fvec)
        {
            transform.position = tpos;
            if (CanPlace())
            {
                placeable = true;
                mSprite.color = inGrid;
            }
            else
            {
                placeable = false;
                mSprite.color = unplaceable;
            }
        }
        else
        {
            placeable = false;
            mSprite.color = outGrid;
        }
    }

    //放置物体
}
