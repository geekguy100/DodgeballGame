/*****************************************************************************
// File Name :         Enemy.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject metarig;
    [SerializeField] private bool shooting;
    [SerializeField] private GameObject attachedCannon;

    private void Awake()
    {
        metarig.SetActive(false);
    }

    private void Start()
    {
        if (shooting)
            attachedCannon.SetActive(true);
        else
            attachedCannon.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Dodgeball"))
        {
            GetComponent<BoxCollider>().enabled = false;
            metarig.SetActive(true);
            
            // The rig is a sum of its parts, so adding a force to the parent rigibody will have no effect.
            // Instead, we have to apply a force to each Rigidbody that makes up the rig.
            foreach(Rigidbody rb in metarig.GetComponentsInChildren<Rigidbody>())
            {
                #region old implementation
                //if (rb.name.Contains("arm") || rb.name.Contains("thigh") || rb.name.Contains("shin"))
                //    rb.AddForce(-collision.relativeVelocity.normalized * 1f, ForceMode.VelocityChange);
                //else
                //    rb.AddForce(collision.relativeVelocity.normalized * 40f, ForceMode.VelocityChange);

                // Adding a random force in the direction the ball was thrown in order to get varying moving parts.
                #endregion
                float randomForce = Random.Range(30f, 50f);
                rb.AddForce(collision.relativeVelocity.normalized * randomForce, ForceMode.VelocityChange);
            }

            Instantiate(VFXFactory.GetRandomVFXPrefab(), transform);
            GetComponentInParent<FollowPath>().Stop();
            transform.GetChild(0).gameObject.SetActive(false);
            GameStats.EnemyHit();
            EventManager.EnemyHit();
        }
    }
}