using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidZone : MonoBehaviour
{
    public bool ballInMidZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallManager>() != null)
            ballInMidZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BallManager>() != null)
            ballInMidZone = false;
    }
}
