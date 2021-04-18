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
[RequireComponent(typeof(PlayerAimController))]
public class CharacterRotateWithCamera : MonoBehaviour
{
    private CharacterRotater rotater;
    private CharacterMotor motor;
    private PlayerAimController aimController;
    private int x_invertModifier = 1;
    private int y_invertModifier = 1;

    [SerializeField] private Transform followTransform;

    [Tooltip("The sensitivity of the camera's rotation.")]
    [SerializeField] private float sensitivity;

    [Header("Main Look Angles")]
    [Range(270f, 360f)]
    [SerializeField] private float minLookAngle;
    private float baseMinLookAngle;

    [Range(0, 90f)]
    [SerializeField] private float maxLookAngle;
    private float baseMaxLookAngle;

    [Header("Aiming Look Angles")]
    [Range(270f, 360f)]
    [SerializeField] private float minAimLookAngle;

    [Range(0, 90f)]
    [SerializeField] private float maxAimLookAngle;

    private void Awake()
    {
        rotater = GetComponent<CharacterRotater>();
        motor = GetComponent<CharacterMotor>();
        aimController = GetComponent<PlayerAimController>();
    }

    private void Start()
    {
        baseMinLookAngle = minLookAngle;
        baseMaxLookAngle = maxLookAngle;
    }

    public void InvertMouseX(bool invert)
    {
        x_invertModifier = invert ? -1 : 1;
    }

    public void InvertMouseY(bool invert)
    {
        y_invertModifier = invert ? -1 : 1;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * x_invertModifier;
        float mouseY = Input.GetAxis("Mouse Y") * y_invertModifier;

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

        // Rotate the player if they are moving, aiming, or both.
        if (motor.IsMoving() || aimController.IsAiming())
        {
            //Set the player rotation based on the look transform
            transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);

            //reset the y rotation of the look transform because rotating the player will already 0 it out!
            followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.forward = -transform.forward;
            followTransform.forward = transform.forward;
        }
    }

    public void SwitchToAimAngles()
    {
        minLookAngle = minAimLookAngle;
        maxLookAngle = maxAimLookAngle;
    }

    public void ResetAngles()
    {
        minLookAngle = baseMinLookAngle;
        maxLookAngle = baseMaxLookAngle;
    }
}
