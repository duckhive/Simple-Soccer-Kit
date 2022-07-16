using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private Team _team;

    private void Awake()
    {
        _team = GetComponentInParent<Team>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallManager>() != null)
        {
            _team.score++;
            StartCoroutine(GameManager.Instance.GoalScored());
        }
    }
}
