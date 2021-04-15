/*****************************************************************************
// File Name :         CharacterRotateWithCamera.cs
// Author :            Kyle Grenier
// Creation Date :     04/07/2021
//
// Brief Description : Rotates the character based on a camera's rotation.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(CharacterRotater))]
[RequireComponent(typeof(CharacterMotor))]
public class CharacterRotateWithCamera : MonoBehaviour
{
    private CharacterRotater rotater;
    private CharacterMotor motor;

    [SerializeField] private Transform followTransform;

    [Tooltip("The sensitivity of the camera's rotation.")]
    [SerializeField] private float sensitivity;

    [Range(270f, 360f)]
    [SerializeField] private float minLookAngle;

    [Range(0, 90f)]
    [SerializeField] private float maxLookAngle;

    private void Awake()
    {
        rotater = GetComponent<CharacterRotater>();
        motor = GetComponent<CharacterMotor>();
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        #region --- Horizontal Camera Rotation ---

        //Rotate the Follow Target transform based on the input
        followTransform.transform.rotation *= Quaternion.AngleAxis(mouseX * sensitivity, Vector3.up);

        #endregion


        #region --- Vertical Camera Rotation ---

        followTransform.transform.rotation *= Quaternion.AngleAxis(mouseY * sensitivity, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < minLookAngle)
        {
            angles.x = minLookAngle;
        }
        else if (angle < 180 && angle > maxLookAngle)
        {
            angles.x = maxLookAngle;
        }

        followTransform.transform.localEulerAngles = angles;

        #endregion



        if (motor.IsMoving())
        {
            //Set the player rotation based on the look transform
            transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);

            //reset the y rotation of the look transform because rotating the player will already 0 it out!
            followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }
    }

    private void RotateFollowTransform()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        followTransform.transform.rotation *= Quaternion.AngleAxis(mouseX * sensitivity, Vector3.up);
        followTransform.transform.rotation *= Quaternion.AngleAxis(mouseY * sensitivity, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }


        followTransform.transform.localEulerAngles = angles;
    }
}
