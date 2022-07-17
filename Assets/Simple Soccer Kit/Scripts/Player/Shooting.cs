using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooting : MonoBehaviour
{
    public float shotForce;
    public bool shooting;

    private Player _player;
    private float _resetShotForce;
    private float _shotTimer;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _resetShotForce = shotForce;
    }

    private void Update()
    {
        if (GameManager.Instance.gameActive)
        {
            if (_player.user)
            {
                if (_player.hasPossession)
                {
                    if (_player.canShoot)
                    {
                        if (Input.GetButton("Shoot"))
                        {
                            AimTarget();
                            _shotTimer += Time.deltaTime;
                            shotForce += Time.deltaTime * 10;
                            StartCoroutine(ShootingCooldown());
                        }

                        if ((Input.GetButtonUp("Shoot") || _shotTimer > 0.75f))
                        {
                            ShootBall();
                            shotForce = _resetShotForce;
                            _shotTimer = 0;
                            _player.team.shotTarget.ResetPosition();
                            GameManager.Instance.ballShot = true;
                        }
                    }   
                }
            }
        }
    }

    private IEnumerator ShootingCooldown()
    {
        shooting = true;

        yield return new WaitForSeconds(0.75f);

        shooting = false;
    }

    public void ShootBall()
    {
        var direction = (_player.team.shotTarget.transform.position - BallManager.Instance.transform.position).normalized;
        
        BallManager.Instance.rb.constraints = RigidbodyConstraints.None;
        BallManager.Instance.transform.SetParent(null);
        BallManager.Instance.rb.AddForce(direction * shotForce, ForceMode.Impulse);
        _player.hasPossession = false;
        _player.team.hasPossession = false;
    }

    private void AimTarget()
    {
        var shotTarget = _player.team.shotTarget;
        
        shotTarget.transform.Translate(Vector3.up * Time.deltaTime * 5f);
        
        var zInput = Input.GetAxis("Vertical");
        
        if(zInput != 0)
            shotTarget.transform.Translate(shotTarget.transform.right * 10 * zInput * Time.deltaTime);
    }
}
