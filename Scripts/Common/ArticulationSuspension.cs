using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticulationSuspension : MonoBehaviour
{
    [SerializeField] private ArticulationBody _hub;
    [SerializeField] private ArticulationBody _wheel;
    [SerializeField] private float _targetSpeed;

    private float _stiffness;
    private float _damping;
    
    ArticulationDrive hubDrive;
    ArticulationDrive wheelDrive;

    public float Stiffness { set => _stiffness = value; }
    public float Damping { set => _damping = value; }
    public float TargetSpeed { set => _targetSpeed = value; }
    public float CurrentSpeed { get => this._wheel.jointVelocity[0]; }

    // Start is called before the first frame update
    void Start()
    {
        wheelDrive = this._wheel.xDrive;
        hubDrive = this._hub.yDrive;
        hubDrive.stiffness = this._stiffness;
        hubDrive.damping = this._damping;
        this._hub.yDrive = hubDrive;
    }

    // Update is called once per frame
    void Update()
    {
        if (this._targetSpeed == 0 && Mathf.Abs(this._wheel.jointVelocity[0]) < 1.0f)
        {
            wheelDrive.stiffness = 500000;
            wheelDrive.damping = 1000;
            this._wheel.xDrive = wheelDrive;
        }
        else
        {
            wheelDrive.stiffness = 0;
            wheelDrive.damping = 100000;
            wheelDrive.targetVelocity = this._targetSpeed;
            this._wheel.xDrive = wheelDrive;
        }
    }
}
