/*****************************************************************************
// File Name :         DashAbility.cs
// Author :            Kyle Grenier
// Creation Date :     04/08/2021
//
// Brief Description : A dash ability that thrusts the character in the direction they are facing.
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class DashAbility : ISpecialAbility
{
    [Tooltip("The strength of the dash. A higher number means a further dash.")]
    [SerializeField] private float dashStrength;

    [Tooltip("The time in seconds to wait before controls are restored to the character.")]
    [SerializeField] private float dashTime;

    [Header("Arc Jump")]
    [Tooltip("How long the player is able to use the arc jump after the inital dash.")]
    [SerializeField] private float arcJumpInputTime;

    //[Tooltip("The time in seconds to wait before controls are restored to the character after an arc jump.")]
    //[SerializeField] private float arcJumpTime;

    [Tooltip("How intense the arc jump is.")]
    [SerializeField] private float arcForce;

    private Rigidbody rb;

    private bool arcJumped = false;

    protected override void ExecuteAbility(CharacterMotor characterMotor, object args)
    {
        if (rb == null)
            rb = characterMotor.GetComponent<Rigidbody>();

        StartCoroutine(PerformDash(characterMotor, args));
    }

    private IEnumerator PerformDash(CharacterMotor characterMotor, object args)
    {
        Vector3 input = (Vector3)args;

        characterMotor.CanMove = false;
        rb.AddForce(input * dashStrength, ForceMode.VelocityChange);

        StartCoroutine(ArcJump(characterMotor));
        yield return new WaitForSeconds(dashTime);

        if (!arcJumped)
            characterMotor.CanMove = true;
    }

    private IEnumerator ArcJump(CharacterMotor characterMotor)
    {
        float currentTime = 0f;
        while (currentTime < arcJumpInputTime)
        {
            currentTime += Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                print("ARCING");
                arcJumped = true;
                rb.AddForce(transform.forward + (Vector3.up / 2) * arcForce, ForceMode.VelocityChange);
                while (!characterMotor.Grounded)
                {
                    yield return null;
                }

                print("DONE ARCING");
                arcJumped = false;
                characterMotor.CanMove = true;

                yield break;
            }

            yield return null;
        }
    }

    #region --- **OLD** Dash Implementation 2: Reenable player control once velocity is 0 ---
    //[Header("Drag and Angular Drag")]
    //[Tooltip("The drag to apply to the character during a dash.")]
    //[SerializeField] private float drag;
    //[Tooltip("The angular drag to apply to the character during a dash.")]
    //[SerializeField] private float angularDrag;


    //private IEnumerator DashTwo(CharacterMotor characterMotor, object args)
    //{
    //    Vector3 input = (Vector3)args;


    //    characterMotor.CanMove = false;

    //    rb.AddForce(input * dashStrength, ForceMode.VelocityChange);

    //    float previousDrag = rb.drag;
    //    float previousAngularDrag = rb.angularDrag;

    //    rb.drag = drag;
    //    rb.angularDrag = angularDrag;

    //    while (Mathf.Abs(rb.velocity.x) > 1f && Mathf.Abs(rb.velocity.z) > 1f)
    //    {
    //        yield return null;
    //    }

    //    rb.drag = previousDrag;
    //    rb.angularDrag = previousAngularDrag;
    //    characterMotor.CanMove = true;
    //}

    #endregion
}