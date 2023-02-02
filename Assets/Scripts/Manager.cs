// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : Application main script.
// SPECIAL NOTES: X
// ===============================

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    /// <summary>
    /// Line model game object.
    /// </summary>
    public GameObject lineBoschParent;

    /// <summary>
    /// Communication with the Usability Test script.
    /// </summary>
    public UsabilityTest usabilityTest;

    /// <summary>
    /// Objects palced.
    /// </summary>
    private List<GameObject> goS = new List<GameObject>();

    /// <summary>
    /// Blue cube prefab.
    /// </summary>
    public GameObject cubePrefab;

    /// <summary>
    /// QR code prefab.
    /// </summary>
    public GameObject qrcodePrefab;

    /// <summary>
    /// QRCode object.
    /// </summary>
    private GameObject qrcode;

    /// <summary>
    /// Information balloon object - For the component association.
    /// </summary>
    private GameObject balloon;

    /// <summary>
    /// Information balloon prefab.
    /// </summary>
    public GameObject balloonPrefab;

    /// <summary>
    /// Component information.
    /// </summary>
    private List<string> boxPieceInformation = new List<string>();

    /// <summary>
    /// Green cube prefab.
    /// </summary>
    public GameObject cubeDonePrefab;

    /// <summary>
    /// Component associated - Flag for confirmation.
    /// </summary>
    private bool confirmedFlag = false;

    /// <summary>
    /// Training cube object.
    /// </summary>
    private GameObject initialCube;

    /// <summary>
    /// Unity Start function.
    /// Places the training cube.
    /// </summary>
    public void Start()
    {
        initialCube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity);
        initialCube.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        initialCube.transform.SetParent(lineBoschParent.transform, false);
    }

    /// <summary>
    /// Start button pressed - Start test.
    /// </summary>
    public void startPressed()
    {
        Destroy(initialCube);

        usabilityTest.placeQRCode();

        qrcode = Instantiate(qrcodePrefab, Vector3.zero, Quaternion.identity);
        qrcode.transform.SetParent(lineBoschParent.transform, false);

        usabilityTest.setTime();
    }

    /// <summary>
    /// Place the next cube object.
    /// </summary>
    public void next()
    {
        if (goS.Count == 0)
        {
            usabilityTest.stopTime(qrcode, "aux_go");
        }
        if (confirmedFlag || goS.Count == 0)
        {
            usabilityTest.placeNextObject(goS.Count);
            GameObject cube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity);
            cube.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            // Usability new Tests:
            if (goS.Count < 2)
            {
                cube.GetComponent<FixPosition>().enabled = false;
                cube.GetComponent<FixRotation>().enabled = true;
                cube.GetComponent<Scaler>().enabled = false;
            }
            else if (goS.Count < 4)
            {
                cube.GetComponent<FixPosition>().enabled = true;
                cube.GetComponent<FixRotation>().enabled = false;
                cube.GetComponent<Scaler>().enabled = false;
                if (goS.Count == 2)
                {
                    cube.transform.position = new Vector3(-1.8f, 0.4f, -0.1f);
                }
                else
                {
                    cube.transform.position = new Vector3(4.95f, 0.14f, -1.3f);
                }
            }
            else if (goS.Count < 6)
            {
                cube.GetComponent<FixPosition>().enabled = true;
                cube.GetComponent<FixRotation>().enabled = true;
                cube.GetComponent<Scaler>().enabled = true;
                if (goS.Count == 4)
                {
                    cube.transform.position = new Vector3(-1.6f, -0.43f, 2.91f);
                }
                else
                {
                    cube.transform.position = new Vector3(-1.60f, -0.17f, 1.81f);
                }
            }
            goS.Add(cube);
            cube.transform.SetParent(lineBoschParent.transform, false);
            confirmedFlag = false;
        }
    }

    /// <summary>
    /// Remove current object.
    /// </summary>
    public void removeObject()
    {
        GameObject toRemove = goS[goS.Count - 1];
        goS.RemoveAt(goS.Count - 1);
        Destroy(toRemove);
    }

    /// <summary>
    /// Change from object placement to the component association and vice-versa.
    /// </summary>
    /// <param name="associationOn">Is the association on?</param>
    public void changeGoToAssociation(bool associationOn)
    {
        if (associationOn)
        {
            balloon = Instantiate(balloonPrefab, lineBoschParent.transform);
            balloon.transform.position = goS[goS.Count - 1].transform.position;
            balloon.transform.rotation = goS[goS.Count - 1].transform.rotation;
            balloon.transform.localScale = goS[goS.Count - 1].transform.localScale * 6;
            if (boxPieceInformation.Count > 0)
            {
                balloon.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = boxPieceInformation[0];
                balloon.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = boxPieceInformation[1];
                balloon.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = boxPieceInformation[2];
            }
            GameObject toRemove = goS[goS.Count - 1];
            goS.RemoveAt(goS.Count - 1);
            Destroy(toRemove);
        }
        else
        {
            if (balloon != null)
            {
                if (!confirmedFlag)
                {
                    GameObject newGo = Instantiate(cubePrefab, lineBoschParent.transform);
                    newGo.transform.position = balloon.transform.position;
                    newGo.transform.rotation = Quaternion.Euler(balloon.GetComponent<Orientate>().getInitialRotation());
                    newGo.transform.localScale = balloon.transform.localScale / 6;
                    //newGo.transform.SetParent(lineBoschParent.transform, false);
                    Destroy(balloon);
                    goS.Add(newGo);
                }
            }
        }
    }

    /// <summary>
    /// Component associated.
    /// </summary>
    /// <param name="clickedButton">Button from the component list pressed.</param>
    public void componentClicked(GameObject clickedButton)
    {
        boxPieceInformation = new List<string> { 
            StaticFunctions.FindChildByRecursion(clickedButton.transform, "ReferenceText").GetComponent<TMP_Text>().text,
            StaticFunctions.FindChildByRecursion(clickedButton.transform, "NameText").GetComponent<TMP_Text>().text,
            StaticFunctions.FindChildByRecursion(clickedButton.transform, "LocalizationText").GetComponent<TMP_Text>().text };
        balloon.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = boxPieceInformation[0];
        balloon.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = boxPieceInformation[1];
        balloon.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = boxPieceInformation[2];
    }

    /// <summary>
    /// Confirm the component association.
    /// </summary>
    public void confirmAssociation()
    {
        if (balloon != null)
        {
            GameObject newGo = Instantiate(cubeDonePrefab, lineBoschParent.transform);
            newGo.transform.position = balloon.transform.position;
            newGo.transform.rotation = Quaternion.Euler(balloon.GetComponent<Orientate>().getInitialRotation());
            newGo.transform.localScale = balloon.transform.localScale / 6;
            //newGo.transform.SetParent(lineBoschParent.transform, false);
            Destroy(balloon);
            goS.Add(newGo);

            usabilityTest.stopTime(goS[goS.Count - 1], boxPieceInformation[1]);

            boxPieceInformation.Clear();
            confirmedFlag = true;
        }
    }

    /// <summary>
    /// Get if the box has any component associated.
    /// </summary>
    /// <returns>0 if no component associated and more than 0 otherwise.</returns>
    public int getBoxPieceInformation()
    {
        return boxPieceInformation.Count;
    }

    /// <summary>
    /// Get the number of objects in the scene.
    /// </summary>
    /// <returns>The number of objects in the scene.</returns>
    public bool getGoSCount()
    {
        return goS.Count == 1;
    }
}