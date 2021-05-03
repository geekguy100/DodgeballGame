/*****************************************************************************
// File Name :         SkipTutorial.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SkipTutorial : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnGameWin += Transition;
    }

    private void OnDisable()
    {
        EventManager.OnGameWin -= Transition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !GameStats.paused)
        {
            SceneManager.LoadScene("NatLevel");
        }
    }

    private void Transition()
    {
        StartCoroutine(TransitionCoroutine());
    }

    private IEnumerator TransitionCoroutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("NatLevel");
    }
}
