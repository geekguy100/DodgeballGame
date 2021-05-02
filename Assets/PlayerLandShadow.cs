/*****************************************************************************
// File Name :         PlayerLandShadow.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class PlayerLandShadow : MonoBehaviour
{
    [SerializeField] private GameObject shadow;
    private CharacterStateManager stateManager;

    private void Awake()
    {
        stateManager = GetComponentInParent<CharacterStateManager>();
    }

    public void CheckAndDisplay(bool grounded)
    {
        if (!stateManager.canMove && shadow.activeInHierarchy)
        {
            shadow.SetActive(false);
            return;
        }


        if (!grounded)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100f))
            {
                if (!shadow.activeInHierarchy)
                    shadow.SetActive(true);
                shadow.transform.position = hit.point + Vector3.up * 0.2f;
            }
        }
        else if (shadow.activeInHierarchy)
            shadow.SetActive(false);

    }
}
