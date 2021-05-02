/*****************************************************************************
// File Name :         CharacterStateManager.cs
// Author :            Kyle Grenier
// Creation Date :     05/02/2021
//
// Brief Description : Class to store data on the character's state.
                       Used as an intermediary class for other classes to get info on the character's state.
*****************************************************************************/
using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    public bool canMove { get; set; }

    private void Start()
    {
        canMove = true;
    }
}