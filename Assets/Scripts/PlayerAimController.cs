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
[RequireComponent(typeof(BallInteractor))]
[RequireComponent(typeof(CharacterStateManager))]
public class PlayerAimController : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject aimCamera;
    [SerializeField] private GameObject reticle;
    [SerializeField] private bool useReticle;

    private CharacterRotateWithCamera playerRotator;
    private BallInteractor ballInteractor;
    private CharacterStateManager stateManager;

    [SerializeField] private float switchWaitTime = 0.25f;
    private bool aiming;

    private enum AimState { NOT_AIMING, AIMING };
    private AimState state;

    private void Awake()
    {
        playerRotator = GetComponent<CharacterRotateWithCamera>();
        ballInteractor = GetComponent<BallInteractor>();
        stateManager = GetComponent<CharacterStateManager>();
    }

    private void Start()
    {
        reticle.SetActive(false);
    }

    void Update()
    {
        if (!stateManager.canMove && state == AimState.AIMING)
        {
            StartCoroutine(SwitchToMain());
        }

        if (!stateManager.canMove)
            return;

        if (Input.GetMouseButtonDown(0) && ballInteractor.HasBall())
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
        if (useReticle)
            reticle.SetActive(false);
        yield return new WaitForSeconds(switchWaitTime);
        aiming = false;
        mainCamera.SetActive(true);
        aimCamera.SetActive(false);

        playerRotator.ResetAngles();
        state = AimState.NOT_AIMING;
    }

    private IEnumerator SwitchToAim()
    {
        aiming = true;
        if (useReticle)
            reticle.SetActive(true);
        yield return new WaitForSeconds(switchWaitTime);
        mainCamera.SetActive(false);
        aimCamera.SetActive(true);

        playerRotator.SwitchToAimAngles();
        state = AimState.AIMING;
    }

    public bool IsAiming()
    {
        return aiming;
    }
}
