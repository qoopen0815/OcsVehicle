using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle
{
    public class WheelLoader : Car
    {
        [Header("- WheelLoader SteerJoint Setting -")]
        [SerializeField] private Transform _steerJoint;
        [SerializeField] private float _smoothTime = 1.0f;
        [SerializeField] private float _maxSpeed = float.PositiveInfinity;
        private float _steeringVel = 0;
        
        [Header("- Work Setting -")]
        [SerializeField] private CylinderDrivenJoint _arm;
        [SerializeField] private CylinderDrivenJoint _bucket;
        [SerializeField] private float _controlSpeed;
        
        public float ArmInput { get; set; }
        public float BucketInput { get; set; }

        private void Start()
        {
            this._arm.Init();
            this._bucket.Init();
        }

        protected override void Update()
        {
            float angle = this._steerJoint.localRotation.eulerAngles.y;
            if (angle > 180) angle -= 360;
            angle = Mathf.SmoothDamp(angle,
                                      base.SteerInput * base.MaxSteerAngle,
                                      ref this._steeringVel, this._smoothTime, this._maxSpeed);
            this._steerJoint.localRotation = Quaternion.Euler(Vector3.up * angle);
            
            foreach (DriveTrain.Wheel driveTrain in base._driveTrains)
            {
                if (!ReverseGear) driveTrain.Drive(MaxAccelTorque * AccelInput, MaxBrakeTorque * BrakeInput);
                if (ReverseGear) driveTrain.Drive(MaxAccelTorque * -AccelInput, MaxBrakeTorque * BrakeInput);
                driveTrain.Steering(angle);
                driveTrain.UpdateMeshTransform();
            }

            foreach (Equipment.Winker winker in this._winkers)
            {
                winker.Blinky(base._winkerFrequency, base._winkerIntensity);
            }

            this._arm.RotateJoint(ArmInput * _controlSpeed);
            this._bucket.RotateJoint(BucketInput * _controlSpeed);
        }
    }
}
