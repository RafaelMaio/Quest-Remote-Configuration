// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : Open/Close the hand menu.
// SPECIAL NOTES: X
// ===============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuScript : MonoBehaviour
{
    /// <summary>
    /// Hand menu.
    /// </summary>
    public GameObject mainMenu;

    /// <summary>
    /// Unity Update function.
    /// </summary>
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Three))
        {
            if (!mainMenu.activeSelf)
            {
                mainMenu.SetActive(true);
            }
        }
        else
        {
            if (mainMenu.activeSelf)
            {
                mainMenu.SetActive(false);
            }
        }
    }
}