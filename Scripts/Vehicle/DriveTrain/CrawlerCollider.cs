using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle.DriveTrain
{
    [System.Serializable]
    public class CrawlerCollider
    {
        [SerializeField] private List<HingeJoint> _wheelJoints;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _motorTorque;

        private float _targetVelocity;
        private bool _reverseGear = false;
        public float TargetVelocity { set => _targetVelocity = value; }
        public bool ReverseGear { get => _reverseGear; set => _reverseGear = value; }

        private void Drive(float velocity)
        {
            if (this._reverseGear) velocity *= -1;
            foreach (HingeJoint wheel in this._wheelJoints)
            {

            }
        }

        public CrawlerCollider()
        {
            foreach (HingeJoint wheel in this._wheelJoints)
            {

            }
        }
    }
}
