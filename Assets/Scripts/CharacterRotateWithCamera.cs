/*****************************************************************************
// File Name :         CharacterRotateWithCamera.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(CharacterRotater))]
public class CharacterRotateWithCamera : MonoBehaviour
{
    private CharacterRotater rotater;
    [SerializeField] private Transform cam;

    private void Awake()
    {
        rotater = GetComponent<CharacterRotater>();
    }

    private void Update()
    {
        Vector3 camEulerAngles = cam.localRotation.eulerAngles;
        camEulerAngles.x = 0f;

        rotater.SetRotation(camEulerAngles);
    }
}
