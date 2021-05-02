/*****************************************************************************
// File Name :         EnemyDodgeball.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class EnemyDodgeball : MonoBehaviour
{
    public LayerMask whatIsGround;

    private void OnCollisionEnter(Collision collision)
    {
        if (whatIsGround == (whatIsGround | 1 << collision.gameObject.layer))
        {
            //print(collision.gameObject.name);
            gameObject.tag = "Dodgeball";
            Destroy(this);
        }
    }
}
