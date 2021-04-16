/*****************************************************************************
// File Name :         PlayerAimController.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class PlayerAimController : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject aimCamera;
    [SerializeField] private float switchWaitTime = 0.25f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            StartCoroutine(SwitchToAim());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
            StartCoroutine(SwitchToMain());
        }
    }

    private IEnumerator SwitchToMain()
    {
        yield return new WaitForSeconds(switchWaitTime);
        mainCamera.SetActive(true);
        aimCamera.SetActive(false);
    }

    private IEnumerator SwitchToAim()
    {
        yield return new WaitForSeconds(switchWaitTime);
        mainCamera.SetActive(false);
        aimCamera.SetActive(true);
    }

}
