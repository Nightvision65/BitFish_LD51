using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlaced : MonoBehaviour
{
    public int unitType, unitWidth, unitHeight, unitAngle, unitFlip;
    public bool canFlip;
    public List<Rigidbody2D> unitRbody;
    public List<UnitPlaced> jointUnit;
    public List<FixedJoint2D> mJoint;
    // Start is called before the first frame update
    void Start()
    {
        jointUnit.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetCurrentForce() > 1000f)
        {
            Debug.Log(GetCurrentForce());
        }
        if (GetCurrentTorque() > 100f)
        {
            Debug.Log(GetCurrentTorque());
        }
    }

    public bool hasJoint(UnitPlaced unit)
    {
        bool has = false;
        foreach (UnitPlaced junit in jointUnit)
        {
            if(junit == unit)
            {
                has = true;
                break;
            }
        }
        return has;
    }

    public float GetCurrentForce()
    {
        float maxForce = 0;
        foreach(FixedJoint2D joint in mJoint)
        {
            maxForce = Mathf.Max(joint.reactionForce.magnitude, maxForce);
        }
        return maxForce;
    }

    public float GetCurrentTorque()
    {
        float maxTorgue = 0;
        foreach (FixedJoint2D joint in mJoint)
        {
            maxTorgue = Mathf.Max(joint.reactionTorque, maxTorgue);
        }
        return maxTorgue;
    }

    //焊接零件（继承方法）
    public virtual void UnitCombine(int x, int y)
    {
        for (int dir = 1; dir <= 4; dir++)
        {
            UnitPlaced unitScript = UnitGrid.instance.GetUnitScript(x, y, dir);
            if (unitScript && !hasJoint(unitScript) && unitScript.unitType == 0)
            {
                jointUnit.Add(unitScript);
                FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = unitScript.unitRbody[0];
                mJoint.Add(joint);
            }
        }
    }
}
