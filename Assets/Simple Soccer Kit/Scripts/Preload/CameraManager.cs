using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    //public CinemachineVirtualCamera cmCamera;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y,
            BallManager.Instance.transform.position.z), 0.1f);
    }
}
