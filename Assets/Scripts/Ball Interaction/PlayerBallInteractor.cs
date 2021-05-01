/*****************************************************************************
// File Name :         PlayerBallInteractor.cs
// Author :            Kyle Grenier
// Creation Date :     04/14/2021
//
// Brief Description : Gives the player a ball on trigger enter.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(CharacterAudioManager))]
public class PlayerBallInteractor : BallInteractor
{
    // IDEA: Use Sebastian's method, but instead of pre-defning the position use a raycast from the followTarget transform
    // and take the hit as the position. Then when we throw the ball we get the velocity required to launch and hit that target.

    // PROS: Realistic, and can implement a projectile arc.
    // CONS: Need to figure out how to incorporate the 'wind up' mechanic into this. Because this approach would take a distance from the player (ray length)
    // and that is the sole determinant of whether or not they'll hit their target
    
    //Added by Ein
    Animator animator;

    protected override void Start()
    {
        base.Start();

        //Added by Ein
        animator = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Dodgeball"))
        {
            AssignBall(other.transform.GetComponent<Rigidbody>());
        }
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) && HasBall())
            WindUpBall();
        else if (windUp && Input.GetMouseButtonUp(0))
        {
            ThrowBall();

            //Added by Ein
            animator.SetTrigger("BallThrown");
        }
        else if (windUp && Input.GetMouseButtonDown(1))
            StopWindUp();
    }

    public override void Targeted()
    {
        audioManager.PlayTargetedSFX();
    }
}
