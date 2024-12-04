using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigate_UI : MonoBehaviour
{
    void Update()
    {

    }

    void Click_Space()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
    void Click_Play()
    {
        SceneManager.LoadSceneAsync("");
    }

    void Click_Settings()
    {
        SceneManager.LoadSceneAsync("Settings");
    }

    void click_Quit()
    {
        Application.Quit();
    }
}
