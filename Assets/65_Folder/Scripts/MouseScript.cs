using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public float force;
    public List<Rigidbody2D> wheelUp, wheelDown;
    public List<FixedJoint2D> wheelJoint;
    private void OnDestroy()
    {
        foreach(Rigidbody2D rbody in wheelUp)
        {
            if (rbody)
            {
                rbody.AddForce(force * Vector2.up);
            }
        }
        foreach (Rigidbody2D rbody in wheelDown)
        {
            if (rbody)
            {
                rbody.AddForce(force * Vector2.down);
            }
        }
        foreach(FixedJoint2D joint in wheelJoint)
        {
            if (joint)
            {
                joint.enabled = false;
            }
        }
    }
}
