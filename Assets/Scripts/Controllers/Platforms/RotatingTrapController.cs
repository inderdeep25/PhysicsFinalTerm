using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTrapController : MonoBehaviour 
{

    [SerializeField] private float _turnSpeed = 100f;

    // Use this for initialization
    void Start () 
    {
		
	}
	
    void RotateTrap() 
    {
        transform.Rotate(0, _turnSpeed * Time.deltaTime, 0);
    }

    // Update is called once per frame
    void Update () 
    {
        RotateTrap();

    }
}
