// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : New script for scaling objects.
// SPECIAL NOTES: Identical behaviour to newGrabber.
// ===============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scaler : MonoBehaviour
{
    /// <summary>
    /// Objects colliding with the controller.
    /// </summary>
    private List<Collider> collidedObjects = new List<Collider>();

    /// <summary>
    /// Tag required to be grabbable by the script.
    /// </summary>
    private string desiredTag = "hand";

    /// <summary>
    /// Left controller.
    /// </summary>
    private GameObject leftHand;

    /// <summary>
    /// Right controller.
    /// </summary>
    private GameObject rightHand;

    /// <summary>
    /// Current distance between the controllers.
    /// </summary>
    private float distance;

    /// <summary>
    /// Previous distance between the controllers.
    /// </summary>
    private float previousDistance;

    /// <summary>
    /// Unity Start function.
    /// </summary>
    void Start()
    {
        leftHand = GameObject.Find("CustomHandLeft");
        rightHand = GameObject.Find("CustomHandRight");
    }

    /// <summary>
    /// Unity OnTriggerEnter function.
    /// Adds the object to the list of objects that both controllers are colliding with.
    /// </summary>
    /// <param name="col">Object collider.</param>
    void OnTriggerEnter(Collider col)
    {
        if (!collidedObjects.Contains(col) && col.tag == desiredTag)
        {
            collidedObjects.Add(col);
        }
    }

    /// <summary>
    /// Unity OnTriggerStay function.
    /// Adds the object to the list of objects that both controllers are colliding with.
    /// </summary>
    /// <param name="col">Object collider.</param>
    void OnTriggerStay(Collider col)
    {
        OnTriggerEnter(col); //same as enter
    }

    /// <summary>
    /// Unity OnTriggerExit function.
    /// Removes the object from the list of objects that both controllers are colliding with as its no longer colliding..
    /// </summary>
    /// <param name="col">Object collider.</param>
    void OnTriggerExit(Collider col)
    {
        if (collidedObjects.Contains(col) && col.tag == desiredTag)
        {
            collidedObjects.Remove(col);
        }
    }

    /// <summary>
    /// Unity Update function.
    /// Use the distance between the controllers for handling the object scaling.
    /// </summary>
    void Update()
    {
        if (collidedObjects.Count == 0)
        {
            distance = 0;
            previousDistance = 0;
        }
        else if (collidedObjects.Count == 1)
        {
            distance = 0;
            previousDistance = 0;
        }
        else if (collidedObjects.Count == 2)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.Touch) == 1 &&
                OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch) == 1)
            {
                distance = Vector3.Distance(rightHand.transform.position, leftHand.transform.position);
                if (previousDistance != 0)
                {
                    this.transform.localScale = this.transform.localScale * (1 + (distance - previousDistance) * 4);
                }
                previousDistance = distance;
            }
        }
    }
}