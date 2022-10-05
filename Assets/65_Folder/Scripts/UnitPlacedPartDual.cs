using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacedPartDual : UnitPlaced
{
    private int count = 0;
    private UnitPlaced tempUnit;
    //焊接零件（继承方法）
    override public void UnitCombine(int x, int y)
    {
        isActived = true;
        UnitPlaced unitScript = UnitGrid.instance.GetUnitScript(x, y, unitAngle + 1);
        if (unitScript && unitScript.unitType == 0 && !hasJoint(unitScript))
        {
            count++;
            if (count > 1)//两个都连才是连
            {
                jointUnit.Add(unitScript);
                FixedJoint2D joint = unitRbody[0].gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = unitScript.unitRbody[0];
                mJoint.Add(joint);
                if (tempUnit != unitScript)
                {
                    jointUnit.Add(tempUnit);
                    joint = unitRbody[0].gameObject.AddComponent<FixedJoint2D>();
                    joint.connectedBody = tempUnit.unitRbody[0];
                    mJoint.Add(joint);
                }
            }
            else
            {
                tempUnit = unitScript;
            }
        }
    }
}
