using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public Behaviour objBehavior;


    public void Settings()
    {
        objBehavior.enabled= !objBehavior.enabled;
    }
}
