using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Goalie : MonoBehaviour
{
    public Vector3 direction;
    public Vector3 homePosition;
    public Team team;

    [SerializeField] private float speed = 300f;
    
    private Player _player;
    private Rigidbody _rb;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();
        team = GetComponentInParent<Team>();
    }

    private void Start()
    {
        //homePosition = GetComponentInParent<Team>().shotTarget.transform.position;
        
        var otherTeam = FindObjectsOfType<Team>()
            .Where(t => t != GetComponentInParent<Team>())
            .FirstOrDefault();

        homePosition = otherTeam.shotTarget.transform.position;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rb.velocity = direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.GetComponent<BallManager>() != null)
            BallManager.Instance.rb.AddForce(-other.contacts[0].normal * 10, ForceMode.Impulse);
    }
}
