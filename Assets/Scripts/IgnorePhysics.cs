// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : Ignore the collision between two objects with label 6 and label 7.
// SPECIAL NOTES: X
// ===============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePhysics : MonoBehaviour
{
    /// <summary>
    /// Unity Start function.
    /// </summary>
    void Start()
    {
        Physics.IgnoreLayerCollision(7, 6, true);
    }
}