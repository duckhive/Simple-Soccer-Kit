using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTarget : MonoBehaviour
{
    private Vector3 _resetPosition;

    private void Awake()
    {
        _resetPosition = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = _resetPosition;
    }
}
