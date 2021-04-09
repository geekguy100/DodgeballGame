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
    [SerializeField] private Transform cam;

    private void Awake()
    {
        rotater = GetComponent<CharacterRotater>();
        motor = GetComponent<CharacterMotor>();
    }

    private void Update()
    {
        Vector3 movementDir = transform.InverseTransformDirection(motor.LocalMovementDirection);

        // Only rotate the character with the camera if we are moving forwards or backwards.
        if (movementDir.z != 0)
        {
            Vector3 camEulerAngles = cam.localRotation.eulerAngles;
            camEulerAngles.x = 0f;

            rotater.SetRotation(camEulerAngles);
        }
    }
}
