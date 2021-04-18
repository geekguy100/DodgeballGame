/*****************************************************************************
// File Name :         BallLevitator.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class BallLevitator : MonoBehaviour
{
    [SerializeField] private float levitationFrequency;
    public float Frequency { get { return levitationFrequency; } }
    [SerializeField] private float levitationAmplitude;
    public float Amplitude { get { return levitationAmplitude; } }

    [SerializeField] private GameObject fakeBallPrefab;
    [SerializeField] private GameObject realBallPrefab;
    private GameObject currentBall;

    private void Start()
    {
        InstaSpawnBall();
    }

    public void OnPlayerEnter(Transform player)
    {
        if (currentBall == null)
            return;

        Destroy(currentBall);
        currentBall = null;
        Instantiate(realBallPrefab, player.position, Quaternion.identity);
        SpawnBall();
    }

    public void SpawnBall()
    {
        StartCoroutine(WaitThenSpawn());
    }

    private void InstaSpawnBall()
    {
        currentBall = Instantiate(fakeBallPrefab, transform.position + Vector3.up, Quaternion.identity);
    }

    private void Update()
    {
        if (currentBall == null)
            return;
    }

    private IEnumerator WaitThenSpawn()
    {
        yield return new WaitForSeconds(1f);
        currentBall = Instantiate(fakeBallPrefab, transform.position + Vector3.up, Quaternion.identity);
    }
}
