/*****************************************************************************
// File Name :         JumpPad.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private JumpPadSettings settings;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.transform.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 force;

            CharacterMotor motor = collision.transform.GetComponent<CharacterMotor>();
            if (motor != null)
            {
                // The player's direction of movement.
                Vector3 input = motor.LocalMovementDirection;

                rb.drag = 0f;
                force = (input * settings.ForwardForce) + (Vector3.up * settings.UpwardForce);
            }
            // If there is no input and we want to bounce some random object, 
            // bounce it in its forward direction.
            else
                force = (transform.forward * settings.ForwardForce) + (Vector3.up * settings.UpwardForce);

            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}
