using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    public class Backhoe : MonoBehaviour
    {
        [SerializeField] private JointInfo _body;
        [SerializeField] private JointInfo _boom;
        [SerializeField] private JointInfo _arm;
        [SerializeField] private JointInfo _end;

        private float _bodyJointInput;
        private float _boomJointInput;
        private float _armJointInput;
        private float _endJointInput;

        public float BodyJointInput { get => _bodyJointInput; set => _bodyJointInput = value; }
        public float BoomJointInput { get => _boomJointInput; set => _boomJointInput = value; }
        public float ArmJointInput { get => _armJointInput; set => _armJointInput = value; }
        public float EndJointInput { get => _endJointInput; set => _endJointInput = value; }

        private void Awake()
        {
            this._boom.joint.parent = this._body.joint;
            this._arm.joint.parent = this._boom.joint;
            this._end.joint.parent = this._arm.joint;
        }

        // Update is called once per frame
        void Update()
        {
            this._body.Rotate(this._bodyJointInput / 10);
            this._boom.Rotate(this._boomJointInput / 10);
            this._arm.Rotate(this._armJointInput / 10);
            this._end.Rotate(this._endJointInput / 10);
        }
    }
}
