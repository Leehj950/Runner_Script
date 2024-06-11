using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlay : MonoBehaviour
{
    private GameObject playerObject;
    private Rigidbody playerRigidbody;
    
    private void Start()
    {
        //playerRigidbody = playerObject.GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.collider.CompareTag("Obstacles"))
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
