using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public bool ballInDangerZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallManager>() != null)
            ballInDangerZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BallManager>() != null)
            ballInDangerZone = false;
    }
}
