/*****************************************************************************
// File Name :         Obstruction.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Obstruction : MonoBehaviour
{
    [Tooltip("The game object's transparent material.")]
    [SerializeField] private Material obstructedMaterial;

    [Tooltip("The game object's opaque material.")]
    [SerializeField] private Material unobstructedMaterial;

    private MeshRenderer meshRenderer;



    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// Start by making the game object opaque.
    /// </summary>
    private void Start()
    {
        meshRenderer.material = unobstructedMaterial;
    }

    /// <summary>
    /// Makes the game object transparent.
    /// </summary>
    public void OnObstructed()
    {
        meshRenderer.material = obstructedMaterial;
    }

    /// <summary>
    /// Makes the game object opaque.
    /// </summary>
    public void OnUnobstructed()
    {
        meshRenderer.material = unobstructedMaterial;
    }
}
