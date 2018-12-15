using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraFollow : MonoBehaviour
{

    private Rigidbody _rigidBody = null;
    private float _targetHead = .2f;

    [SerializeField] private Vector3 _offset = new Vector3(0f, 1.5f, -2.0f);
    [SerializeField] private Transform _target;
    [SerializeField] private float _distance = 3.0f;
    [SerializeField] private float _height = 2.0f;
    [SerializeField] private float _rotationDamping = 1.5f;
    [SerializeField] private float _heightDamping;

    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {

        transform.position = _target.position + _offset;

        var wantedRotationAngle = _target.eulerAngles.y;
        var wantedHeight = _target.position.y; //+ height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position = _target.position;
        transform.position -= currentRotation * Vector3.forward * _distance;
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        transform.LookAt(new Vector3(_target.position.x, transform.position.y - _targetHead, _target.position.z));
    }

}
