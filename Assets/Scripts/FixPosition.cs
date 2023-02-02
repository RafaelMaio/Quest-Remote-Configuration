// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : Fix the object position - Disabling the capability of translating it.
// SPECIAL NOTES: X
// ===============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPosition : MonoBehaviour
{
    /// <summary>
    /// Object initial position.
    /// </summary>
    Vector3 initialPos;
    
    /// <summary>
    /// Unity Start function
    /// </summary>
    void Start()
    {
        initialPos = this.transform.position;
    }

    /// <summary>
    /// Unity Update function.
    /// </summary>
    void Update()
    {
        this.transform.position = initialPos;
    }
}
