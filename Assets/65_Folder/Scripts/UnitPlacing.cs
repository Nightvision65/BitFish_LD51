using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacing : MonoBehaviour
{
    public bool placeable, canFlip;
    public int unitIndex, unitAngle, unitFlip;
    public Vector2Int gridPos;  //所处网格位置
    public Camera craftCam;
    private Color outGrid = new Color(1, 1, 1, 0), inGrid = new Color(1, 1, 1, 0.49f), unplaceable = new Color(1, 0.5f, 0.5f, 0.49f);
    private SpriteRenderer mSprite;
    private GameObject mUnit;
    private int unitWidth, unitHeight;
    private bool keyRotate, keyPlace, keyFlip;
    // Start is called before the first frame update
    void Start()
    {
        mUnit = Global.instance.chariotUnit[unitIndex];
        mSprite = GetComponent<SpriteRenderer>();
        mSprite.sprite = mUnit.GetComponent<SpriteRenderer>().sprite;
        unitWidth = mUnit.GetComponent<UnitPlaced>().unitWidth;
        unitHeight = mUnit.GetComponent<UnitPlaced>().unitHeight;
        canFlip = mUnit.GetComponent<UnitPlaced>().canFlip;
    }

    // Update is called once per frame
    void Update()
    {
        keyRotate = Input.GetButtonDown("Rotate");
        keyPlace = Input.GetButtonDown("Place");
        keyFlip = Input.GetButtonDown("Flip");
        RotateUnit();
        FlipUnit();
        SetPosition();
        PlaceUnit();
    }

    //判断能否放置该物体
    bool CanPlace()
    {
        bool can = true;
        for (int i = Mathf.Min(0, unitWidth); i <= Mathf.Max(0, unitWidth); i++)
        {
            for (int j = Mathf.Min(0, unitHeight); j <= Mathf.Max(0, unitHeight); j++)
            {
                if (!UnitGrid.instance.IsGridEmpty(gridPos.x + i, gridPos.y + j))
                {
                    can = false;
                    break;
                }
            }
        }
        return can;
    }

    //旋转物体角度
    void RotateUnit()
    {
        if (keyRotate)
        {
            int t = unitWidth;
            unitWidth = -unitHeight;
            unitHeight = t;
            unitAngle = (unitAngle + 1) % 4;
            transform.Rotate(new Vector3(0, 0, 90));
        }
    }

    //翻转物体
    void FlipUnit()
    {
        if (keyFlip && canFlip)
        {
            if (unitAngle % 2 == 0)
            {
                unitWidth = -unitWidth;
            }
            else
            {
                unitHeight = -unitHeight;
            }
            transform.localScale += new Vector3(0, -2 * transform.localScale.y, 0);
            unitFlip = (unitFlip + 1) % 2;
        }
    }


    //附着网格位置
    void SetPosition()
    {
        Vector3 mousePos = craftCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 tpos = mousePos;
        Vector2Int fvec = new Vector2Int(-1, -1);
        gridPos = UnitGrid.instance.AttachGrid(ref tpos, unitWidth, unitHeight);
        if (gridPos != fvec)
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
    void PlaceUnit()
    {
        if (keyPlace && placeable)
        {
            GameObject placedUnit = Instantiate(mUnit, transform.position, transform.rotation, GameObject.FindGameObjectWithTag("Chariot").transform);
            placedUnit.transform.localScale = transform.localScale;
            UnitPlaced unitScript = placedUnit.GetComponent<UnitPlaced>();
            unitScript.unitWidth = unitWidth;
            unitScript.unitHeight = unitHeight;
            unitScript.unitAngle = unitAngle;
            unitScript.unitFlip = unitFlip;
            for (int i = Mathf.Min(0, unitWidth); i <= Mathf.Max(0, unitWidth); i++)
            {
                for (int j = Mathf.Min(0, unitHeight); j <= Mathf.Max(0, unitHeight); j++)
                {
                    UnitGrid.instance.gridUnit[gridPos.x + i, gridPos.y + j] = unitScript;
                }
            }
            Destroy(gameObject);
        }
    }
}
