/*****************************************************************************
// File Name :         MenuManager.cs
// Author :            Kyle Grenier
// Creation Date :     04/18/2021
//
// Brief Description : Manages button presses on title screen.
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("NatLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
