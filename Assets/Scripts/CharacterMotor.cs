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
    #region --- Movement Fields ---
    [Header("Movement")]
    [Tooltip("How fast the character moves.")]
    [SerializeField] private float movementSpeed;

    // True if the character is on the ground.
    private bool grounded = false;
    public bool Grounded { get { return grounded; } }

    /// <summary>
    /// The character's local direction of movement.
    /// </summary>
    public Vector3 LocalMovementDirection { get { return localMovementDirection; } }
    private Vector3 localMovementDirection;

    #endregion

    #region --- Jumping Fields ---
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

    #endregion

    // The special ability attached to this character.
    private ISpecialAbility specialAbility;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        specialAbility = GetComponent<ISpecialAbility>();
    }

    private void Update()
    {
        bool touchingGround = Physics.Linecast(transform.position, groundCheck.position);

        // Once we hit the ground, make sure to reset out current jumps.
        if (!grounded && touchingGround)
        {
            grounded = true;
            currentJumps = 0;
        }
        else if (grounded && !touchingGround)
            grounded = false;
    }

    #region --- Movement ---
    public void Move(Vector3 dir)
    {
        //if (!canMove)
        //{
        //    localMovementDirection = Vector3.zero;
        //    return;
        //}

        localMovementDirection = dir;

        //Vector3 vel = new Vector3(dir.x, 0, dir.z) * movementSpeed;
        //vel.y = rb.velocity.y;

        //rb.velocity = vel;
        rb.MovePosition(transform.position + dir * movementSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Returns true if the character is moving.
    /// </summary>
    /// <returns>True if the character is moving.</returns>
    public bool IsMoving()
    {
        return localMovementDirection != Vector3.zero;
    }

    public void ResetVelocity()
    {
        //Debug.Log("PLAYER VELOCITY RESET");
        rb.velocity = Vector3.zero;
    }

    #endregion

    #region --- Jumping ---
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

        if(currentJumps > 1)
            ResetVelocity();

        Vector3 vel = rb.velocity;
        float newVel = Mathf.Sqrt(2 * 9.81f * jumpHeight);

        if (currentJumps > 1 && consecutiveJumpsLessPowerful)
            newVel /= (currentJumps * consecutiveJumpMultiplier);
        else if (currentJumps > 1)
            newVel /= consecutiveJumpMultiplier;

        vel.y = newVel;
        rb.velocity = vel;
    }
    #endregion

    #region --- Special (SHIFT Key) Ability ---
    public void PerformSpecialAbility()
    {
        if (specialAbility != null)
            specialAbility.Execute(this, localMovementDirection);
    }
    #endregion

    private void OnCollisionEnter(Collision col)
    {
        if (!col.gameObject.CompareTag("IgnoreVelocity") && rb.velocity != Vector3.zero)
        {
            //print(col.gameObject.name);
            ResetVelocity();
        }
    }
}