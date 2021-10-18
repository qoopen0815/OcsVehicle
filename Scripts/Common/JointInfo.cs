using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JointInfo
{
    // —ñ‹“Œ^‚Ì’è‹`
    public enum Axis { x, y, z }
    public Axis axis = Axis.x;
    private Vector3 _axis;

    public Transform transform;
    public float min;
    public float max;

    public JointInfo()
    {
        switch (axis)
        {
            case Axis.x:
                this._axis = Vector3.right;
                break;
            case Axis.y:
                this._axis = Vector3.up;
                break;
            case Axis.z:
                this._axis = Vector3.forward;
                break;
        }
    }

    public void Rotate(float speed)
    {
        transform.Rotate(this._axis * speed);
    }
}
