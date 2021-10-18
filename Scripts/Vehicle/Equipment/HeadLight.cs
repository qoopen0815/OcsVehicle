using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocs.Vehicle.Equipment
{
    [System.Serializable]
    public class HeadLight
    {
        public Light leftLight;
        public Light rightLight;

        public void SwitchLight()
        {
            bool state = !leftLight.enabled;
            leftLight.enabled = state;
            rightLight.enabled = state;
        }
    }
}
