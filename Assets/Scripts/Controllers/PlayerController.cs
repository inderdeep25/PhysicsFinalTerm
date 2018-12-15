using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PhysicsController))]
[RequireComponent(typeof(PlayerLaunchProjectileController))]

public class PlayerController : MonoBehaviour
{

    private PlayerLaunchProjectileController _projectileController = null;
    private PhysicsController _physicsController = null;


    void Start()
    {
        _projectileController = GetComponent<PlayerLaunchProjectileController>();
        _physicsController = GetComponent<PhysicsController>();
    }

    void FixedUpdate()
    {
        _physicsController.HandleMovement();
        _physicsController.HandleJump();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "outerSpace")
        {
            //TODO:Add death and restart level code later
        }


    }

    private IEnumerator WaitAndFly(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _projectileController.Launch();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "launcher")
        {
            Debug.Log("In Launcher!");
            StartCoroutine(WaitAndFly(1.5f));
        }

        if (collision.gameObject.name == "WinTarget")
        {
            //TODO: Add win condition later
        }
    }

}
