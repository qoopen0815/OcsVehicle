using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicles
{
    [RequireComponent(typeof(CarController))]
    public class TruckController : MonoBehaviour
    {
        [SerializeField] private Truck _vehicle;

        private Ocs.Input.VehicleInput _input;

        private void Awake()
        {
            this._input = new Ocs.Input.VehicleInput();
        }

        private void OnEnable() => this._input.Enable();

        private void OnDisable() => this._input.Disable();

        private void OnDestroy() => this._input.Dispose();

        void Update()
        {
            this._vehicle.TruckbedJointInput = this._input.CarDriver.Joint0.ReadValue<float>();
        }
    }
}
