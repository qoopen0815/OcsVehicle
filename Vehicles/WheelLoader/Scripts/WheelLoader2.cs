using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    [System.Serializable]
    public class Suspension
    {
        public ArticulationSuspension leftSuspension;
        public ArticulationSuspension rightSuspension;
        public float stiffness = 500000.0f;
        public float damping = 1000.0f;

        public float motorTorque = 100;
        public float brakeTorque = 100;

        public void Setup()
        {
            leftSuspension.Stiffness = stiffness;
            leftSuspension.Damping = damping;
            rightSuspension.Stiffness = stiffness;
            rightSuspension.Damping = damping;
        }

        public void Drive(float motor, float brake)
        {
            leftSuspension.TargetSpeed = motor * motorTorque;
            rightSuspension.TargetSpeed = motor * motorTorque;

            if (brake != 0)
            {
                leftSuspension.TargetSpeed = -leftSuspension.CurrentSpeed * brake * brakeTorque;
                rightSuspension.TargetSpeed = -leftSuspension.CurrentSpeed * brake * brakeTorque;
            }
        }
    }

    [System.Serializable]
    public class Cylinder
    {
        public CylinderJoint cylinder;
        public float lowerLimit;
        public float upperLimit;
        public float scale;
    }

    public class WheelLoader2 : MonoBehaviour
    {
        [Header("- Suspension Setting -")]
        [SerializeField] private List<Suspension> _suspension;
        [SerializeField] private float _steeringAngle = 60.0f;
        [SerializeField] private ArticulationBody _steeringBody;
        [SerializeField] private Transform _steeringObj;

        [Header("- Work Setting -")]
        [SerializeField] private ArticulationBody _boom;
        [SerializeField] private ArticulationBody _bucket;
        [SerializeField] private Cylinder _boomCylinder;
        [SerializeField] private Cylinder _bucketCylinder;

        [SerializeField, Range(0, 50)] public float debug;

        ArticulationDrive steeringDrive;
        ArticulationDrive boomDrive;
        ArticulationDrive bucketDrive;

        private float _driveInput;
        private float _brakeInput;
        private float _heightInput;
        private float _rotateInput;

        public float DriveInput { set => _driveInput = value; }
        public float BrakeInput { set => _brakeInput = value; }
        public float SteerInput { set => steeringDrive.targetVelocity = value * this._steeringAngle; }
        public float HeightInput { set => _heightInput = value; }
        public float RotateInput { set => _rotateInput = value; }

        private void Awake()
        {
            foreach (Suspension suspension in this._suspension)
            {
                suspension.Setup();
            }
        }

        private void Start()
        {
            steeringDrive = this._steeringBody.xDrive;
            boomDrive = this._boom.xDrive;
            bucketDrive = this._bucket.xDrive;
        }

        private void Update()
        {
            // Drive Control
            foreach (Suspension suspension in this._suspension)
            {
                suspension.Drive(this._driveInput, this._brakeInput);
            }

            // Steering Control
            this._steeringBody.xDrive = steeringDrive;
            this._steeringObj.localRotation = Quaternion.Euler(Vector3.up * this._steeringBody.jointPosition[0] * Mathf.Rad2Deg * 10);

            // Work Control
            float input, length;
            input = this._heightInput * 50.0f;
            length = this._boomCylinder.cylinder.Length;
            if (length >= this._boomCylinder.upperLimit * this._boomCylinder.scale)
            {
                if (input < 0) boomDrive.targetVelocity = input;
                else boomDrive.targetVelocity = 0;
            }
            else if (length <= this._boomCylinder.lowerLimit * this._boomCylinder.scale)
            {
                if (input > 0) boomDrive.targetVelocity = input;
                else boomDrive.targetVelocity = 0;
            }
            else
            {
                boomDrive.targetVelocity = input;
            }

            input = (this._rotateInput - this._heightInput) * 50.0f;
            length = this._bucketCylinder.cylinder.Length;
            if (length >= this._bucketCylinder.upperLimit * this._bucketCylinder.scale)
            {
                if (input > 0) bucketDrive.targetVelocity = input;
                else bucketDrive.targetVelocity = 0;
            }
            else if (length <= this._bucketCylinder.lowerLimit * this._bucketCylinder.scale)
            {
                if (input < 0) bucketDrive.targetVelocity = input;
                else bucketDrive.targetVelocity = 0;
            }
            else
            {
                bucketDrive.targetVelocity = input;
            }
            this._boom.xDrive = boomDrive;
            this._bucket.xDrive = bucketDrive;
        }
    }
}
