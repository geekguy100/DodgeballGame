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
        CharacterMotor motor = collision.transform.GetComponent<CharacterMotor>();
        if (motor != null)
        {
            Rigidbody rb = motor.gameObject.GetComponent<Rigidbody>();

        }
    }
}
