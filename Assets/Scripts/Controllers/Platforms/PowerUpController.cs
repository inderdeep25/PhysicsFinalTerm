using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

    public int boostPercentage;

    private Transform _player;

	// Use this for initialization
	void Start () {
        _player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(_player);
	}

}
