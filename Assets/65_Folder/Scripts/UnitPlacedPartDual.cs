using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacedPartDual : UnitPlaced
{
    private int count = 0;
    //����������̳з�����
    override public void UnitCombine(int x, int y)
    {
        UnitPlaced unitScript = UnitGrid.instance.GetUnitScript(x, y, unitAngle + 1);
        if (unitScript && !hasJoint(unitScript) && unitScript.unitType == 0)
        {
            jointUnit.Add(unitScript);
        }
        if (unitScript && unitScript.unitType == 0 && unitScript != this)
        {
            count++;
            if (count > 1)//��������������
            {
                FixedJoint2D joint = unitRbody[0].gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = unitScript.unitRbody[0];
                if (jointUnit[1] != unitScript)
                {
                    joint = unitRbody[0].gameObject.AddComponent<FixedJoint2D>();
                    joint.connectedBody = jointUnit[1].unitRbody[0];
                }
            }
        }
    }
}
