/*****************************************************************************
// File Name :         BallInteractor.cs
// Author :            Kyle Grenier
// Creation Date :     04/14/2021
//
// Brief Description : A character that can pick up and throw balls.
*****************************************************************************/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterAudioManager))]
public abstract class BallInteractor : MonoBehaviour
{
    private Rigidbody ball = null;
    private CharacterAudioManager audioManager;

    [SerializeField] private Transform ballHolder;
    [SerializeField] private Transform ballInstantiationSpot;

    [Tooltip("Transform that determines the rotation of the ball.")]
    [SerializeField] private Transform look;

    [SerializeField] private BallThrowerSettings settings;
    private float throwForce;
    private float currentWindUpTime;
    protected bool windUp = false;

    private bool canPickUpBall = true;

    private float gravity;

    [Header("BALL THROWING")]
    [SerializeField] private float rayLength;
    [SerializeField] private float arcAmount;
    [SerializeField] private LineRenderer lineRenderer;

    private Vector3 hitPos;
    private bool raycastHit;
    private Vector3 launchVelocity;
    private bool doneWindingUp = false;


    private void Awake()
    {
        audioManager = GetComponent<CharacterAudioManager>();

        throwForce = settings.initialThrowForce;
        gravity = Physics.gravity.y;
    }

    protected virtual void Update()
    {
        // Draw the path the ball will take if we're winding up.
        if (windUp)
        {
            // If we hit something within the ray's length.
            if (Physics.Raycast(ball.position + look.forward * 2f, look.forward, out RaycastHit hit, rayLength))
            {
                float diff = hit.point.y - ball.position.y;
                h = diff * Mathf.Sign(diff) + arcAmount;
                hitPos = hit.point;
                DrawPath(hitPos);
            }

            if (doneWindingUp)
            {
                launchVelocity = CalculateLaunchData(hitPos).initialVelocity;
            }

            //print("HIT POS: " + hitPos);
        }
    }

    #region --- Picking up and Throwing ---
    protected void AssignBall(Rigidbody ball)
    {
        if (this.ball != null || !canPickUpBall)
            return;

        this.ball = ball;
        this.ball.isKinematic = true;
        this.ball.transform.parent = ballHolder;
        this.ball.transform.localPosition = Vector3.zero;
    }

    protected void ThrowBall()
    {
        if (ball == null)
            return;

        canPickUpBall = false;
        StopAllCoroutines();
        ball.transform.parent = null;
        ball.isKinematic = false;
        ball.transform.position = ballInstantiationSpot.position;
        //ball.AddForce(look.forward * throwForce, ForceMode.Impulse);
        //ball.velocity = CalculateLaunchData(hitPos).initialVelocity;
        ball.velocity = launchVelocity;
        ball = null;

        audioManager.PlayThrowSFX();

        StopWindUp();
    }

    protected void WindUpBall()
    {
        StartCoroutine(WindUp());
    }

    private IEnumerator WindUp()
    {
        if (windUp)
            yield break;

        windUp = true;

        //float diff = settings.maxThrowForce - settings.initialThrowForce;
        while (currentWindUpTime < settings.windUpTime)
        {
            currentWindUpTime += Time.deltaTime;
            //throwForce = settings.initialThrowForce + (diff * currentWindUpTime) / settings.windUpTime;

            launchVelocity = Vector3.Lerp(Vector3.zero, CalculateLaunchData(hitPos).initialVelocity, currentWindUpTime / settings.windUpTime);

            //print(throwForce);
            //Debug.Log(currentWindUpTime);
            yield return null;
        }

        doneWindingUp = true;
    }
    #endregion

    #region --- Helper Methods ---
    protected bool HasBall()
    {
        return ball != null;
    }

    private IEnumerator ResetPickup()
    {
        yield return new WaitForSeconds(0.25f);
        canPickUpBall = true;
    }


    protected void StopWindUp()
    {
        StopAllCoroutines();
        StartCoroutine(ResetPickup());
        windUp = false;
        doneWindingUp = false;
        throwForce = settings.initialThrowForce;
        launchVelocity = Vector3.zero;
        currentWindUpTime = 0f;
        lineRenderer.enabled = false;
    }

    #endregion

    #region --- Launching the Dodgeball ---

    public struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }

    [Tooltip("The max height the ball will be at.")]
    [SerializeField] private float h;

    protected LaunchData CalculateLaunchData(Vector3 position)
    {
        float displacementY = position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(position.x - ball.position.x, 0, position.z - ball.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath(Vector3 position)
    {
        if (!lineRenderer.enabled)
            lineRenderer.enabled = true;

        LaunchData launchData = CalculateLaunchData(position);
        Vector3 previousDrawPoint = ball.position;

        int resolution = 30;
        lineRenderer.positionCount = resolution;

        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * settings.windUpTime;
            Vector3 displacement = launchVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ball.position + displacement;
            lineRenderer.SetPosition(i - 1, drawPoint);

            //Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    #endregion
}