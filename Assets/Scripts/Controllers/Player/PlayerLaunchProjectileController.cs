using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunchProjectileController : MonoBehaviour
{

    private Transform _desiredDestination = null;
    private Rigidbody _rigidBody = null;

    [SerializeField] private float verticalAngle = 50.0f;
    [SerializeField] private float horizontalAngle = 5.0f;
    [SerializeField] private float initialVelocity = 3.0f;
    [SerializeField] private float inputForce = 15.0f;

    private bool _isGrounded = true;
    public bool IsGrounded
    {
        get { return _isGrounded; }
    }

    void Start()
    {
        _desiredDestination = GameObject.Find("LaunchDestination").transform;
        _rigidBody = GetComponent<Rigidbody>();
        transform.LookAt(_desiredDestination);
    }

    public void Launch()
    {
        if (IsGrounded)
        {
            _isGrounded = false;
            transform.LookAt(_desiredDestination);
            _rigidBody.velocity = GetLandingPosition() * inputForce;
        }


    }

    Vector3 GetLandingPosition()
    {
        float vertAngleRad = Mathf.Deg2Rad * verticalAngle;
        float hozAngleRad = Mathf.Deg2Rad * horizontalAngle;
        float dh = ((-2 * initialVelocity * initialVelocity * Mathf.Sin(vertAngleRad) * Mathf.Cos(vertAngleRad)) / Physics.gravity.y);

        Vector3 landingPosition = new Vector3(Mathf.Sin(hozAngleRad), Mathf.Sin(vertAngleRad), Mathf.Cos(hozAngleRad));

        return landingPosition;

    }

}
