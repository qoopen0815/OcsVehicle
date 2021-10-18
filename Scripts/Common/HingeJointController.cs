using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class HingeJointController : MonoBehaviour
{
    enum ControlType{ position, velocity }
    [SerializeField] private ControlType _type = ControlType.position;
    [SerializeField] private float _damper = 3.402823e+38f;
    [SerializeField] private float _force = 3.402823e+38f;

    HingeJoint joint;
    JointMotor motor;
    float input;

    public float Input { set => input = value; }

    // Start is called before the first frame update
    void Start()
    {
        joint = this.GetComponent<HingeJoint>();
        joint.useSpring = true;
        joint.useMotor = false;

        JointSpring spring = joint.spring;
        spring.damper = this._damper;
        joint.spring = spring;
        motor = joint.motor;
        motor.force = this._force;
        joint.motor = motor;
    }

    // Update is called once per frame
    void Update()
    {
        switch(_type)
        {
            case ControlType.position:
                PositionControl(input);
                break;
            case ControlType.velocity:
                VelocityControl(input);
                break;
        }
    }

    void PositionControl(float targetPos)
    {
        float now = this.transform.localRotation.eulerAngles.y;
    }

    void VelocityControl(float targetVel)
    {
        if (targetVel != 0)
        {
            joint.useMotor = true;
            motor.targetVelocity = targetVel;
            joint.motor = motor;
        }
        else
        {
            joint.useMotor = false;
            return;
        }
    }
}
