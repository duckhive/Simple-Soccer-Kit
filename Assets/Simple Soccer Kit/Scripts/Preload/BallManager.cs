using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Vector3 startPosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    private void Update()
    {
        OutOfBounds();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (GameManager.Instance.ballShot)
        {
            if (other.collider.GetComponent<PitchManager>() == null)
                GameManager.Instance.ballShot = false;
        }
    }

    public void ResetBall()
    {
        transform.position = startPosition;
        transform.SetParent(null);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.None;
    }

    public void OutOfBounds()
    {
        if (transform.position.y < 0)
        {
            GameManager.Instance.ResetBallAndPlayers();
            GameManager.Instance.ballShot = false;
        }
    }
}
