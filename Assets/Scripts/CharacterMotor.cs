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
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float airControl = 5f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    private bool grounded = false;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        grounded = Physics.Linecast(transform.position, groundCheck.position, whatIsGround);
    }

    public void Move(Vector3 dir)
    {
        Vector3 vel = new Vector3(dir.x, 0, dir.z) * movementSpeed;
        vel.y = rb.velocity.y;

        rb.velocity = vel;
    }

    public void Jump()
    {
        if (grounded)
        {
            Vector3 vel = rb.velocity;
            vel.y = Mathf.Sqrt(2 * 9.81f * jumpHeight);

            rb.velocity = vel;
        }
    }
}