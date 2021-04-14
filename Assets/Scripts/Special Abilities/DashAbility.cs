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

    [Tooltip("The drag of the rigidbody mid dash. Used to slow it down. Will be reset at the end of the dash.")]
    [SerializeField] private float dashDrag;
    private float oldDrag;

    [Tooltip("The time in seconds to wait before controls are restored to the character.")]
    [SerializeField] private float dashTime;

    [Tooltip("How intense the arc jump is.")]
    [SerializeField] private float arcForwardForce;
    [SerializeField] private float arcUpwardsForce;

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
        //characterMotor.ResetVelocity();
        Vector3 input = characterMotor.LocalMovementDirection;

        oldDrag = rb.drag;
        rb.drag = dashDrag;

        //characterMotor.CanMove = false;
        rb.AddForce(input * dashStrength, ForceMode.VelocityChange);

        StartCoroutine(ArcJump(characterMotor));
        yield return new WaitForSeconds(dashTime);

        //if (!arcJumped)
        //{
        //    print("no arc jump, so moving!");
        //    rb.drag = 0;
        //}
    }

    private IEnumerator ArcJump(CharacterMotor characterMotor)
    {
        float currentTime = 0f;

        // We keep track of the dashTime so we HAVE to jump mid dash.
        while (currentTime < dashTime)
        {
            currentTime += Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                print("ARCING");
                arcJumped = true;

                Vector3 input = characterMotor.LocalMovementDirection;

                rb.drag = oldDrag;
                rb.AddForce((input * arcForwardForce) + (Vector3.up * arcUpwardsForce), ForceMode.VelocityChange);

                yield return new WaitForSeconds(dashTime);

                arcJumped = false;
            }

            yield return null;
        }

        rb.drag = oldDrag;
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