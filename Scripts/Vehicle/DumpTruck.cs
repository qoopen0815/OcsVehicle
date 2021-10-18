using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle
{
    public class DumpTruck : Car
    {
        [Header("- Work Setting -")]
        [SerializeField] private JointInfo _work;
        [SerializeField] private float _controlSpeed;
        
        public float WorkJointInput { get; set; }

        protected override void Update()
        {
            base.Update();
            this._work.Rotate(WorkJointInput * this._controlSpeed);
        }
    }
}
