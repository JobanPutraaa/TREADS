using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public GameObject AudioCanvas;
    public GameObject DisplayCanvas;
    public GameObject CreditsCanvas;
    public void Click_Audio()
    {
        AudioCanvas.SetActive(true);
        DisplayCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
    }

    public void Click_Display()
    {
        AudioCanvas.SetActive(false);
        DisplayCanvas.SetActive(true);
        CreditsCanvas.SetActive(false);
    }

    public void Click_Credits()
    {
        AudioCanvas.SetActive(false);
        DisplayCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }
}
