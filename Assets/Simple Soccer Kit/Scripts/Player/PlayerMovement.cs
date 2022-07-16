using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _turboSpeed;
    private Rigidbody _rb;
    private Vector3 _movementVector;
    private Player _player;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _turboSpeed = _player.speed * 1.2f;
    }

    private void Update()
    {
        _movementVector = InputDirection();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameActive)
        {
            if (_player.user)
            {
                if(Input.GetAxis("Turbo") == 0)
                    _rb.velocity = _movementVector * _player.speed * Time.deltaTime;

                if (Input.GetAxis("Turbo") > 0)
                    _rb.velocity = _movementVector * _turboSpeed * Time.deltaTime;
                
                if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                    transform.rotation = Quaternion.LookRotation(_movementVector);
            }

            else
            {
                if(_player.rb.velocity != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(_rb.velocity);
            }
        }
    }
    
    private Vector3 InputDirection()
    {
        var xInput = Input.GetAxis("Horizontal");
        var zInput = Input.GetAxis("Vertical");
        var forward = Camera.main.transform.forward;
        forward.y = 0;
        var right = Camera.main.transform.right;
        right.y = 0;

        return (right * xInput + forward * zInput).normalized;
    }
}
