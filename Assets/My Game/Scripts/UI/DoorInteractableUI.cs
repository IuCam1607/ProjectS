using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteractableUI : MonoBehaviour
{
    public void ExitToDesktop()
    {
        Debug.Log("Exiting to desktop");

        Application.Quit();
    }
}
