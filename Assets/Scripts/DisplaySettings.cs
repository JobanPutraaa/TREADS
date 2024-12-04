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

    public void Start()
    {
        isFullScreen = true;
        Allresolutions = Screen.resolutions;

        List<string> list = new List<string>();

        foreach(Resolution res in Allresolutions)
        {
            list.Add(res.ToString());
        }

        resolutionDropdown.AddOptions(list);
    }
    void Update()
    {
        

    }
}
