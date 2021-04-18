/*****************************************************************************
// File Name :         PlayerInput.cs
// Author :            Kyle Grenier
// Creation Date :     04/09/2021
//
// Brief Description : Script that takes in player keyboard input.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class PlayerInput : MonoBehaviour
{
    private CharacterMotor motor;
    private Vector3 input;

    //Added by Ein
    Animator animator;

    private void Awake()
    {
        motor = GetComponent<CharacterMotor>();
    }

    private void Start()
    {
        //Added by Ein
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        input = new Vector3(h, 0, v).normalized;
        input = transform.TransformDirection(input);

        if (Input.GetButtonDown("Jump"))
            motor.Jump();

        if (Input.GetButtonDown("SpecialAbility"))
            motor.PerformSpecialAbility();




        //Added by Ein
        animator.SetFloat("VerticalMovement", v);
        animator.SetFloat("HorizontalMovement", h);
        animator.SetBool("Dance", Input.GetKey(KeyCode.V));
        animator.SetBool("Scroomble", Input.GetKey(KeyCode.B));
    }

    private void FixedUpdate()
    {
        motor.Move(input);
    }
}
