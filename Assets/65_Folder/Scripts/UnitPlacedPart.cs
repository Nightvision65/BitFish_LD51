using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacedPart : UnitPlaced
{
    //����������̳з�����
    override public void UnitCombine(int x, int y)
    {
        isActived = true;
        UnitPlaced unitScript = UnitGrid.instance.GetUnitScript(x, y, unitAngle + 1);
        if (unitScript && !hasJoint(unitScript) && unitScript.unitType == 0)
        {
            jointUnit.Add(unitScript);
            FixedJoint2D joint = unitRbody[0].gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = unitScript.unitRbody[0];
            mJoint.Add(joint);
        }
    }
}
