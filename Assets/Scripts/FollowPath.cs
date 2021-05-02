/*****************************************************************************
// File Name :         FollowPath.cs
// Author :            Kyle Grenier
// Creation Date :     05/02/2021
//
// Brief Description : Makes a Transform follow a path by moving it to each child in the parent Path transform.
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private Transform path;
    [SerializeField] private float travelTime = 2f;
    private int pathPoint = 0;

    [SerializeField] private bool sporadicMovement;

    private void Start()
    {
        if (sporadicMovement)
            StartCoroutine(UpdateMovement());
        else
            StartCoroutine(TraversePath());
    }

    #region --- Sporadic Movement ---
    private IEnumerator Translate(Vector3 endPos)
    {
        Vector3 startPos = transform.localPosition;
        Vector3 finalPos = new Vector3(endPos.x, startPos.y, endPos.z);

        float movementSpeed = Random.Range(2f, 10f);

        float dist = Vector3.Distance(startPos, endPos);
        float distCovered = 0f;

        float counter = 0f;
        float fractionOfJourney = 0f;

        while (fractionOfJourney < 1f)
        {
            counter += Time.deltaTime;
            distCovered = counter * movementSpeed;
            fractionOfJourney = distCovered / dist;

            transform.localPosition = Vector3.Lerp(startPos, finalPos, fractionOfJourney);
            yield return null;
        }
    }

    private IEnumerator UpdateMovement()
    {
        Coroutine translationCoroutine = null;

        Transform[] endPositions = new Transform[path.childCount];
        for (int i = 0; i < endPositions.Length; ++i)
            endPositions[i] = path.GetChild(i);

        while (true)
        {
            if (translationCoroutine != null)
                StopCoroutine(translationCoroutine);

            Vector3 position = ArrayHelper.GetRandomElement(endPositions).position;
            translationCoroutine = StartCoroutine(Translate(position));

            yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        }
    }

    #endregion

    #region --- Simple Movement ---

    private IEnumerator TraversePath()
    {
        if (path == null)
        {
            yield break;
        }

        while (true)
        {
            transform.position = path.GetChild(pathPoint).position;

            Vector3 currentPos = transform.position;
            Vector3 nextPos;

            if (pathPoint + 1 >= path.childCount)
                nextPos = path.GetChild(0).position;
            else
                nextPos = path.GetChild(pathPoint + 1).position;


            float currentTime = 0f;
            while (currentTime < travelTime)
            {
                currentTime += Time.deltaTime;
                transform.position = Vector3.Lerp(currentPos, nextPos, currentTime / travelTime);
                yield return null;
            }

            ++pathPoint;
            if (pathPoint >= path.childCount)
                pathPoint = 0;

            yield return null;
        }
    }

    #endregion

    public void Stop()
    {
        StopAllCoroutines();
    }
}