/*****************************************************************************
// File Name :         CharacterMotor.cs
// Author :            Kyle Grenier
// Creation Date :     04/06/2021
//
// Brief Description : Performs movement on a character; actually moves the character.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("How fast the character moves.")]
    [SerializeField] private float movementSpeed;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Double Jumping")]
    [Tooltip("The maximum number of times the character can jump.")]
    [SerializeField] private int maxJumps = 2;

    [Tooltip("Multplier affecting power of consecutive jumps. A larger value means less power. Cannot equal 0.")]
    [SerializeField] private float consecutiveJumpMultiplier = 1f;

    [Tooltip("True if each jump should be less powerful than the previous, regardless of the consecutiveJumpMultiplier value.")]
    [SerializeField] private bool consecutiveJumpsLessPowerful = true;

    private int currentJumps;




    private bool grounded = false;

    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        print(grounded);
        bool touchingGround = Physics.Linecast(transform.position, groundCheck.position, whatIsGround);

        // Once we hit the ground, make sure to reset out current jumps.
        if (!grounded && touchingGround)
        {
            grounded = true;
            currentJumps = 0;
        }
        else if (grounded && !touchingGround)
            grounded = false;
    }

    public void Move(Vector3 dir)
    {
        Vector3 vel = new Vector3(dir.x, 0, dir.z) * movementSpeed;
        vel.y = rb.velocity.y;

        rb.velocity = vel;
    }

    public void Jump()
    {
        if (currentJumps < maxJumps)
        {
            PerformJump();
        }
    }

    private void PerformJump()
    {
        ++currentJumps;

        print(currentJumps);

        Vector3 vel = rb.velocity;
        float newVel = Mathf.Sqrt(2 * 9.81f * jumpHeight);

        if (currentJumps > 1 && consecutiveJumpsLessPowerful)
            newVel /= (currentJumps * consecutiveJumpMultiplier);
        else if (currentJumps > 1)
            newVel /= consecutiveJumpMultiplier;

        vel.y = newVel;
        rb.velocity = vel;
    }
}