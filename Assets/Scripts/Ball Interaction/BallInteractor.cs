/*****************************************************************************
// File Name :         BallInteractor.cs
// Author :            Kyle Grenier
// Creation Date :     04/14/2021
//
// Brief Description : A character that can pick up and throw balls.
*****************************************************************************/
using UnityEngine;
using System.Collections;

public abstract class BallInteractor : MonoBehaviour
{
    private Rigidbody ball = null;

    [SerializeField] private Transform ballHolder;
    [SerializeField] private Transform ballInstantiationSpot;

    [SerializeField] private BallThrowerSettings settings;
    private float throwForce;
    private float currentWindUpTime;
    protected bool windUp = false;

    private void Awake()
    {
        throwForce = settings.initialThrowForce;
    }

    protected void AssignBall(Rigidbody ball)
    {
        if (this.ball != null)
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
        StopAllCoroutines();
        ball.transform.parent = null;
        ball.isKinematic = false;
        ball.transform.position = ballInstantiationSpot.position;
        ball.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        ball = null;

        StopWindUp();
    }

    protected void WindUpBall()
    {
        StartCoroutine(WindUp());
    }

    protected bool HasBall()
    {
        return ball != null;
    }

    private IEnumerator WindUp()
    {
        if (windUp)
            yield break;

        windUp = true;

        float diff = settings.maxThrowForce - settings.initialThrowForce;
        while (currentWindUpTime < settings.windUpTime)
        {
            currentWindUpTime += Time.deltaTime;
            throwForce = settings.initialThrowForce + (diff * currentWindUpTime) / settings.windUpTime;
            print(throwForce);
            //Debug.Log(currentWindUpTime);
            yield return null;
        }
    }

    protected void StopWindUp()
    {
        StopAllCoroutines();
        windUp = false;
        throwForce = settings.initialThrowForce;
        currentWindUpTime = 0f;
    }
}