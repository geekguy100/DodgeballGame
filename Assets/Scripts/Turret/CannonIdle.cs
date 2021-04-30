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
    [SerializeField] private float rotationFrequency;
    [SerializeField] private float rotationAmplitude;

    [SerializeField] private float detectionAngle = 45f;

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
    /// <returns></returns>
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
        if (other.CompareTag("Player") && target == null)
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