using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    [System.Serializable]
    public class JointInfo
    {
        public Transform joint;
        public enum Axis
        {
            X_axis,
            Y_axis,
            Z_axis
        }
        public Axis jointAxis = Axis.Z_axis;
        public float limitMax;
        public float limitMin;

        public void Rotate(float delta)
        {
            Vector3 tmp = joint.localRotation.eulerAngles;
            float val = tmp[(int)jointAxis] + delta;
            val = Mathf.Repeat(val + 180, 360) - 180;
            val = System.Math.Min(val, limitMax);
            val = System.Math.Max(val, limitMin);
            tmp[(int)jointAxis] = Mathf.Repeat(val, 360);
            joint.localRotation = Quaternion.Euler(tmp);
        }
    }
}
