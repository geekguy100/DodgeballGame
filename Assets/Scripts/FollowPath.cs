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

    private void Start()
    {
        StartCoroutine(TraversePath());
    }

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

    public void Stop()
    {
        StopAllCoroutines();
    }
}