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

    [SerializeField] CanvasHandler _canvasHandler;


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

    private IEnumerator WaitAndFly(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _projectileController.Launch();
    }

    private IEnumerator WaitAndSetSpeedBackTo(float originalSpeed, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _physicsController.speed = originalSpeed;
    }

    private IEnumerator WaitAndSetJumpForceTo(float originamForce, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _physicsController.jumpForce = originamForce;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SpeedBoost") 
        {
            double boostPercentage = (double) other.gameObject.GetComponent<PowerUpController>().boostPercentage / 100f;
            float originalSpeed = _physicsController.speed;

            double newSpeed = originalSpeed + originalSpeed * boostPercentage;

            Destroy(other.gameObject);

            _physicsController.speed = (float)newSpeed;
            Debug.Log("NewSpeed is " + _physicsController.speed.ToString());
            StartCoroutine(WaitAndSetSpeedBackTo(originalSpeed, 10f));
        }
        else if (other.gameObject.tag == "JumpBoost")
        {
            double boostPercentage = (double) other.gameObject.GetComponent<PowerUpController>().boostPercentage / 100f;
            float originamForce = _physicsController.jumpForce;

            double newJumpForce = originamForce + originamForce * boostPercentage;

            Destroy(other.gameObject);
            _physicsController.jumpForce = (float) newJumpForce;
            StartCoroutine(WaitAndSetJumpForceTo(originamForce, 10f));
        }

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
            Debug.Log("You Won!");
            _canvasHandler.SetResultText("You Won!");
            _canvasHandler.gameObject.SetActive(true);
        }

        if (collision.gameObject.name == "Plane")
        {
            //TODO:Add death and restart level code later
            Debug.Log("You Died!");
            _canvasHandler.SetResultText("You Died!");
            _canvasHandler.gameObject.SetActive(true);
        }
    }

}
