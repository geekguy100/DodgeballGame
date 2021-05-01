/*****************************************************************************
// File Name :         Billboard.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}