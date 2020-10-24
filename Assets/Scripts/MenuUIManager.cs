using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("DemoScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
