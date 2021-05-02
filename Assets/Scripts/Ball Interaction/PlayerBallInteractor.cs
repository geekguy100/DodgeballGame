/*****************************************************************************
// File Name :         PlayerBallInteractor.cs
// Author :            Kyle Grenier
// Creation Date :     04/14/2021
//
// Brief Description : Gives the player a ball on trigger enter.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(CharacterStateManager))]
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

    [SerializeField] private GameObject ragdoll;
    [SerializeField] private Rigidbody ragdollSpine;
    [SerializeField] private GameObject animatedChalky;
    private bool shouldRagdoll = false;
    private float currentTime;
    [SerializeField] private float ragdollTime;

    private CharacterStateManager stateManager;
    private Vector3 hitForce;

    protected override void Awake()
    {
        base.Awake();
        stateManager = GetComponent<CharacterStateManager>();
    }

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
        else if (other.transform.CompareTag("EnemyDodgeball"))
        {
            hitForce = other.relativeVelocity;
            shouldRagdoll = true;
            StopWindUp();
            print("Hit by enemy ball!");
        }
    }

    protected override void Update()
    {
        if (stateManager.canMove)
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

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                EventManager.PlayerToggleLockOn(ToggleLockOn());
            }
        }

        if (shouldRagdoll)
            HandleRagdoll();
    }

    private void HandleRagdoll()
    {
        if (!ragdoll.activeInHierarchy)
            ActivateRagdoll();

        currentTime += Time.deltaTime;
        if (currentTime > ragdollTime)
        {
            DeactiveRagdoll();
            currentTime = 0;
            shouldRagdoll = false;
        }
    }

    private void ActivateRagdoll()
    {
        ragdoll.SetActive(true);
        animatedChalky.SetActive(false);
        stateManager.canMove = false;
        GetComponent<Rigidbody>().AddForce(hitForce / 2, ForceMode.VelocityChange);
        ragdollSpine.AddForce(hitForce * 2f, ForceMode.VelocityChange);
    }

    private void DeactiveRagdoll()
    {
        ragdoll.SetActive(false);
        ragdollSpine.MovePosition(transform.position);
        animatedChalky.SetActive(true);
        stateManager.canMove = true;
    }

    public override void Targeted()
    {
        audioManager.PlayTargetedSFX();
    }
}
