using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingPlatformController : MonoBehaviour {

    private Rigidbody _rigidBody;
    private Collider _collider;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
        _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private IEnumerator EnableGravityOnPlatform(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _rigidBody.useGravity = true;
        _collider.isTrigger = true;
        _rigidBody.constraints = RigidbodyConstraints.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player") 
        {
            StartCoroutine(EnableGravityOnPlatform(4.5f)); 
        }
    }
	
}
