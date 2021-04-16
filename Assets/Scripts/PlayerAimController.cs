/*****************************************************************************
// File Name :         PlayerAimController.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterRotateWithCamera))]
public class PlayerAimController : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject aimCamera;
    [SerializeField] private GameObject reticle;

    private CharacterRotateWithCamera playerRotator;

    [SerializeField] private float switchWaitTime = 0.25f;
    private bool aiming;

    private void Awake()
    {
        playerRotator = GetComponent<CharacterRotateWithCamera>();
    }

    private void Start()
    {
        reticle.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            StartCoroutine(SwitchToAim());
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonDown(1))
        {
            StopAllCoroutines();
            StartCoroutine(SwitchToMain());
        }
    }

    private IEnumerator SwitchToMain()
    {
        reticle.SetActive(false);
        yield return new WaitForSeconds(switchWaitTime);
        aiming = false;
        mainCamera.SetActive(true);
        aimCamera.SetActive(false);

        playerRotator.ResetAngles();
    }

    private IEnumerator SwitchToAim()
    {
        aiming = true;
        reticle.SetActive(true);
        yield return new WaitForSeconds(switchWaitTime);
        mainCamera.SetActive(false);
        aimCamera.SetActive(true);

        playerRotator.SwitchToAimAngles();
    }

    public bool IsAiming()
    {
        return aiming;
    }
}
