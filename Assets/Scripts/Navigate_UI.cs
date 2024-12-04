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
            SceneManager.LoadSceneAsync(1);
        }
    }

    public void Click_Back()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Click_Play()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void Click_Settings()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void click_Quit()
    {
        Application.Quit();
    }
}
