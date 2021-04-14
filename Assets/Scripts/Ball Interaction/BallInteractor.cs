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

    [SerializeField] private BallThrowerSettings settings;
    private float throwForce;
    private float currentWindUpTime;
    protected bool windUp = false;

    private void Awake()
    {
        throwForce = settings.throwForce;
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
        while (currentWindUpTime < settings.windUpTime)
        {
            currentWindUpTime += Time.deltaTime;
            throwForce += (currentWindUpTime);
            Debug.Log(currentWindUpTime);
            yield return null;
        }
    }

    protected void StopWindUp()
    {
        StopAllCoroutines();
        windUp = false;
        throwForce = settings.throwForce;
    }
}