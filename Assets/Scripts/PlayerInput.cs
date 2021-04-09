/*****************************************************************************
// File Name :         PlayerInput.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class PlayerInput : MonoBehaviour
{
    private CharacterMotor motor;
    private Vector3 input;


    private void Awake()
    {
        motor = GetComponent<CharacterMotor>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        input = new Vector3(h, 0, v);
        input = transform.TransformDirection(input);

        if (Input.GetButtonDown("Jump"))
            motor.Jump();

        if (Input.GetButtonDown("SpecialAbility"))
            motor.PerformSpecialAbility();
    }

    private void FixedUpdate()
    {
        motor.Move(input);
    }
}
