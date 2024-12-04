using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigate_UI : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
    public void Click_Play()
    {
        SceneManager.LoadSceneAsync("");
    }
    public void Click_Settings()
    {
        SceneManager.LoadSceneAsync("Settings");
    }

    public void click_Quit()
    {
        Application.Quit();
    }
}
