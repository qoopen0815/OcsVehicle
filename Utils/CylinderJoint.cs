using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderJoint : MonoBehaviour
{
    [SerializeField] private Transform _rod;
    [SerializeField] private Transform _refJoint;
    [SerializeField] private float _scale = 1.0f;

    private float _length;

    public float Length { get => _length; }

    // Update is called once per frame
    void Update()
    {
        Quaternion look = Quaternion.LookRotation(this._refJoint.position - this.transform.position);
        this.transform.rotation = look;

        this._length = (this.transform.position - this._refJoint.position).magnitude;
        this._rod.localPosition = Vector3.forward * this._length * this._scale;
    }
}
