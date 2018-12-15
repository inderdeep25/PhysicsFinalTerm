using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilShootingController : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject projectilePrefab;
    private GameObject _player;
    private bool _firing;
    private bool _isPlayerInRange;

    [SerializeField] private float _speed = 25f;

    // Use this for initialization
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    private IEnumerator Shoot(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        _firing = false;
        GameObject p = Instantiate(projectilePrefab, spawnPoint.transform.position, Quaternion.identity);
        p.GetComponent<Rigidbody>().velocity = (spawnPoint.transform.forward + Vector3.up/10) * _speed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            spawnPoint.LookAt(_player.transform);
            if (_firing == false)
            {
                _firing = true;
                StartCoroutine(Shoot(2.5f));
            }
        }
    }

}