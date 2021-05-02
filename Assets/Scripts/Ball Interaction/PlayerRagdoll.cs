/*****************************************************************************
// File Name :         PlayerRagdoll.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class PlayerRagdoll : MonoBehaviour
{
    [SerializeField] private Transform rbs;

    private void Start()
    {
        Rigidbody[] childRbs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody r in childRbs)
            r.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }

    private void FixedUpdate()
    {
        //rbs.MovePosition(transform.position);
        rbs.position = transform.position;
    }

    private void OnDisable()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).localPosition = Vector3.zero;
        }
    }
}