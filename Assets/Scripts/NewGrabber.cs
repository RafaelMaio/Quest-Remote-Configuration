// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : New script for grabbing objects.
// SPECIAL NOTES: X
// ===============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGrabber : MonoBehaviour
{
    /// <summary>
    /// Right controller.
    /// </summary>
    private GameObject rightHand;

    /// <summary>
    /// Line model game object.
    /// </summary>
    private GameObject modelParent;

    /// <summary>
    /// Objects colliding with the controller.
    /// </summary>
    private List<Collider> collidedObjects = new List<Collider>();

    /// <summary>
    /// Tag required to be grabbable by the script.
    /// </summary>
    private string desiredTag = "hand";

    /// <summary>
    /// Button triggered
    /// </summary>
    private bool triggered = false;

    /// <summary>
    /// Unity Start function.
    /// </summary>
    void Start()
    {
        rightHand = GameObject.Find("CustomHandRight");
        modelParent = GameObject.Find("ModelParent");
    }

    /// <summary>
    /// Unity OnTriggerEnter function.
    /// Adds the object to the list of objects that the controller is colliding with.
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
    /// Adds the object to the list of objects that the controller is colliding with.
    /// </summary>
    /// <param name="col">Object collider.</param>
    void OnTriggerStay(Collider col)
    {
        OnTriggerEnter(col); //same as enter
    }

    /// <summary>
    /// Unity OnTriggerExit function.
    /// Removes the object from the list of objects that the controller is colliding with as its no longer colliding.
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
    /// Make the colliders children of the hand for copying its movement.
    /// </summary>
    void Update()
    {
        if(collidedObjects.Count == 0)
        {
            if (!this.transform.parent.Equals(modelParent.transform))
            {
                this.transform.parent = modelParent.transform;
            }
        }
        if (collidedObjects.Count == 1)
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) == 1)
            {
                triggered = true;
                if (!this.transform.parent.Equals(rightHand.transform))
                {
                    this.transform.SetParent(rightHand.transform);
                }
            }
            else
            {
                if (!this.transform.parent.Equals(modelParent.transform))
                {
                    this.transform.parent = modelParent.transform;
                }
                if (triggered)
                {
                    collidedObjects.Clear();
                    triggered = false;
                }
            }
        }
    }
}