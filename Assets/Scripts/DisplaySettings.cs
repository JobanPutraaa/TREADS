using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySettings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullToggle;

    Resolution[] Allresolutions;
    public bool isFullScreen;
    public int SelectedRes;
    List<Resolution> SelectedResList = new List<Resolution>();

    public void Start()
    {
        isFullScreen = true;
        Allresolutions = Screen.resolutions;

        List<string> list = new List<string>();
        string newRes;
        foreach (Resolution res in Allresolutions)
        {
            newRes = res.width.ToString() + " X " + res.height.ToString();
            if(!list.Contains(newRes))
            {
                list.Add(newRes);
                SelectedResList.Add(res);
            }
        }

        resolutionDropdown.AddOptions(list);
    }

    public void changeRes()
    {
        SelectedRes = resolutionDropdown.value;
        Screen.SetResolution(SelectedResList[SelectedRes].width, SelectedResList[SelectedRes].height, isFullScreen);
    }

    public void ChangeFullScreen()
    {
        isFullScreen = fullToggle.isOn;
        Screen.SetResolution(SelectedResList[SelectedRes].width, SelectedResList[SelectedRes].height, isFullScreen);
    }
    void Update()
    {

    }
}
