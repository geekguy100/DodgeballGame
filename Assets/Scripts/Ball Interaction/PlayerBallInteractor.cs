/*****************************************************************************
// File Name :         PlayerBallInteractor.cs
// Author :            Kyle Grenier
// Creation Date :     04/14/2021
//
// Brief Description : Gives the player a ball on trigger enter.
*****************************************************************************/
using UnityEngine;

public class PlayerBallInteractor : BallInteractor
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Dodgeball"))
        {
            AssignBall(other.transform.GetComponent<Rigidbody>());
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && HasBall())
            WindUpBall();
        else if (windUp && Input.GetMouseButtonUp(0))
            ThrowBall();
        else if (windUp && Input.GetMouseButtonDown(1))
            StopWindUp();
    }
}
