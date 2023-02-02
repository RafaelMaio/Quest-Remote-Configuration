// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : Fix the object rotation - Disabling the capability of rotating it.
// SPECIAL NOTES: X
// ===============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    /// <summary>
    /// Obejct initial rotation
    /// </summary>
    Quaternion initialRot;

    /// <summary>
    /// Unity Start function.
    /// </summary>
    void Start()
    {
        initialRot = this.transform.rotation;    
    }

    /// <summary>
    /// Unity Update function.
    /// </summary>
    void Update()
    {
        this.transform.rotation = initialRot;
    }
}