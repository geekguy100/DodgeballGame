/*****************************************************************************
// File Name :         CharacterRotater.cs
// Author :            Kyle Grenier
// Creation Date :     04/06/2021
//
// Brief Description : Updates the body rotation of the character.
*****************************************************************************/
using UnityEngine;

public class CharacterRotater : MonoBehaviour
{
    /// <summary>
    /// Sets the character's body rotation.
    /// </summary>
    /// <param name="eulerAngles">The euler angles of the rotation.</param>
    public void SetRotation(Vector3 eulerAngles)
    {
        transform.localRotation = Quaternion.Euler(eulerAngles);
    }

}