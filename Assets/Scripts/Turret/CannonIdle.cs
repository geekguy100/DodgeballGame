/*****************************************************************************
// File Name :         CannonIdle.cs
// Author :            Kyle Grenier
// Creation Date :     04/28/2021
//
// Brief Description : Cannon idle behaviour: Surveys the area around it.
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class CannonIdle : ICannonState
{
    [Header("Rotation Fields")]
    [Tooltip("How quickly the cannon surveys the area (rotation speed).")]
    [SerializeField] private float rotationFrequency;

    [Tooltip("How far the cannon rotates.")]
    [SerializeField] private float rotationAmplitude;


    [Header("Cannon Target Detection")]
    [Tooltip("The cannon's field of view. If a target is within the cannon's boundaries and is at most this angle from " +
        "the cannon's shooting point, the cannon will begin firing at the target.")]
    [SerializeField] private float detectionAngle = 45f;

    [Tooltip("The layer(s) that identify GameObject's as targets.")]
    [SerializeField] private LayerMask whatIsTarget;

    public override void SurveyArea()
    {
        Debug.Log("[CANNON_IDLE]: Starting area survellience.");
        target = null;
        StopAllCoroutines();
        StartCoroutine(Survey());
    }

    public override void FireAtTarget(GameObject target)
    {
        Debug.Log("[CANNON_IDLE]: Cannot fire at target while surveying the area.");
    }

    public override void TargetAquired(GameObject target)
    {
        cannon.currentState = cannon.FireState;
        cannon.TargetAcquired(target);
    }

    public override void TargetLost(GameObject target)
    {
        Debug.LogWarning("[CANNON_IDLE]: We don't have a target to begin with!");
    }

    /// <summary>
    /// Rotates the cannon in search of a target.
    /// </summary>
    private IEnumerator Survey()
    {
        Vector3 newRot = cannon.CannonBody.eulerAngles;
        float startRotY = newRot.y;

        float currentTime = 0f;

        while (target == null)
        {
            newRot.y = Mathf.Sin(currentTime * rotationFrequency) * rotationAmplitude + startRotY;
            cannon.CannonBody.eulerAngles = newRot;

            currentTime += Time.deltaTime;
            yield return null;
        }

        TargetAquired(target);
    }

    private GameObject target;
    private void OnTriggerStay(Collider other)
    {
        if ((whatIsTarget == (whatIsTarget | (1 << other.gameObject.layer))) && target == null)
        {
            Vector3 vectorToPlayer = (other.transform.position - cannon.origin.position);
            float angle = Vector3.Angle(-cannon.origin.forward, vectorToPlayer);

            if (angle <= detectionAngle)
            {
                print("TARGET FOUND");
                target = other.gameObject;
            }
        }
    }
}