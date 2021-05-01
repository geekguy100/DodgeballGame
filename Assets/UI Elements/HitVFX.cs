/*****************************************************************************
// File Name :         HitVFX.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class HitVFX : MonoBehaviour
{
    [SerializeField] private GameObject hitParticles;

    private void Start()
    {
        Instantiate(hitParticles, transform.parent);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
