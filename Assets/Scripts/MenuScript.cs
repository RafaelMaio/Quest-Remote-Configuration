// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : Menu navigation script.
// SPECIAL NOTES: X
// ===============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{
    /// <summary>
    /// Start the user test button.
    /// </summary>
    public Button startButton;

    /// <summary>
    /// Component association button.
    /// </summary>
    public Button associationButton;

    /// <summary>
    /// Add next object button.
    /// </summary>
    public Button addObjectButton;

    /// <summary>
    /// Remove current object button.
    /// </summary>
    public Button removeObjectButton;

    /// <summary>
    /// Communication with the Manager script - App main script.
    /// </summary>
    public Manager manager;

    /// <summary>
    /// Start menu.
    /// </summary>
    public GameObject startMenu;

    /// <summary>
    /// Collection of buttons.
    /// </summary>
    public GameObject buttonsCollection;

    /// <summary>
    /// Collection of component buttons.
    /// </summary>
    public GameObject componentsCollection;

    /// <summary>
    /// Collection of component buttons - Content.
    /// </summary>
    public GameObject content;

    /// <summary>
    /// Text in menu.
    /// </summary>
    public GameObject textMenu;

    /// <summary>
    /// User test progress text.
    /// </summary>
    public TMP_Text progressText;

    /// <summary>
    /// Next component text.
    /// </summary>
    public TMP_Text messageText;

    /// <summary>
    /// Camera - To acess its pose.
    /// </summary>
    private GameObject cam;

    /// <summary>
    /// Unity Start function.
    /// </summary>
    void Start()
    {
        startButton.onClick.AddListener(startTest);
        addObjectButton.onClick.AddListener(addObject);
        removeObjectButton.onClick.AddListener(removeObject);
        cam = GameObject.Find("OVRCameraRig");
    }

    /// <summary>
    /// Unity Update function.
    /// </summary>
    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0);
    }

    /// <summary>
    /// Start the user test when the button is pressed.
    /// </summary>
    private void startTest()
    {
        startMenu.SetActive(false);
        buttonsCollection.SetActive(true);
        manager.startPressed();
    }

    /// <summary>
    /// Add the next object to the scene.
    /// </summary>
    private void addObject()
    {
        manager.next();
        if (!textMenu.activeSelf)
        {
            associationButton.interactable = true;
            removeObjectButton.interactable = true;
            textMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Remove current object from the scene.
    /// </summary>
    private void removeObject()
    {
        manager.removeObject();
    }

    /// <summary>
    /// Component button pressed.
    /// </summary>
    /// <param name="clickedButton">Component button that was pressed.</param>
    public void componentClicked(GameObject clickedButton)
    {
        if (StaticFunctions.FindChildByRecursion(clickedButton.transform, "normalImage").gameObject.activeSelf)
        {
            for (int i = 0; i < content.transform.childCount; i++)
            {
                if (StaticFunctions.FindChildByRecursion(content.transform.GetChild(i), "selectedImage").gameObject.activeSelf)
                {
                    StaticFunctions.FindChildByRecursion(content.transform.GetChild(i), "normalImage").gameObject.SetActive(true);
                    StaticFunctions.FindChildByRecursion(content.transform.GetChild(i), "selectedImage").gameObject.SetActive(false);
                    break;
                }
            }
            StaticFunctions.FindChildByRecursion(clickedButton.transform, "normalImage").gameObject.SetActive(false);
            StaticFunctions.FindChildByRecursion(clickedButton.transform, "selectedImage").gameObject.SetActive(true);
            manager.componentClicked(clickedButton);
        }
    }

    /// <summary>
    /// Change number of objects and progress indicator.
    /// </summary>
    /// <param name="numObjs">Current number of objects.</param>
    public void changeNumObjectsText(int numObjs)
    {
        progressText.text = numObjs + "/8";
        switch (numObjs)
        {
            case 0:
                messageText.text = "8-738-726-533 - Saco 325x230";
                break;
            case 1:
                messageText.text = "8-738-710-491 - Mangueira Flexivel";
                break;
            case 2:
                messageText.text = "8-709-918-680 - Pilha alcalina 1,5V LR20";
                break;
            case 3:
                messageText.text = "8-738-726-577 - Etiqueta";
                break;
            case 4:
                messageText.text = "8-710-103-045 - Anilha de vedação";
                break;
            case 5:
                messageText.text = "8-709-918-850 - Acessório de instalação";
                break;
            case 6:
                messageText.text = "7-709-003-556 - Acessório Nr.1083";
                break;
            case 7:
                messageText.text = "8-731-500-264 - Manípulo";
                break;
            case 8:
                messageText.text = "Configuration Done!";
                break;
        }
    }

    /// <summary>
    /// Open/close the menu for association.
    /// </summary>
    /// <param name="open">Open/close the menu.</param>
    public void openAssociationMenu(bool open)
    {
        componentsCollection.SetActive(open);
        buttonsCollection.SetActive(!open);
        manager.changeGoToAssociation(open);
    }

    /// <summary>
    /// Association confirmation button pressed.
    /// </summary>
    public void confirmAssociation()
    {
        if (manager.getBoxPieceInformation() != 0)
        {
            manager.confirmAssociation();
            openAssociationMenu(false);
            for (int i = 0; i < content.transform.childCount; i++)
            {
                //content.transform.GetChild(i).GetComponent<Image>().color = normalColor;

                StaticFunctions.FindChildByRecursion(content.transform.GetChild(i), "normalImage").gameObject.SetActive(true);
                StaticFunctions.FindChildByRecursion(content.transform.GetChild(i), "selectedImage").gameObject.SetActive(false);
            }
        }
        addObject();
    }
}