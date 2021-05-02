/*****************************************************************************
// File Name :         CannonFireState.cs
// Author :            Kyle Grenier
// Creation Date :     04/28/2021
//
// Brief Description : State for when the cannon is firing at a target.
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class CannonFire : ICannonState
{
    [Header("Shooting Fields")]
    [SerializeField] private float shootForce;
    [SerializeField] private float timeBetweenShots;

    [SerializeField] private Rigidbody dodgeball;
    [SerializeField] private float rotationResetTime = 1f;

    [SerializeField] private LineRenderer lineRenderer;

    private Quaternion startRotation;

    private void Start()
    {
        lineRenderer.material.color = Color.red;
        startRotation = cannon.CannonBody.rotation;
    }

    public override void FireAtTarget(GameObject target)
    {
        StartCoroutine(TrackTarget(target.transform));
        StartCoroutine(ShootDodgeball(target.transform));
    }

    public override void SurveyArea()
    {
        Debug.LogWarning("[CANNON_FIRE]: Cannot survey the area while tracking a target!");
    }

    public override void TargetAquired(GameObject target)
    {
        Debug.Log("[CANNON_FIRE]: Target found: " + target.name);
        target.GetComponent<BallInteractor>().Targeted();
        StopAllCoroutines();
        targetInRange = true;
        FireAtTarget(target);
    }

    public override void TargetLost(GameObject target)
    {
        Debug.Log("[CANNON_FIRE]: Target lost: " + target.name);
        StartCoroutine(ResetRotation());
    }


    // Helper methods

    private IEnumerator TrackTarget(Transform target)
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;

        while (targetInRange)
        {
            lineRenderer.SetPosition(0, cannon.origin.position);
            lineRenderer.SetPosition(1, target.position);
            Vector3 vectorToTarget = (target.position - cannon.CannonBody.position);
            cannon.CannonBody.rotation = Quaternion.LookRotation(-vectorToTarget, Vector3.up);
            yield return null;
        }

        lineRenderer.enabled = false;
    }

    private IEnumerator ShootDodgeball(Transform target)
    {
        while (targetInRange)
        {
            yield return new WaitForSeconds(timeBetweenShots);
            if (targetInRange)
            {
                Rigidbody db = Instantiate(dodgeball, cannon.origin.position, Quaternion.identity);
                db.AddForce(-cannon.origin.forward * shootForce, ForceMode.Impulse);
            }
        }
    }

    private IEnumerator ResetRotation()
    {
        Quaternion currentRot = cannon.CannonBody.rotation;
        float currentTime = 0f;

        while (currentTime < rotationResetTime)
        {
            currentTime += Time.deltaTime;
            cannon.CannonBody.rotation = Quaternion.Lerp(currentRot, startRotation, currentTime / rotationResetTime);
            yield return null;
        }

        // After we reset the rotation, go back to idling.
        cannon.currentState = cannon.IdleState;
        cannon.SurveyArea();
    }

    // Once the player leaves the turret's range, perform the 
    // reset sequence.
    private bool targetInRange;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && targetInRange)
        {
            targetInRange = false;
            TargetLost(other.gameObject);
        }
    }
}