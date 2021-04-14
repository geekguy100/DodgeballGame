/*****************************************************************************
// File Name :         ObstructionCheck.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class ObstructionCheck : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsObstruction;
    [SerializeField] private float rayLength = 2f;

    private Transform obstructionObject;
    private Obstruction obstruction;

    private void Update()
    {
        // If we hit a wall, hide it from the camera view.
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayLength, whatIsObstruction))
        {
            // If we already hit the same thing, return.
            if (hit.transform == obstruction)
                return;

            if (obstructionObject != null)
                ResetObstruction();

            obstructionObject = hit.transform;

            SetupObstruction();
        }
        // If we don't hit anything, reset our obstruction object.
        else if (obstructionObject != null)
            ResetObstruction();
    }
    
    private void SetupObstruction()
    {
        obstruction = obstructionObject.GetComponent<Obstruction>();
        obstruction.OnObstructed();
    }

    private void ResetObstruction()
    {
        obstruction.OnUnobstructed();
        obstruction = null;
        obstructionObject = null;
    }
}
