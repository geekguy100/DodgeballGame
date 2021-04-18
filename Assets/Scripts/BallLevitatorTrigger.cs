/*****************************************************************************
// File Name :         BallLevitatorTrigger.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class BallLevitatorTrigger : MonoBehaviour
{
    private BallLevitator ballLevitator;

    private void Awake()
    {
        ballLevitator = GetComponentInParent<BallLevitator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LevitatingBall"))
        {
            StartCoroutine(Levitate(other.transform));
        }
        else if (other.CompareTag("Player"))
        {
            ballLevitator.OnPlayerEnter(other.transform);
        }
    }

    private IEnumerator Levitate(Transform ball)
    {
        float currentTime = 0f;
        float initialYPos = ball.transform.position.y;
        float yOffset = 0.5f;

        while(ball != null)
        {
            // Don't run while we're paused.
            while (Time.timeScale == 0)
                yield return null;

           // print("RN");
            currentTime += Time.deltaTime;
            Vector3 pos = ball.position;
            pos.y = Mathf.Sin(currentTime * ballLevitator.Frequency) * ballLevitator.Amplitude + initialYPos + yOffset;

            ball.transform.position = pos;

            yield return null;
        }
    }
}
