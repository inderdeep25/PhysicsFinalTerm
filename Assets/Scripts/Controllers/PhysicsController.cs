using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{

    [SerializeField] private float _jumpForce = 3;

    private Rigidbody _rigidBody;

    public float speed = 5.0f;
    private float _turnSpeed = 100f;
    private float _lastJumpTime = 0;
    private float _minJumpInterval = 0.25f;
    private bool _isGrounded;
    private bool _wasGrounded;
    private List<Collider> _collisions = new List<Collider>();

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _wasGrounded = _isGrounded;
    }

    public void HandleMovement()
    {
        float forwardSpeed = Input.GetAxis("Vertical") * speed;
        float strafeSpeed = Input.GetAxis("Horizontal") * speed;
        Vector3 forwardAcceleration = forwardSpeed * transform.forward;
        Vector3 strafeforwarAcceleration = strafeSpeed * transform.right;

        _rigidBody.AddForce(forwardAcceleration, ForceMode.Acceleration);
        transform.Rotate(0, Input.GetAxis("Horizontal") * _turnSpeed * Time.deltaTime, 0);


    }

    public void HandleJump()
    {
        bool isJumpCooldownOver = (Time.time - _lastJumpTime) >= _minJumpInterval;

        if (isJumpCooldownOver && _isGrounded && Input.GetKey(KeyCode.Space))
        {
            _lastJumpTime = Time.time;
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!_collisions.Contains(collision.collider))
                {
                    _collisions.Add(collision.collider);
                }
                _isGrounded = true;
                _rigidBody.velocity = Vector3.zero;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_collisions.Contains(collision.collider))
        {
            _collisions.Remove(collision.collider);
        }
        if (_collisions.Count == 0) 
        {
            _isGrounded = false;
        }
    }



}

